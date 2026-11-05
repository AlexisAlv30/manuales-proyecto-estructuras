using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: AUTOCOMPLETADO (Trie)
    // Responsable: Johann
    //
    // A la izquierda se prueban las operaciones del Trie con un prefijo
    // (SugerirPorPrefijo, Buscar y EmpiezaCon). A la derecha se dibuja el árbol
    // tal como está en memoria, recorriendo PrimerHijo y Hermano de cada nodo,
    // y se resalta el camino que sigue el prefijo escrito.
    public partial class PestanaTrie : PestanaBase
    {
        // Nodos resaltados del prefijo actual, para poder quitarles el color
        private TreeNode[] caminoResaltado = new TreeNode[0];

        public PestanaTrie()
        {
            InitializeComponent();
        }

        // Trie que se está consultando según la opción elegida
        private Trie TrieActual
        {
            get { return rdoCiudades.Checked ? sistema.TrieCiudades : sistema.TrieCodigos; }
        }

        public override void Refrescar()
        {
            ConstruirArbol();
            ActualizarBusqueda();
        }

        // DIBUJO DEL ÁRBOL

        private void ConstruirArbol()
        {
            Trie trie = TrieActual;

            trvTrie.BeginUpdate();
            trvTrie.Nodes.Clear();
            caminoResaltado = new TreeNode[0];

            TreeNode raiz = new TreeNode("(raíz)");
            raiz.Tag = trie.Raiz;
            AgregarHijos(trie.Raiz, raiz);
            trvTrie.Nodes.Add(raiz);
            raiz.Expand();

            trvTrie.EndUpdate();
            lblTotal.Text = "Palabras guardadas en el Trie: " + trie.Cantidad;
        }

        // Recorre los hijos de un nodo: primero PrimerHijo y luego cada Hermano
        private void AgregarHijos(NodoTrie nodo, TreeNode nodoVisual)
        {
            NodoTrie? hijo = nodo.PrimerHijo;

            while (hijo != null)
            {
                TreeNode visual = new TreeNode(TextoNodo(hijo));
                visual.Tag = hijo;

                if (hijo.EsFinPalabra)
                {
                    visual.ForeColor = Paleta.Exito;
                }

                nodoVisual.Nodes.Add(visual);
                AgregarHijos(hijo, visual);
                hijo = hijo.Hermano;
            }
        }

        private string TextoNodo(NodoTrie nodo)
        {
            string letra = nodo.Letra == ' ' ? "(espacio)" : "'" + nodo.Letra + "'";

            if (nodo.EsFinPalabra)
            {
                return letra + "  ✔ " + nodo.PalabraOriginal;
            }

            return letra;
        }

        // BÚSQUEDA POR PREFIJO

        private void ActualizarBusqueda()
        {
            Trie trie = TrieActual;
            string prefijo = txtPrefijo.Text;

            string[] sugerencias = trie.SugerirPorPrefijo(prefijo).ConvertirArreglo();
            lstSugerencias.BeginUpdate();
            lstSugerencias.Items.Clear();
            foreach (string sugerencia in sugerencias)
            {
                lstSugerencias.Items.Add(sugerencia);
            }
            lstSugerencias.EndUpdate();
            lblSugerencias.Text = "Sugerencias (SugerirPorPrefijo): " + sugerencias.Length;

            if (prefijo.Trim() == "")
            {
                lblPalabraCompleta.Text = "¿Es una palabra completa? (Buscar): —";
                lblExistePrefijo.Text = "¿Existe el prefijo? (EmpiezaCon): —";
                lblPalabraCompleta.ForeColor = Paleta.Texto;
                lblExistePrefijo.ForeColor = Paleta.Texto;
            }
            else
            {
                bool completa = trie.Buscar(prefijo);
                bool existe = trie.EmpiezaCon(prefijo);

                lblPalabraCompleta.Text = "¿Es una palabra completa? (Buscar): " + (completa ? "Sí" : "No");
                lblExistePrefijo.Text = "¿Existe el prefijo? (EmpiezaCon): " + (existe ? "Sí" : "No");
                lblPalabraCompleta.ForeColor = completa ? Paleta.Exito : Paleta.Error;
                lblExistePrefijo.ForeColor = existe ? Paleta.Exito : Paleta.Error;
            }

            ResaltarCamino(prefijo.Trim().ToLower());
        }

        // Baja por el árbol letra por letra, igual que BuscarNodo del Trie
        private void ResaltarCamino(string prefijo)
        {
            foreach (TreeNode nodo in caminoResaltado)
            {
                nodo.BackColor = Color.Empty;
            }
            caminoResaltado = new TreeNode[0];

            if (trvTrie.Nodes.Count == 0 || prefijo == "")
            {
                return;
            }

            TreeNode[] camino = new TreeNode[prefijo.Length];
            int largo = 0;
            TreeNode actual = trvTrie.Nodes[0];

            foreach (char letra in prefijo)
            {
                TreeNode? siguiente = null;

                foreach (TreeNode hijo in actual.Nodes)
                {
                    if (hijo.Tag is NodoTrie nodo && nodo.Letra == letra)
                    {
                        siguiente = hijo;
                        break;
                    }
                }

                // El prefijo no existe a partir de esta letra
                if (siguiente == null)
                {
                    break;
                }

                actual = siguiente;
                camino[largo] = actual;
                largo++;
            }

            caminoResaltado = new TreeNode[largo];
            for (int i = 0; i < largo; i++)
            {
                caminoResaltado[i] = camino[i];
                camino[i].BackColor = Paleta.CeldaResaltada;
            }

            if (largo > 0)
            {
                actual.Expand();
                actual.EnsureVisible();
                trvTrie.SelectedNode = actual;
            }
        }

        private void txtPrefijo_TextChanged(object? sender, EventArgs e)
        {
            ActualizarBusqueda();
        }

        private void rdoCiudades_CheckedChanged(object? sender, EventArgs e)
        {
            Refrescar();
        }

        private void btnExpandir_Click(object? sender, EventArgs e)
        {
            trvTrie.ExpandAll();
            if (trvTrie.Nodes.Count > 0)
            {
                trvTrie.Nodes[0].EnsureVisible();
            }
        }

        private void btnContraer_Click(object? sender, EventArgs e)
        {
            trvTrie.CollapseAll();
            if (trvTrie.Nodes.Count > 0)
            {
                trvTrie.Nodes[0].Expand();
            }
        }
    }
}
