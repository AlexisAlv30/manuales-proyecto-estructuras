using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // VENTANA PRINCIPAL
    // Responsable: Johann
    //
    // Contiene el menú Archivo (cargar y guardar los .csv), la barra de estado y
    // una pestaña por cada parte del sistema. Todas las pestañas comparten el
    // mismo SistemaLogistica; cuando una cambia los datos, se refrescan todas.
    public partial class FormPrincipal : Form
    {
        private SistemaLogistica sistema = new SistemaLogistica();
        private readonly PestanaBase[] todasLasPestanas;

        // Carpeta datos del proyecto (ahí se leen y se guardan los .csv)
        private readonly string carpetaPorDefecto = SistemaLogistica.CarpetaDatosPorDefecto();

        public FormPrincipal()
        {
            InitializeComponent();

            todasLasPestanas = new PestanaBase[]
            {
                pestanaRastreo, pestanaEnvios, pestanaGrafo, pestanaRutas, pestanaRegiones, pestanaTrie,
                pestanaReportes
            };

            foreach (PestanaBase pestana in todasLasPestanas)
            {
                pestana.DatosCambiados += Pestana_DatosCambiados;
            }

            InicializarPestanas();
            lblMensaje.Text = "Para empezar use Archivo → Cargar datos predeterminados (Ctrl+D).";
        }

        private void InicializarPestanas()
        {
            foreach (PestanaBase pestana in todasLasPestanas)
            {
                pestana.Inicializar(sistema);
            }
            ActualizarResumen();
        }

        private void RefrescarTodo()
        {
            foreach (PestanaBase pestana in todasLasPestanas)
            {
                pestana.Refrescar();
            }
            ActualizarResumen();
        }

        private void Pestana_DatosCambiados(object? sender, EventArgs e)
        {
            RefrescarTodo();
        }

        private void ActualizarResumen()
        {
            if (sistema.Mapa.Cantidad > 0 && (lblMensaje.Text ?? "").StartsWith("Para empezar"))
            {
                lblMensaje.Text = "";
            }

            lblResumen.Text = "Ciudades: " + sistema.Mapa.Cantidad +
                              "   |   Envíos: " + sistema.Envios.Cantidad +
                              "   |   Regiones: " + sistema.Regiones.ContarRegiones();
        }

        // CARGA DE ARCHIVOS

        // Si ya hay datos, pregunta si se reemplazan o se agregan.
        // Devuelve false si el usuario cancela.
        private bool PrepararCarga()
        {
            if (sistema.Mapa.Cantidad == 0)
            {
                return true;
            }

            DialogResult respuesta = MessageBox.Show(this,
                "Ya hay datos cargados.\n\nSí: vaciar todo y cargar de nuevo.\nNo: agregar a los datos actuales.",
                "Cargar datos", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Cancel)
            {
                return false;
            }

            if (respuesta == DialogResult.Yes)
            {
                sistema = new SistemaLogistica();
                InicializarPestanas();
            }

            return true;
        }

        private void CargarCarpeta(string carpeta)
        {
            if (!Directory.Exists(carpeta))
            {
                MessageBox.Show(this, "La carpeta no existe:\n" + carpeta, "Cargar datos",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PrepararCarga())
            {
                return;
            }

            // Primero ciudades, luego rutas y por último envíos
            ResultadoCarga[]? resultados = sistema.CargarCarpeta(carpeta);
            if (resultados != null)
            {
                MostrarResultados(resultados);
            }
            RefrescarTodo();
        }

        private void CargarArchivo(string titulo, Func<string, ResultadoCarga> cargar)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = titulo;
            dialogo.Filter = "Archivos CSV (*.csv)|*.csv|Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (Directory.Exists(carpetaPorDefecto))
            {
                dialogo.InitialDirectory = carpetaPorDefecto;
            }

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            MostrarResultados(new ResultadoCarga[] { cargar(dialogo.FileName) });
            RefrescarTodo();
        }

        // Muestra cuántas líneas se cargaron de cada archivo y los avisos de error
        private void MostrarResultados(ResultadoCarga[] resultados)
        {
            const int MaximoAvisos = 15;
            StringBuilder texto = new StringBuilder();
            int totalAvisos = 0;

            foreach (ResultadoCarga resultado in resultados)
            {
                string nombre = Path.GetFileName(resultado.Archivo);
                if (resultado.ArchivoLeido)
                {
                    texto.AppendLine(nombre + ": " + resultado.Cargados + " cargados, " +
                                     resultado.Errores + " líneas con error");
                }
                else
                {
                    texto.AppendLine(nombre + ": no se pudo leer");
                }
            }

            foreach (ResultadoCarga resultado in resultados)
            {
                foreach (string aviso in resultado.Mensajes.ConvertirArreglo())
                {
                    if (totalAvisos == 0)
                    {
                        texto.AppendLine();
                        texto.AppendLine("Detalle:");
                    }
                    if (totalAvisos < MaximoAvisos)
                    {
                        texto.AppendLine("• " + Path.GetFileName(resultado.Archivo) + " – " + aviso);
                    }
                    totalAvisos++;
                }
            }

            if (totalAvisos > MaximoAvisos)
            {
                texto.AppendLine("… y " + (totalAvisos - MaximoAvisos) + " avisos más.");
            }

            MessageBox.Show(this, texto.ToString(), "Resultado de la carga", MessageBoxButtons.OK,
                            totalAvisos > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            lblMensaje.Text = totalAvisos > 0 ? "Datos cargados con avisos." : "Datos cargados correctamente.";
        }

        // MENÚ ARCHIVO

        private void menuCargarEjemplo_Click(object? sender, EventArgs e)
        {
            CargarCarpeta(carpetaPorDefecto);
        }

        private void menuCargarCarpeta_Click(object? sender, EventArgs e)
        {
            using FolderBrowserDialog dialogo = new FolderBrowserDialog();
            dialogo.Description = "Seleccione la carpeta que contiene ciudades.csv, rutas.csv y envios.csv";
            dialogo.UseDescriptionForTitle = true;
            if (Directory.Exists(carpetaPorDefecto))
            {
                dialogo.InitialDirectory = carpetaPorDefecto;
            }

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                CargarCarpeta(dialogo.SelectedPath);
            }
        }

        private void menuCargarCiudades_Click(object? sender, EventArgs e)
        {
            CargarArchivo("Archivo de ciudades (nombre,latitud,longitud)", sistema.CargarCiudades);
        }

        private void menuCargarRutas_Click(object? sender, EventArgs e)
        {
            CargarArchivo("Archivo de rutas (origen,destino,costo,tiempo,sentido)", sistema.CargarRutas);
        }

        private void menuCargarEnvios_Click(object? sender, EventArgs e)
        {
            CargarArchivo("Archivo de envíos (codigo,remitente,destinatario,origen,destino)", sistema.CargarEnvios);
        }

        // Guarda en la carpeta datos del proyecto (la misma que se carga al iniciar)
        private void menuGuardar_Click(object? sender, EventArgs e)
        {
            Guardar(carpetaPorDefecto);
        }

        private void menuGuardarComo_Click(object? sender, EventArgs e)
        {
            using FolderBrowserDialog dialogo = new FolderBrowserDialog();
            dialogo.Description = "Seleccione la carpeta donde se guardarán ciudades.csv, rutas.csv y envios.csv";
            dialogo.UseDescriptionForTitle = true;

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                Guardar(dialogo.SelectedPath);
            }
        }

        private void Guardar(string carpeta)
        {
            bool guardado = sistema.GuardarDatos(carpeta, out string mensaje);
            MessageBox.Show(this, mensaje, "Guardar datos", MessageBoxButtons.OK,
                            guardado ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            lblMensaje.Text = mensaje;
        }

        private void menuVaciar_Click(object? sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(this,
                "¿Desea borrar todas las ciudades, rutas y envíos de la memoria?\nLos archivos .csv no se modifican.",
                "Vaciar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            sistema = new SistemaLogistica();
            InicializarPestanas();
            lblMensaje.Text = "Se vaciaron todos los datos.";
        }

        private void menuSalir_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void menuAcercaDe_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(this,
                "Proyecto 2 – Estructura de Datos II\n" +
                "Escenario: Logística de envíos\n\n" +
                "Estructuras: Tabla Hash, Grafo (BFS, DFS, Dijkstra), Union-Find y Trie.\n\n" +
                "Código de las estructuras: Ángel\n" +
                "Interfaz gráfica: Johann\n" +
                "Manual técnico y de usuario: Alexis",
                "Acerca del proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
