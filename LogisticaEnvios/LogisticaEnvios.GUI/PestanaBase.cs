using System;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // CLASE BASE DE LAS PESTAÑAS
    // Responsable: Johann
    //
    // Todas las pestañas trabajan sobre el MISMO SistemaLogistica, así lo que se
    // registra en una pestaña aparece en las demás. Cuando una pestaña cambia los
    // datos avisa con DatosCambiados y la ventana principal refresca todas.
    public class PestanaBase : UserControl
    {
        protected SistemaLogistica sistema = new SistemaLogistica();

        public event EventHandler? DatosCambiados;

        public virtual void Inicializar(SistemaLogistica sistema)
        {
            this.sistema = sistema;
            Refrescar();
        }

        // Vuelve a leer las estructuras y actualiza lo que se ve en pantalla
        public virtual void Refrescar()
        {
        }

        protected void AvisarCambio()
        {
            DatosCambiados?.Invoke(this, EventArgs.Empty);
        }

        // Consultas al Trie que usan los cuadros de autocompletado
        protected string[] SugerirCiudades(string prefijo)
        {
            return sistema.TrieCiudades.SugerirPorPrefijo(prefijo).ConvertirArreglo();
        }

        protected string[] SugerirCodigos(string prefijo)
        {
            return sistema.TrieCodigos.SugerirPorPrefijo(prefijo).ConvertirArreglo();
        }
    }
}
