using System;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: REGIONES DE DISTRIBUCIÓN (Union-Find)
    // Responsable: Johann
    //
    // Muestra las regiones (cada una con el color que tiene en el mapa), los
    // arreglos padre y rango tal como están en memoria, y permite verificar con
    // Find si dos ciudades pertenecen a la misma región.
    public partial class PestanaRegiones : PestanaBase
    {
        public PestanaRegiones()
        {
            InitializeComponent();
            autoCiudad1.AsignarProveedor(SugerirCiudades);
            autoCiudad2.AsignarProveedor(SugerirCiudades);
            autoCiudad2.Confirmado += autoCiudad2_Confirmado;

            dgvArreglos.DefaultCellStyle.SelectionBackColor = Paleta.CeldaResaltada;
            dgvArreglos.DefaultCellStyle.SelectionForeColor = Paleta.Texto;
            dgvRegiones.DefaultCellStyle.SelectionBackColor = Paleta.CeldaResaltada;
            dgvRegiones.DefaultCellStyle.SelectionForeColor = Paleta.Texto;
        }

        public override void Refrescar()
        {
            Grafo mapa = sistema.Mapa;
            UnionFind regiones = sistema.Regiones;
            int n = regiones.Cantidad;

            // Primero se leen padre y rango tal como están, porque NumerarRegiones
            // llama a Find y eso comprime los caminos
            int[] padres = new int[n];
            int[] rangos = new int[n];
            for (int i = 0; i < n; i++)
            {
                padres[i] = regiones.ObtenerPadre(i);
                rangos[i] = regiones.ObtenerRango(i);
            }

            int[] numeroRegion = regiones.NumerarRegiones();
            int total = regiones.ContarRegiones();

            // Arreglos del Union-Find
            dgvArreglos.Rows.Clear();
            for (int i = 0; i < n; i++)
            {
                int fila = dgvArreglos.Rows.Add(i, mapa.ObtenerNombre(i), mapa.ObtenerNombre(padres[i]),
                                                rangos[i], numeroRegion[i]);
                dgvArreglos.Rows[fila].DefaultCellStyle.BackColor =
                    Paleta.Aclarar(Paleta.ColorRegion(numeroRegion[i]), 0.85f);
            }

            // Una fila por región con sus integrantes
            int[] cantidad = new int[total + 1];
            int[] representante = new int[total + 1];
            string[] integrantes = new string[total + 1];

            for (int i = 0; i < n; i++)
            {
                int region = numeroRegion[i];
                cantidad[region]++;
                representante[region] = regiones.Find(i);
                integrantes[region] = integrantes[region] == null
                    ? mapa.ObtenerNombre(i)
                    : integrantes[region] + ", " + mapa.ObtenerNombre(i);
            }

            dgvRegiones.Rows.Clear();
            for (int region = 1; region <= total; region++)
            {
                int fila = dgvRegiones.Rows.Add("", "Región " + region, mapa.ObtenerNombre(representante[region]),
                                                cantidad[region], integrantes[region]);
                dgvRegiones.Rows[fila].Cells[colColor.Index].Style.BackColor = Paleta.ColorRegion(region);
                dgvRegiones.Rows[fila].Cells[colColor.Index].Style.SelectionBackColor = Paleta.ColorRegion(region);
            }

            lblRegiones.Text = "Regiones de distribución (" + total + ")";
            dgvRegiones.ClearSelection();
            dgvArreglos.ClearSelection();
        }

        private void autoCiudad2_Confirmado(object? sender, EventArgs e)
        {
            Verificar();
        }

        private void btnVerificar_Click(object? sender, EventArgs e)
        {
            Verificar();
        }

        private void Verificar()
        {
            Grafo mapa = sistema.Mapa;
            int a = mapa.BuscarIndice(autoCiudad1.Texto);
            int b = mapa.BuscarIndice(autoCiudad2.Texto);

            if (a == -1 || b == -1)
            {
                txtResultado.Text = "✖ Elija dos ciudades registradas (puede escribir solo el inicio del nombre).";
                return;
            }

            int raizA = sistema.Regiones.Find(a);
            int raizB = sistema.Regiones.Find(b);

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("Find(" + mapa.ObtenerNombre(a) + ") = " + mapa.ObtenerNombre(raizA));
            texto.AppendLine("Find(" + mapa.ObtenerNombre(b) + ") = " + mapa.ObtenerNombre(raizB));
            texto.AppendLine();

            if (raizA == raizB)
            {
                texto.Append("✔ Tienen el mismo representante: pertenecen a la misma región de distribución.");
            }
            else
            {
                texto.Append("✖ Tienen representantes distintos: pertenecen a regiones distintas y no hay ninguna ruta entre ellas.");
            }

            txtResultado.Text = texto.ToString();

            // Se vuelve a mostrar la tabla para ver el efecto de la compresión de camino
            Refrescar();
            dgvArreglos.Rows[a].Selected = true;
            dgvArreglos.Rows[b].Selected = true;
        }
    }
}
