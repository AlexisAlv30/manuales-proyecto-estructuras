using System;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: RASTREAR ENVÍO (flujo integrado)
    // Responsable: Johann
    //
    // Muestra paso a paso cómo trabajan juntas las cuatro estructuras:
    //   1. Trie       -> sugiere el código de rastreo mientras se escribe
    //   2. Tabla Hash -> localiza el envío completo por su código
    //   3. Union-Find -> valida que origen y destino estén en la misma región
    //   4. Grafo      -> Dijkstra calcula la ruta más económica y la más rápida
    public partial class PestanaRastreo : PestanaBase
    {
        public PestanaRastreo()
        {
            InitializeComponent();
            autoCodigo.AsignarProveedor(SugerirCodigos);
            autoCodigo.Confirmado += autoCodigo_Confirmado;
        }

        public override void Refrescar()
        {
            panelMapa.AsignarSistema(sistema);
            LimpiarPasos();
        }

        private void LimpiarPasos()
        {
            lblPaso1.Text = "Mientras escribe, el Trie de códigos sugiere los envíos que empiezan así.";
            lblPaso1.ForeColor = Paleta.TextoSecundario;
            txtEnvio.Text = "";
            lblPaso3.Text = "";
            txtRutas.Text = "";
            panelMapa.Limpiar();
        }

        private void autoCodigo_Confirmado(object? sender, EventArgs e)
        {
            Rastrear();
        }

        private void btnRastrear_Click(object? sender, EventArgs e)
        {
            Rastrear();
        }

        private void Rastrear()
        {
            string codigo = autoCodigo.Texto.ToUpper();
            LimpiarPasos();

            if (codigo == "")
            {
                MarcarPaso(lblPaso1, "Escriba un código de rastreo.", false);
                return;
            }

            // Paso 1: Trie
            string[] sugerencias = SugerirCodigos(codigo);
            MarcarPaso(lblPaso1, "Trie: " + sugerencias.Length + " código(s) empiezan con \"" + codigo + "\".",
                       sugerencias.Length > 0);

            // Paso 2: Tabla Hash
            Envio? envio = sistema.Envios.Buscar(codigo);
            if (envio == null)
            {
                txtEnvio.Text = "✖ No existe un envío con el código " + codigo + "." +
                                (sugerencias.Length > 0 ? " Elija una de las sugerencias del Trie." : "");
                return;
            }

            StringBuilder datos = new StringBuilder();
            datos.AppendLine("✔ Envío encontrado en la posición " + sistema.Envios.BuscarPosicion(codigo) +
                             " de la tabla hash");
            datos.AppendLine();
            datos.AppendLine("Código: " + envio.CodigoRastreo);
            datos.AppendLine("Remitente: " + envio.Remitente);
            datos.AppendLine("Destinatario: " + envio.Destinatario);
            datos.AppendLine("Ciudad de origen: " + envio.CiudadOrigen);
            datos.Append("Ciudad de destino: " + envio.CiudadDestino);
            txtEnvio.Text = datos.ToString();

            // Paso 3: Union-Find (CalcularRuta lo consulta antes de ejecutar Dijkstra)
            int origen = sistema.Mapa.BuscarIndice(envio.CiudadOrigen);
            int destino = sistema.Mapa.BuscarIndice(envio.CiudadDestino);
            ResultadoRuta economica = sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, true);

            if (!economica.MismaRegion)
            {
                MarcarPaso(lblPaso3, "✖ " + economica.Mensaje, false);
                txtRutas.Text = "Dijkstra no se ejecuta porque no hay ninguna ruta entre las dos regiones.";
                panelMapa.MarcarExtremos(origen, destino);
                return;
            }

            string representante = sistema.Mapa.ObtenerNombre(sistema.Regiones.Find(origen));
            MarcarPaso(lblPaso3, "✔ " + envio.CiudadOrigen + " y " + envio.CiudadDestino +
                                 " están en la misma región (representante: " + representante +
                                 "). Se puede ejecutar Dijkstra.", true);

            // Paso 4: Dijkstra por costo y por tiempo
            ResultadoRuta rapida = sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, false);

            if (economica.Camino == null || rapida.Camino == null)
            {
                txtRutas.Text = "✖ " + economica.Mensaje;
                panelMapa.MarcarExtremos(origen, destino);
                return;
            }

            StringBuilder rutas = new StringBuilder();
            rutas.AppendLine("■ Ruta más económica (línea roja)");
            rutas.AppendLine(sistema.TextoCamino(economica.Camino));
            rutas.AppendLine("Costo: Q" + economica.CostoTotal.ToString("0.00") +
                             "    Tiempo: " + economica.TiempoTotal.ToString("0.0") + " h");
            rutas.AppendLine();
            rutas.AppendLine("■ Ruta más rápida (línea morada punteada)");
            rutas.AppendLine(sistema.TextoCamino(rapida.Camino));
            rutas.AppendLine("Costo: Q" + rapida.CostoTotal.ToString("0.00") +
                             "    Tiempo: " + rapida.TiempoTotal.ToString("0.0") + " h");

            if (MismoCamino(economica.Camino, rapida.Camino))
            {
                rutas.AppendLine();
                rutas.Append("En este envío las dos rutas coinciden.");
            }

            txtRutas.Text = rutas.ToString();
            panelMapa.ResaltarCaminos(economica.Camino, "Ruta más económica",
                                      rapida.Camino, "Ruta más rápida");
        }

        private void MarcarPaso(Label etiqueta, string texto, bool correcto)
        {
            etiqueta.Text = texto;
            etiqueta.ForeColor = correcto ? Paleta.Exito : Paleta.Error;
        }

        private bool MismoCamino(int[] a, int[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
