using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // CUADRO DE TEXTO CON AUTOCOMPLETADO
    // Responsable: Johann
    //
    // Mientras el usuario escribe, se le piden al TRIE las palabras que empiezan
    // con ese prefijo y se muestran en una lista debajo del cuadro.
    // No se usa el autocompletado que trae el TextBox (AutoCompleteCustomSource)
    // porque guarda las palabras en una colección de .NET y se saltaría el Trie.
    //
    // Teclas: flechas para moverse en la lista, Enter para elegir, Esc para cerrar.
    public class CuadroAutocompletado : UserControl
    {
        private const int MaximoVisibles = 8;

        private readonly TextBox cuadro;
        private readonly ListBox lista;

        // Función que devuelve las sugerencias para un prefijo (la consulta al Trie)
        private Func<string, string[]>? obtenerSugerencias;

        // Evita mostrar sugerencias cuando el texto se cambia desde el código
        private bool cambiandoTexto = false;

        // Se dispara cuando se elige una sugerencia o se presiona Enter
        public event EventHandler? Confirmado;

        public CuadroAutocompletado()
        {
            cuadro = new TextBox();
            cuadro.Dock = DockStyle.Fill;
            cuadro.TextChanged += Cuadro_TextChanged;
            cuadro.KeyDown += Cuadro_KeyDown;
            cuadro.LostFocus += AlPerderFoco;
            Controls.Add(cuadro);

            // La lista se agrega al formulario (no a este control) para que pueda
            // verse encima de los demás controles
            lista = new ListBox();
            lista.Visible = false;
            lista.IntegralHeight = false;
            lista.TabStop = false;
            lista.BorderStyle = BorderStyle.FixedSingle;
            lista.MouseClick += Lista_MouseClick;
            lista.LostFocus += AlPerderFoco;

            Size = new Size(200, cuadro.PreferredHeight);
        }

        // Texto escrito (sin espacios al inicio ni al final)
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Texto
        {
            get { return cuadro.Text.Trim(); }
            set
            {
                cambiandoTexto = true;
                cuadro.Text = value;
                cuadro.SelectionStart = cuadro.Text.Length;
                cambiandoTexto = false;
                Ocultar();
            }
        }

        // Texto gris que se ve cuando el cuadro está vacío
        [DefaultValue("")]
        public string TextoGuia
        {
            get { return cuadro.PlaceholderText; }
            set { cuadro.PlaceholderText = value; }
        }

        public void AsignarProveedor(Func<string, string[]> funcion)
        {
            obtenerSugerencias = funcion;
        }

        public void Enfocar()
        {
            cuadro.Focus();
        }

        private void Cuadro_TextChanged(object? sender, EventArgs e)
        {
            if (cambiandoTexto || obtenerSugerencias == null || !cuadro.Focused)
            {
                return;
            }

            string prefijo = cuadro.Text.Trim();
            if (prefijo == "")
            {
                Ocultar();
                return;
            }

            string[] sugerencias = obtenerSugerencias(prefijo);

            // Si no hay sugerencias o ya escribió la palabra completa, no se muestra la lista
            if (sugerencias.Length == 0 ||
                (sugerencias.Length == 1 && string.Equals(sugerencias[0], prefijo, StringComparison.OrdinalIgnoreCase)))
            {
                Ocultar();
                return;
            }

            Mostrar(sugerencias);
        }

        private void Mostrar(string[] sugerencias)
        {
            Form? formulario = FindForm();
            if (formulario == null)
            {
                return;
            }

            if (lista.Parent != formulario)
            {
                formulario.Controls.Add(lista);
            }

            lista.BeginUpdate();
            lista.Items.Clear();
            foreach (string sugerencia in sugerencias)
            {
                lista.Items.Add(sugerencia);
            }
            lista.EndUpdate();

            // Se ubica justo debajo del cuadro, en coordenadas del formulario
            Point esquina = formulario.PointToClient(PointToScreen(new Point(0, Height)));
            int visibles = Math.Min(sugerencias.Length, MaximoVisibles);
            lista.SetBounds(esquina.X, esquina.Y, Width, visibles * lista.ItemHeight + 4);
            lista.Visible = true;
            lista.BringToFront();
        }

        private void Ocultar()
        {
            lista.Visible = false;
        }

        private void Elegir(int indice)
        {
            Texto = lista.Items[indice]?.ToString() ?? "";
            cuadro.Focus();
            Confirmado?.Invoke(this, EventArgs.Empty);
        }

        private void Cuadro_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lista.Visible)
            {
                if (lista.SelectedIndex < lista.Items.Count - 1)
                {
                    lista.SelectedIndex++;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up && lista.Visible)
            {
                if (lista.SelectedIndex > 0)
                {
                    lista.SelectedIndex--;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (lista.Visible && lista.SelectedIndex >= 0)
                {
                    Elegir(lista.SelectedIndex);
                }
                else
                {
                    Ocultar();
                    Confirmado?.Invoke(this, EventArgs.Empty);
                }
                // Evita el sonido de Windows al presionar Enter en un TextBox
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Ocultar();
                e.Handled = true;
            }
        }

        private void Lista_MouseClick(object? sender, MouseEventArgs e)
        {
            int indice = lista.IndexFromPoint(e.Location);
            if (indice >= 0)
            {
                Elegir(indice);
            }
        }

        private void AlPerderFoco(object? sender, EventArgs e)
        {
            if (!IsHandleCreated)
            {
                return;
            }

            // Se espera a que termine el cambio de foco: si pasó del cuadro a la
            // lista (porque el usuario va a hacer clic en una sugerencia) no se oculta
            BeginInvoke(new Action(() =>
            {
                if (!cuadro.Focused && !lista.Focused)
                {
                    Ocultar();
                }
            }));
        }

        // Si la pestaña deja de verse, la lista también se oculta
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible)
            {
                Ocultar();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lista.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
