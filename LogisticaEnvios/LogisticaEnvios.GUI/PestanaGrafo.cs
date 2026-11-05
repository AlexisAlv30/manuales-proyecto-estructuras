using System;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: CIUDADES Y RUTAS (Grafo)
    // Responsable: Johann
    //
    // Registra ciudades y rutas, elimina rutas y ejecuta los recorridos BFS y
    // DFS. El mapa muestra el grafo; el cuadro de abajo muestra el detalle de
    // cada operación (por ejemplo, cómo cambia la cola en BFS).
    public partial class PestanaGrafo : PestanaBase
    {
        public PestanaGrafo()
        {
            InitializeComponent();
            autoOrigenRuta.AsignarProveedor(SugerirCiudades);
            autoDestinoRuta.AsignarProveedor(SugerirCiudades);
            autoOrigenEliminar.AsignarProveedor(SugerirCiudades);
            autoDestinoEliminar.AsignarProveedor(SugerirCiudades);
            autoInicio.AsignarProveedor(SugerirCiudades);
            autoInicio.Confirmado += autoInicio_Confirmado;
        }

        public override void Refrescar()
        {
            panelMapa.AsignarSistema(sistema);
            panelMapa.MostrarPesos(chkPesos.Checked);
        }

        private void Escribir(string texto)
        {
            txtResultado.Text = texto;
        }

        // CIUDADES

        private void btnRegistrarCiudad_Click(object? sender, EventArgs e)
        {
            double latitud = double.NaN;
            double longitud = double.NaN;
            string textoLatitud = txtLatitud.Text.Trim();
            string textoLongitud = txtLongitud.Text.Trim();

            // Las coordenadas son opcionales, pero si se escriben deben ser números
            if (textoLatitud != "" || textoLongitud != "")
            {
                if (!SistemaLogistica.ConvertirNumero(textoLatitud, out latitud) ||
                    !SistemaLogistica.ConvertirNumero(textoLongitud, out longitud))
                {
                    Escribir("✖ La latitud y la longitud deben ser números (ej. 14.63 y -90.51), o dejarse vacías.");
                    return;
                }
            }

            bool registrada = sistema.RegistrarCiudad(txtNombre.Text, latitud, longitud, out string mensaje);
            Escribir((registrada ? "✔ " : "✖ ") + mensaje);

            if (registrada)
            {
                txtNombre.Text = "";
                txtLatitud.Text = "";
                txtLongitud.Text = "";
                AvisarCambio();
            }
        }

        // RUTAS

        private void btnRegistrarRuta_Click(object? sender, EventArgs e)
        {
            // Antes de agregar la ruta, el sistema consulta el Union-Find para
            // avisar si las ciudades ya estaban en la misma región
            bool registrada = sistema.RegistrarRuta(autoOrigenRuta.Texto, autoDestinoRuta.Texto,
                                                    (double)numCosto.Value, (double)numTiempo.Value,
                                                    chkDobleSentido.Checked, out string mensaje);
            Escribir((registrada ? "✔ " : "✖ ") + mensaje);

            if (registrada)
            {
                AvisarCambio();
            }
        }

        private void btnEliminarRuta_Click(object? sender, EventArgs e)
        {
            bool eliminada = sistema.EliminarRuta(autoOrigenEliminar.Texto, autoDestinoEliminar.Texto,
                                                  chkAmbosSentidos.Checked, out string mensaje);
            Escribir((eliminada ? "✔ " : "✖ ") + mensaje);

            if (eliminada)
            {
                AvisarCambio();
            }
        }

        // RECORRIDOS

        private void autoInicio_Confirmado(object? sender, EventArgs e)
        {
            HacerRecorrido(true);
        }

        private void btnBFS_Click(object? sender, EventArgs e)
        {
            HacerRecorrido(true);
        }

        private void btnDFS_Click(object? sender, EventArgs e)
        {
            HacerRecorrido(false);
        }

        private void HacerRecorrido(bool esBFS)
        {
            Grafo mapa = sistema.Mapa;
            int inicio = mapa.BuscarIndice(autoInicio.Texto);

            if (inicio == -1)
            {
                Escribir("✖ Elija la ciudad de inicio del recorrido (puede escribir solo el inicio del nombre).");
                return;
            }

            ResultadoRecorrido recorrido = esBFS ? mapa.BFS(inicio) : mapa.DFS(inicio);
            string tipo = esBFS ? "BFS" : "DFS";
            int[] orden = recorrido.Orden;

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("RECORRIDO " + tipo + " DESDE " + mapa.ObtenerNombre(inicio).ToUpper());

            if (recorrido.EstadoCola != null)
            {
                texto.AppendLine("Cola inicial: [" + mapa.ObtenerNombre(inicio) + "]");
            }

            string textoOrden = "";
            for (int i = 0; i < orden.Length; i++)
            {
                string linea = (i + 1).ToString().PadLeft(2) + ". Se visita: " + mapa.ObtenerNombre(orden[i]);
                if (recorrido.EstadoCola != null)
                {
                    linea = linea.PadRight(36) + "Cola: [" + recorrido.EstadoCola[i] + "]";
                }
                texto.AppendLine(linea);

                textoOrden += (i == 0 ? "" : " → ") + mapa.ObtenerNombre(orden[i]);
            }

            texto.AppendLine("Orden final " + tipo + ": " + textoOrden);

            int noVisitadas = mapa.Cantidad - orden.Length;
            if (noVisitadas > 0)
            {
                texto.Append("No se alcanzaron " + noVisitadas + " ciudad(es): están en otra región " +
                             "o solo se llega a ellas por rutas de un solo sentido.");
            }

            Escribir(texto.ToString());
            panelMapa.ResaltarRecorrido(orden);
        }

        // Impresión del grafo: cada ciudad con sus rutas de salida
        private void btnAdyacencia_Click(object? sender, EventArgs e)
        {
            Grafo mapa = sistema.Mapa;

            if (mapa.Cantidad == 0)
            {
                Escribir("No hay ciudades registradas.");
                return;
            }

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("LISTA DE ADYACENCIA (ciudad -> rutas de salida)");

            for (int i = 0; i < mapa.Cantidad; i++)
            {
                string linea = mapa.ObtenerNombre(i) + " -> ";
                Arista? arista = mapa.PrimeraArista(i);

                if (arista == null)
                {
                    linea += "(sin rutas de salida)";
                }

                while (arista != null)
                {
                    linea += mapa.ObtenerNombre(arista.Destino) + " (Q" + arista.Costo.ToString("0.00") +
                             ", " + arista.Tiempo.ToString("0.0") + " h)";
                    arista = arista.Siguiente;
                    if (arista != null)
                    {
                        linea += ", ";
                    }
                }

                texto.AppendLine(linea);
            }

            Escribir(texto.ToString());
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            panelMapa.Limpiar();
            Escribir("");
        }

        private void chkPesos_CheckedChanged(object? sender, EventArgs e)
        {
            panelMapa.MostrarPesos(chkPesos.Checked);
        }
    }
}
