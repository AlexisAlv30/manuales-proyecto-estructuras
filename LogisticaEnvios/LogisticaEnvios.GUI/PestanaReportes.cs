using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: REPORTES PARA TOMA DE DECISIONES
    // Responsable: Johann
    //
    // Muestra los reportes que arma GeneradorReportes (biblioteca Core) en una
    // tabla, con su resumen y recomendaciones, y permite exportarlos a .csv o .txt.
    public partial class PestanaReportes : PestanaBase
    {
        private Reporte? reporteActual;

        public PestanaReportes()
        {
            InitializeComponent();

            foreach (string nombre in GeneradorReportes.NombresReportes())
            {
                lstReportes.Items.Add(nombre);
            }

            dgvReporte.DefaultCellStyle.SelectionBackColor = Paleta.CeldaResaltada;
            dgvReporte.DefaultCellStyle.SelectionForeColor = Paleta.Texto;
        }

        public override void Refrescar()
        {
            if (lstReportes.SelectedIndex == -1 && lstReportes.Items.Count > 0)
            {
                // Al elegir el primero se dispara SelectedIndexChanged y se muestra
                lstReportes.SelectedIndex = 0;
                return;
            }

            MostrarReporte();
        }

        // Genera el reporte elegido con los datos actuales y lo muestra
        private void MostrarReporte()
        {
            int numero = Math.Max(0, lstReportes.SelectedIndex);
            reporteActual = new GeneradorReportes(sistema).Generar(numero);

            lblTitulo.Text = reporteActual.Titulo;
            lblDescripcion.Text = reporteActual.Descripcion;

            dgvReporte.Rows.Clear();
            dgvReporte.Columns.Clear();

            foreach (string columna in reporteActual.Columnas)
            {
                int indice = dgvReporte.Columns.Add("col" + dgvReporte.Columns.Count, columna);
                dgvReporte.Columns[indice].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (string[] fila in reporteActual.Filas.ConvertirArreglo())
            {
                int indice = dgvReporte.Rows.Add(fila);

                // Los envíos que no se pueden despachar se marcan en rojo claro
                // (en el reporte 1 los que dicen "Sin ruta"; en el reporte 2, todos)
                if ((numero == 0 && fila[3].StartsWith("Sin ruta")) || numero == 1)
                {
                    dgvReporte.Rows[indice].DefaultCellStyle.BackColor = Paleta.CeldaEliminada;
                }
            }

            dgvReporte.ClearSelection();
            dgvReporte.CurrentCell = null;

            StringBuilder resumen = new StringBuilder();
            foreach (string linea in reporteActual.Resumen.ConvertirArreglo())
            {
                resumen.AppendLine("• " + linea);
            }
            txtResumen.Text = resumen.ToString();
        }

        private void lstReportes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            MostrarReporte();
        }

        private void btnActualizar_Click(object? sender, EventArgs e)
        {
            MostrarReporte();
        }

        private void btnExportarCsv_Click(object? sender, EventArgs e)
        {
            if (reporteActual == null)
            {
                return;
            }

            int numero = Math.Max(0, lstReportes.SelectedIndex);

            using SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Title = "Exportar reporte a .csv";
            dialogo.Filter = "Archivos CSV (*.csv)|*.csv";
            dialogo.FileName = GeneradorReportes.NombresArchivo()[numero];
            dialogo.InitialDirectory = CarpetaReportes();

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            bool exportado = GeneradorReportes.ExportarCsv(reporteActual, dialogo.FileName, out string mensaje);
            MessageBox.Show(this, mensaje, "Exportar reporte", MessageBoxButtons.OK,
                            exportado ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void btnExportarTxt_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Title = "Exportar todos los reportes a .txt";
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt";
            dialogo.FileName = "reportes.txt";
            dialogo.InitialDirectory = CarpetaReportes();

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            Reporte[] reportes = new GeneradorReportes(sistema).GenerarTodos();
            bool exportado = GeneradorReportes.ExportarTxt(reportes, dialogo.FileName, out string mensaje);
            MessageBox.Show(this, mensaje, "Exportar reportes", MessageBoxButtons.OK,
                            exportado ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        // Carpeta reportes del proyecto; se crea si todavía no existe
        private string CarpetaReportes()
        {
            string carpeta = SistemaLogistica.CarpetaReportesPorDefecto();

            try
            {
                Directory.CreateDirectory(carpeta);
            }
            catch (Exception)
            {
                // Si no se puede crear, el diálogo abre en su carpeta habitual
            }

            return carpeta;
        }
    }
}
