using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PESTAÑA: ENVÍOS (Tabla Hash)
    // Responsable: Johann
    //
    // Permite registrar, buscar y eliminar envíos. A la derecha se ve la tabla
    // hash completa: cada fila es una posición del arreglo y su estado
    // (ocupada, eliminada o vacía), para observar el sondeo lineal y el rehashing.
    public partial class PestanaEnvios : PestanaBase
    {
        // Código del envío que se está mostrando, para resaltarlo en la tabla
        private string codigoSeleccionado = "";

        public PestanaEnvios()
        {
            InitializeComponent();
            autoOrigen.AsignarProveedor(SugerirCiudades);
            autoDestino.AsignarProveedor(SugerirCiudades);
            autoBuscar.AsignarProveedor(SugerirCodigos);
            autoBuscar.Confirmado += autoBuscar_Confirmado;

            dgvTabla.DefaultCellStyle.SelectionBackColor = Paleta.CeldaResaltada;
            dgvTabla.DefaultCellStyle.SelectionForeColor = Paleta.Texto;
        }

        public override void Refrescar()
        {
            TablaHash tabla = sistema.Envios;

            lblResumen.Text = "Envíos: " + tabla.Cantidad +
                              "     Tamaño de la tabla: " + tabla.Tamanio +
                              "     Factor de carga: " + tabla.FactorCarga().ToString("0.00") +
                              "     Rehashing realizados: " + tabla.CantidadRehashing;

            dgvTabla.Rows.Clear();

            for (int i = 0; i < tabla.Tamanio; i++)
            {
                Envio? envio = tabla.ObtenerPosicion(i);
                int fila;

                if (envio != null)
                {
                    fila = dgvTabla.Rows.Add(i, "Ocupada", envio.CodigoRastreo, envio.Remitente,
                                             envio.Destinatario, envio.CiudadOrigen, envio.CiudadDestino);
                    dgvTabla.Rows[fila].DefaultCellStyle.BackColor = Paleta.CeldaOcupada;
                }
                else if (tabla.PosicionEliminada(i))
                {
                    fila = dgvTabla.Rows.Add(i, "Eliminada", "", "", "", "", "");
                    dgvTabla.Rows[fila].DefaultCellStyle.BackColor = Paleta.CeldaEliminada;
                }
                else
                {
                    fila = dgvTabla.Rows.Add(i, "Vacía", "", "", "", "", "");
                    dgvTabla.Rows[fila].DefaultCellStyle.BackColor = Paleta.CeldaVacia;
                    dgvTabla.Rows[fila].DefaultCellStyle.ForeColor = Paleta.TextoSecundario;
                }
            }

            SeleccionarFila(codigoSeleccionado);
        }

        // Resalta en la tabla la fila del código indicado
        private void SeleccionarFila(string codigo)
        {
            dgvTabla.ClearSelection();

            int posicion = codigo == "" ? -1 : sistema.Envios.BuscarPosicion(codigo);
            if (posicion == -1 || posicion >= dgvTabla.Rows.Count)
            {
                return;
            }

            dgvTabla.Rows[posicion].Selected = true;
            dgvTabla.FirstDisplayedScrollingRowIndex = Math.Max(0, posicion - 3);
        }

        private void Escribir(string texto)
        {
            txtDetalle.Text = texto;
        }

        private void MostrarEnvio(Envio envio)
        {
            TablaHash tabla = sistema.Envios;
            int inicial = tabla.PosicionInicial(envio.CodigoRastreo);
            int posicion = tabla.BuscarPosicion(envio.CodigoRastreo);

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("✔ Envío encontrado en la tabla hash");
            texto.AppendLine("h(" + envio.CodigoRastreo + ") = " + inicial + "  →  quedó en la posición " + posicion +
                             (posicion == inicial ? " (sin colisión)" : " (sondeo lineal por colisión)"));
            texto.AppendLine();
            texto.AppendLine("Código: " + envio.CodigoRastreo);
            texto.AppendLine("Remitente: " + envio.Remitente);
            texto.AppendLine("Destinatario: " + envio.Destinatario);
            texto.AppendLine("Ciudad de origen: " + envio.CiudadOrigen);
            texto.Append("Ciudad de destino: " + envio.CiudadDestino);
            Escribir(texto.ToString());

            codigoSeleccionado = envio.CodigoRastreo;
            SeleccionarFila(codigoSeleccionado);
        }

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            bool registrado = sistema.RegistrarEnvio(txtCodigo.Text, txtRemitente.Text, txtDestinatario.Text,
                                                     autoOrigen.Texto, autoDestino.Texto, out string mensaje);

            if (!registrado)
            {
                Escribir("✖ " + mensaje);
                return;
            }

            codigoSeleccionado = txtCodigo.Text.Trim().ToUpper();
            Escribir("✔ " + mensaje);

            txtCodigo.Text = "";
            txtRemitente.Text = "";
            txtDestinatario.Text = "";
            autoOrigen.Texto = "";
            autoDestino.Texto = "";

            AvisarCambio();
        }

        private void autoBuscar_Confirmado(object? sender, EventArgs e)
        {
            Buscar();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            Buscar();
        }

        // El Trie sugiere el código y la tabla hash localiza el envío
        private void Buscar()
        {
            string codigo = autoBuscar.Texto.ToUpper();

            if (codigo == "")
            {
                Escribir("Escriba un código de rastreo o su inicio.");
                return;
            }

            Envio? envio = sistema.Envios.Buscar(codigo);

            if (envio == null)
            {
                string[] sugerencias = SugerirCodigos(codigo);
                codigoSeleccionado = "";
                SeleccionarFila("");

                if (sugerencias.Length > 0)
                {
                    Escribir("No existe exactamente el código " + codigo + ".\r\n\r\nEl Trie sugiere: " +
                             string.Join(", ", sugerencias));
                }
                else
                {
                    Escribir("✖ Envío no encontrado.");
                }
                return;
            }

            MostrarEnvio(envio);
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            string codigo = autoBuscar.Texto.ToUpper();

            if (sistema.Envios.Buscar(codigo) == null)
            {
                Escribir("✖ Escriba el código completo del envío que desea eliminar (puede elegirlo de las sugerencias).");
                return;
            }

            DialogResult respuesta = MessageBox.Show(this, "¿Seguro que desea eliminar el envío " + codigo + "?",
                                                     "Eliminar envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            int posicion = sistema.Envios.BuscarPosicion(codigo);
            sistema.EliminarEnvio(codigo);

            Escribir("✔ Envío " + codigo + " eliminado.\r\n\r\nLa posición " + posicion +
                     " queda marcada como ELIMINADA para no cortar el sondeo lineal de otros códigos." +
                     " También se quitó el código del Trie.");

            codigoSeleccionado = "";
            autoBuscar.Texto = "";
            AvisarCambio();
        }

        // Al hacer clic en una fila ocupada se muestran los datos de ese envío
        private void dgvTabla_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string codigo = dgvTabla.Rows[e.RowIndex].Cells[colCodigo.Index].Value?.ToString() ?? "";
            Envio? envio = codigo == "" ? null : sistema.Envios.Buscar(codigo);

            if (envio != null)
            {
                autoBuscar.Texto = codigo;
                MostrarEnvio(envio);
            }
        }
    }
}
