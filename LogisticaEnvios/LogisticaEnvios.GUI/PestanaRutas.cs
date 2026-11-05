using System;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: RUTA ÓPTIMA (Dijkstra)
    // Responsable: Johann
    //
    // Calcula la ruta más económica o más rápida entre dos ciudades. Primero el
    // Union-Find valida que estén en la misma región y solo entonces se ejecuta
    // Dijkstra. Se muestra el camino en el mapa y la tabla de distancias.
    public partial class PestanaRutas : PestanaBase
    {
        public PestanaRutas()
        {
            InitializeComponent();
            autoOrigen.AsignarProveedor(SugerirCiudades);
            autoDestino.AsignarProveedor(SugerirCiudades);
            autoDestino.Confirmado += autoDestino_Confirmado;
            panelMapa.CiudadClic += panelMapa_CiudadClic;

            dgvDijkstra.DefaultCellStyle.SelectionBackColor = Paleta.CeldaResaltada;
            dgvDijkstra.DefaultCellStyle.SelectionForeColor = Paleta.Texto;
        }

        public override void Refrescar()
        {
            panelMapa.AsignarSistema(sistema);
            dgvDijkstra.Rows.Clear();
        }

        private void Escribir(string texto)
        {
            txtResultado.Text = texto;
        }

        private void Calcular()
        {
            bool porCosto = rdoCosto.Checked;
            ResultadoRuta ruta = sistema.CalcularRuta(autoOrigen.Texto, autoDestino.Texto, porCosto);
            dgvDijkstra.Rows.Clear();

            if (!ruta.DatosValidos)
            {
                Escribir("✖ " + ruta.Mensaje + "\r\n\r\nElija las ciudades de las sugerencias o haga clic en el mapa.");
                panelMapa.Limpiar();
                return;
            }

            int origen = sistema.Mapa.BuscarIndice(autoOrigen.Texto);
            int destino = sistema.Mapa.BuscarIndice(autoDestino.Texto);

            // Paso 1: Union-Find
            if (!ruta.MismaRegion)
            {
                Escribir("Union-Find: ✖ " + ruta.Mensaje);
                panelMapa.MarcarExtremos(origen, destino);
                return;
            }

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("Union-Find: ✔ ambas ciudades están en la misma región. Se ejecuta Dijkstra.");
            texto.AppendLine();

            // Paso 2: Dijkstra
            LlenarTabla(ruta.Dijkstra!, ruta.Camino);

            if (ruta.Camino == null)
            {
                texto.Append("✖ " + ruta.Mensaje);
                Escribir(texto.ToString());
                panelMapa.MarcarExtremos(origen, destino);
                return;
            }

            string criterio = porCosto ? "Ruta más económica" : "Ruta más rápida";
            texto.AppendLine(criterio + ":");
            texto.AppendLine(sistema.TextoCamino(ruta.Camino));
            texto.AppendLine();
            texto.AppendLine("Costo total: Q" + ruta.CostoTotal.ToString("0.00"));
            texto.AppendLine("Tiempo total: " + ruta.TiempoTotal.ToString("0.0") + " h");
            texto.Append("Tramos: " + (ruta.Camino.Length - 1));
            Escribir(texto.ToString());

            panelMapa.ResaltarCaminos(ruta.Camino, criterio, null, "");
        }

        // Muestra las distancias y los anteriores que dejó Dijkstra
        private void LlenarTabla(ResultadoDijkstra dijkstra, int[]? camino)
        {
            Grafo mapa = sistema.Mapa;

            // Marca qué ciudades forman parte del camino
            bool[] enCamino = new bool[mapa.Cantidad];
            if (camino != null)
            {
                foreach (int ciudad in camino)
                {
                    enCamino[ciudad] = true;
                }
            }

            for (int i = 0; i < mapa.Cantidad; i++)
            {
                string distancia;
                if (dijkstra.Distancias[i] == double.MaxValue)
                {
                    distancia = "∞";
                }
                else if (dijkstra.PorCosto)
                {
                    distancia = "Q" + dijkstra.Distancias[i].ToString("0.00");
                }
                else
                {
                    distancia = dijkstra.Distancias[i].ToString("0.0") + " h";
                }

                string anterior = dijkstra.Anteriores[i] == -1 ? "-" : mapa.ObtenerNombre(dijkstra.Anteriores[i]);
                int fila = dgvDijkstra.Rows.Add(mapa.ObtenerNombre(i), distancia, anterior, enCamino[i] ? "Sí" : "");

                if (enCamino[i])
                {
                    dgvDijkstra.Rows[fila].DefaultCellStyle.BackColor = Paleta.Aclarar(Paleta.RutaPrincipal, 0.85f);
                }
                else if (dijkstra.Distancias[i] == double.MaxValue)
                {
                    dgvDijkstra.Rows[fila].DefaultCellStyle.ForeColor = Paleta.TextoSecundario;
                }
            }

            dgvDijkstra.ClearSelection();
        }

        private void btnCalcular_Click(object? sender, EventArgs e)
        {
            Calcular();
        }

        private void autoDestino_Confirmado(object? sender, EventArgs e)
        {
            if (autoOrigen.Texto != "")
            {
                Calcular();
            }
        }

        // Al cambiar el criterio se recalcula si ya hay origen y destino
        private void rdoCosto_CheckedChanged(object? sender, EventArgs e)
        {
            if (autoOrigen.Texto != "" && autoDestino.Texto != "")
            {
                Calcular();
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            autoOrigen.Texto = "";
            autoDestino.Texto = "";
            dgvDijkstra.Rows.Clear();
            panelMapa.Limpiar();
            Escribir("");
        }

        // Primer clic en el mapa: origen. Segundo clic: destino (y se calcula la ruta).
        private void panelMapa_CiudadClic(object? sender, string ciudad)
        {
            if (autoOrigen.Texto == "" || autoDestino.Texto != "")
            {
                autoOrigen.Texto = ciudad;
                autoDestino.Texto = "";
                dgvDijkstra.Rows.Clear();
                Escribir("Origen: " + ciudad + ".\r\nAhora haga clic en la ciudad de destino.");
                panelMapa.MarcarExtremos(sistema.Mapa.BuscarIndice(ciudad), -1);
            }
            else
            {
                autoDestino.Texto = ciudad;
                Calcular();
            }
        }
    }
}
