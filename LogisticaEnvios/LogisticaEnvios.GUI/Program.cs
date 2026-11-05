using System;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // PROYECTO 2 - ESTRUCTURA DE DATOS II
    // Escenario: Logística de envíos
    // Interfaz gráfica en Windows Forms
    // Responsable: Johann
    //
    // La interfaz no tiene lógica propia de estructuras: usa el mismo
    // SistemaLogistica de la biblioteca LogisticaEnvios.Core que la consola.
    // Los controles de WinForms (tablas, listas, árbol) solo muestran los datos
    // que devuelven la Tabla Hash, el Grafo, el Union-Find y el Trie.
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }
    }
}
