using System.Drawing;

namespace LogisticaEnvios.GUI
{
    // COLORES DE LA INTERFAZ
    // Responsable: Johann
    //
    // Se comparten entre todas las pestañas para que una región tenga el mismo
    // color en el mapa y en las tablas, y los caminos se distingan siempre igual.
    public static class Paleta
    {
        private static readonly Color[] coloresRegion =
        {
            Color.FromArgb(37, 99, 235),    // azul
            Color.FromArgb(22, 163, 74),    // verde
            Color.FromArgb(217, 119, 6),    // ámbar
            Color.FromArgb(13, 148, 136),   // turquesa
            Color.FromArgb(219, 39, 119),   // rosa
            Color.FromArgb(120, 85, 60),    // café
            Color.FromArgb(71, 85, 105),    // gris azulado
            Color.FromArgb(101, 163, 13)    // lima
        };

        // Caminos de Dijkstra
        public static readonly Color RutaPrincipal = Color.FromArgb(220, 38, 38);
        public static readonly Color RutaSecundaria = Color.FromArgb(124, 58, 237);

        // Anillos de origen y destino
        public static readonly Color Origen = Color.FromArgb(21, 128, 61);
        public static readonly Color Destino = Color.FromArgb(185, 28, 28);

        public static readonly Color Arista = Color.FromArgb(148, 163, 184);
        public static readonly Color Texto = Color.FromArgb(30, 41, 59);
        public static readonly Color TextoSecundario = Color.FromArgb(100, 116, 139);
        public static readonly Color Exito = Color.FromArgb(21, 128, 61);
        public static readonly Color Error = Color.FromArgb(185, 28, 28);

        // Estados de las posiciones de la tabla hash
        public static readonly Color CeldaOcupada = Color.FromArgb(220, 252, 231);
        public static readonly Color CeldaEliminada = Color.FromArgb(254, 226, 226);
        public static readonly Color CeldaVacia = Color.FromArgb(248, 250, 252);
        public static readonly Color CeldaResaltada = Color.FromArgb(254, 240, 138);

        // La región 1 usa el primer color, la 2 el segundo, y así sucesivamente
        public static Color ColorRegion(int numeroRegion)
        {
            if (numeroRegion <= 0)
            {
                return Color.Gray;
            }

            return coloresRegion[(numeroRegion - 1) % coloresRegion.Length];
        }

        // Mezcla un color con blanco (0 = el mismo color, 1 = blanco)
        public static Color Aclarar(Color color, float cantidad)
        {
            int r = (int)(color.R + (255 - color.R) * cantidad);
            int g = (int)(color.G + (255 - color.G) * cantidad);
            int b = (int)(color.B + (255 - color.B) * cantidad);
            return Color.FromArgb(r, g, b);
        }
    }
}
