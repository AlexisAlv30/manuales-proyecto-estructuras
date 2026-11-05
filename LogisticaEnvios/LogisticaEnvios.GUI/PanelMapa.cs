using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LogisticaEnvios.GUI
{
    // MAPA DEL GRAFO
    // Responsable: Johann
    //
    // Control que dibuja el grafo de ciudades:
    // - Si las ciudades tienen latitud y longitud se ubican como en el mapa;
    //   si ninguna tiene, se acomodan en círculo.
    // - Cada ciudad se pinta con el color de su región de distribución (Union-Find).
    // - Las rutas de doble sentido son líneas; las de un solo sentido llevan flecha.
    // - Puede resaltar uno o dos caminos de Dijkstra y numerar el orden de un
    //   recorrido BFS o DFS.
    public class PanelMapa : Control
    {
        private const float Radio = 9f;
        private const float DistanciaMinima = 46f;
        private const float Margen = 45f;
        private const float AltoLineaLeyenda = 17f;

        private SistemaLogistica? sistema;
        private int[]? caminoPrincipal;
        private int[]? caminoSecundario;
        private string textoPrincipal = "";
        private string textoSecundario = "";
        private int[]? ordenRecorrido;
        private int ciudadOrigen = -1;
        private int ciudadDestino = -1;
        private bool mostrarPesos = true;

        // Posición en pantalla de cada ciudad; se recalcula cada vez que se dibuja
        private PointF[] posiciones = new PointF[0];

        private readonly Font fuenteCiudad = new Font("Segoe UI", 8.5f);
        private readonly Font fuentePeso = new Font("Segoe UI", 7f);
        private readonly Font fuenteNumero = new Font("Segoe UI", 7f, FontStyle.Bold);
        private readonly Font fuenteLeyenda = new Font("Segoe UI", 8f);

        // Se dispara cuando el usuario hace clic sobre una ciudad (envía su nombre)
        public event EventHandler<string>? CiudadClic;

        public PanelMapa()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = Color.White;
        }

        public void AsignarSistema(SistemaLogistica sistema)
        {
            this.sistema = sistema;
            Limpiar();
        }

        // Quita los caminos, el recorrido y las ciudades marcadas
        public void Limpiar()
        {
            caminoPrincipal = null;
            caminoSecundario = null;
            ordenRecorrido = null;
            ciudadOrigen = -1;
            ciudadDestino = -1;
            Invalidate();
        }

        // Marca el origen y el destino sin dibujar ningún camino (-1 = sin marcar)
        public void MarcarExtremos(int origen, int destino)
        {
            Limpiar();
            ciudadOrigen = origen;
            ciudadDestino = destino;
        }

        // Resalta uno o dos caminos (por ejemplo, la ruta más económica y la más rápida)
        public void ResaltarCaminos(int[]? principal, string textoPrincipal,
                                    int[]? secundario, string textoSecundario)
        {
            Limpiar();
            caminoPrincipal = principal;
            caminoSecundario = secundario;
            this.textoPrincipal = textoPrincipal;
            this.textoSecundario = textoSecundario;

            int[]? referencia = principal ?? secundario;
            if (referencia != null && referencia.Length > 0)
            {
                ciudadOrigen = referencia[0];
                ciudadDestino = referencia[referencia.Length - 1];
            }
        }

        // Numera las ciudades en el orden en que las visitó el recorrido
        public void ResaltarRecorrido(int[] orden)
        {
            Limpiar();
            ordenRecorrido = orden;
            if (orden.Length > 0)
            {
                ciudadOrigen = orden[0];
            }
        }

        public void MostrarPesos(bool mostrar)
        {
            mostrarPesos = mostrar;
            Invalidate();
        }

        // DIBUJO

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (sistema == null || sistema.Mapa.Cantidad == 0)
            {
                posiciones = new PointF[0];
                DibujarMensajeVacio(g);
                return;
            }

            CalcularPosiciones();
            int n = posiciones.Length;
            int[] regiones = sistema.Regiones.NumerarRegiones();

            // Número de visita de cada ciudad en el recorrido (0 = no se visitó)
            int[] numeroVisita = new int[n];
            if (ordenRecorrido != null)
            {
                for (int k = 0; k < ordenRecorrido.Length; k++)
                {
                    numeroVisita[ordenRecorrido[k]] = k + 1;
                }
            }

            // Primero se reserva el lugar de la leyenda, de cada ciudad, de su número
            // de visita y de su nombre; así los costos de las rutas no los tapan
            Espacios ocupados = new Espacios();
            ocupados.Agregar(RectanguloLeyenda());

            for (int i = 0; i < n; i++)
            {
                ocupados.Agregar(new RectangleF(posiciones[i].X - Radio - 2, posiciones[i].Y - Radio - 2,
                                                2 * Radio + 4, 2 * Radio + 4));
                if (numeroVisita[i] > 0)
                {
                    ocupados.Agregar(RectanguloNumero(posiciones[i]));
                }
            }

            RectangleF[] etiquetas = new RectangleF[n];
            for (int i = 0; i < n; i++)
            {
                SizeF tamanio = g.MeasureString(sistema.Mapa.ObtenerNombre(i), fuenteCiudad);
                etiquetas[i] = UbicarEtiqueta(posiciones[i], tamanio, ocupados);
                ocupados.Agregar(etiquetas[i]);
            }

            DibujarRutas(g, ocupados);

            // El segundo camino se corre un poco para que se vea aunque coincida con el primero
            if (caminoSecundario != null)
            {
                DibujarCamino(g, caminoSecundario, Paleta.RutaSecundaria, true, caminoPrincipal != null ? 5f : 0f);
            }
            if (caminoPrincipal != null)
            {
                DibujarCamino(g, caminoPrincipal, Paleta.RutaPrincipal, false, 0f);
            }

            DibujarCiudades(g, regiones, numeroVisita, etiquetas);
            DibujarLeyenda(g, regiones);
        }

        // Rectángulos que ya están ocupados en el dibujo (ciudades, nombres, costos)
        private class Espacios
        {
            private RectangleF[] rectangulos = new RectangleF[32];
            private int cantidad = 0;

            public void Agregar(RectangleF rectangulo)
            {
                if (cantidad == rectangulos.Length)
                {
                    RectangleF[] nuevos = new RectangleF[rectangulos.Length * 2];
                    for (int i = 0; i < cantidad; i++)
                    {
                        nuevos[i] = rectangulos[i];
                    }
                    rectangulos = nuevos;
                }

                rectangulos[cantidad] = rectangulo;
                cantidad++;
            }

            public bool Choca(RectangleF rectangulo)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    if (rectangulos[i].IntersectsWith(rectangulo))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private void DibujarMensajeVacio(Graphics g)
        {
            using StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            using SolidBrush brocha = new SolidBrush(Paleta.TextoSecundario);

            g.DrawString("No hay ciudades registradas.\nUse Archivo → Cargar datos predeterminados (Ctrl+D) para comenzar.",
                         fuenteLeyenda, brocha, ClientRectangle, formato);
        }

        // Convierte latitud y longitud en una posición dentro del control
        private void CalcularPosiciones()
        {
            Grafo mapa = sistema!.Mapa;
            int n = mapa.Cantidad;
            posiciones = new PointF[n];

            // Rango de coordenadas de las ciudades que sí las tienen
            double minLat = double.MaxValue, maxLat = double.MinValue;
            double minLon = double.MaxValue, maxLon = double.MinValue;
            int conCoordenadas = 0;

            for (int i = 0; i < n; i++)
            {
                if (!mapa.TieneCoordenadas(i))
                {
                    continue;
                }

                conCoordenadas++;
                minLat = Math.Min(minLat, mapa.ObtenerLatitud(i));
                maxLat = Math.Max(maxLat, mapa.ObtenerLatitud(i));
                minLon = Math.Min(minLon, mapa.ObtenerLongitud(i));
                maxLon = Math.Max(maxLon, mapa.ObtenerLongitud(i));
            }

            float ancho = Math.Max(Width - 2 * Margen, 10);
            float alto = Math.Max(Height - 2 * Margen, 10);

            if (conCoordenadas == 0)
            {
                // Ninguna ciudad tiene coordenadas: se acomodan en círculo
                float centroX = Width / 2f;
                float centroY = Height / 2f;
                float radioCirculo = Math.Min(ancho, alto) / 2f;

                for (int i = 0; i < n; i++)
                {
                    double angulo = 2 * Math.PI * i / n - Math.PI / 2;
                    posiciones[i] = new PointF(centroX + radioCirculo * (float)Math.Cos(angulo),
                                               centroY + radioCirculo * (float)Math.Sin(angulo));
                }
            }
            else
            {
                int sinCoordenadas = n - conCoordenadas;

                // Si hay ciudades sin coordenadas se deja una franja abajo para ellas
                float altoMapa = sinCoordenadas > 0 ? alto - 40 : alto;

                double rangoLon = Math.Max(maxLon - minLon, 0.01);
                double rangoLat = Math.Max(maxLat - minLat, 0.01);

                // Cada eje tiene su escala para aprovechar el espacio,
                // pero sin estirar el mapa más del doble en ningún sentido
                double escalaX = ancho / rangoLon;
                double escalaY = altoMapa / rangoLat;
                escalaX = Math.Min(escalaX, escalaY * 2);
                escalaY = Math.Min(escalaY, escalaX * 2);

                float inicioX = Margen + (float)(ancho - rangoLon * escalaX) / 2;
                float inicioY = Margen + (float)(altoMapa - rangoLat * escalaY) / 2;
                int numeroSinCoordenadas = 0;

                for (int i = 0; i < n; i++)
                {
                    if (mapa.TieneCoordenadas(i))
                    {
                        float x = inicioX + (float)((mapa.ObtenerLongitud(i) - minLon) * escalaX);
                        // La latitud crece hacia el norte y la pantalla crece hacia abajo
                        float y = inicioY + (float)((maxLat - mapa.ObtenerLatitud(i)) * escalaY);
                        posiciones[i] = new PointF(x, y);
                    }
                    else
                    {
                        numeroSinCoordenadas++;
                        float x = Margen + ancho * numeroSinCoordenadas / (sinCoordenadas + 1);
                        posiciones[i] = new PointF(x, Height - 30);
                    }
                }
            }

            SepararCiudades();
        }

        // Si dos ciudades quedan muy juntas (por ejemplo Flores y San Benito)
        // se empujan un poco en sentidos opuestos para que no se encimen.
        private void SepararCiudades()
        {
            int n = posiciones.Length;

            for (int vuelta = 0; vuelta < 60; vuelta++)
            {
                bool huboCambio = false;

                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        float dx = posiciones[j].X - posiciones[i].X;
                        float dy = posiciones[j].Y - posiciones[i].Y;
                        float distancia = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distancia >= DistanciaMinima)
                        {
                            continue;
                        }

                        // Dos ciudades en el mismo punto: se elige una dirección cualquiera
                        if (distancia < 0.01f)
                        {
                            dx = (float)Math.Cos(j);
                            dy = (float)Math.Sin(j);
                            distancia = 1;
                        }

                        float empuje = (DistanciaMinima - distancia) / 2;
                        float ex = dx / distancia * empuje;
                        float ey = dy / distancia * empuje;

                        posiciones[i] = new PointF(posiciones[i].X - ex, posiciones[i].Y - ey);
                        posiciones[j] = new PointF(posiciones[j].X + ex, posiciones[j].Y + ey);
                        huboCambio = true;
                    }
                }

                if (!huboCambio)
                {
                    break;
                }
            }

            // Ninguna ciudad debe quedar fuera del control
            for (int i = 0; i < n; i++)
            {
                float x = Math.Max(Radio + 4, Math.Min(Width - Radio - 4, posiciones[i].X));
                float y = Math.Max(Radio + 4, Math.Min(Height - Radio - 4, posiciones[i].Y));
                posiciones[i] = new PointF(x, y);
            }
        }

        private void DibujarRutas(Graphics g, Espacios ocupados)
        {
            Grafo mapa = sistema!.Mapa;

            using Pen lapizDoble = new Pen(Paleta.Arista, 1.6f);
            using Pen lapizSentido = new Pen(Paleta.Arista, 1.6f);
            using AdjustableArrowCap flecha = new AdjustableArrowCap(4f, 5f);
            lapizSentido.CustomEndCap = flecha;

            for (int i = 0; i < mapa.Cantidad; i++)
            {
                Arista? arista = mapa.PrimeraArista(i);

                while (arista != null)
                {
                    int j = arista.Destino;
                    Arista? vuelta = mapa.BuscarArista(j, i);

                    // Igual que al guardar: es doble si la vuelta tiene los mismos valores
                    bool esDoble = vuelta != null &&
                                   vuelta.Costo == arista.Costo &&
                                   vuelta.Tiempo == arista.Tiempo;

                    if (esDoble)
                    {
                        // La ruta doble se dibuja una sola vez
                        if (i < j)
                        {
                            DibujarLinea(g, posiciones[i], posiciones[j], lapizDoble, 0f);
                            if (mostrarPesos)
                            {
                                DibujarPeso(g, posiciones[i], posiciones[j], arista, 0f, ocupados);
                            }
                        }
                    }
                    else
                    {
                        // Si existe la vuelta con otros valores, cada sentido va un poco separado
                        float separacion = vuelta != null ? 4f : 0f;
                        DibujarLinea(g, posiciones[i], posiciones[j], lapizSentido, separacion);
                        if (mostrarPesos)
                        {
                            DibujarPeso(g, posiciones[i], posiciones[j], arista, separacion * 3, ocupados);
                        }
                    }

                    arista = arista.Siguiente;
                }
            }
        }

        // Dibuja una línea entre dos ciudades sin tapar los círculos.
        // desplazamiento mueve la línea hacia un lado (perpendicular a la dirección).
        private void DibujarLinea(Graphics g, PointF a, PointF b, Pen lapiz, float desplazamiento)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float largo = (float)Math.Sqrt(dx * dx + dy * dy);

            if (largo < 2 * Radio + 4)
            {
                return;
            }

            float ux = dx / largo;
            float uy = dy / largo;
            float px = -uy * desplazamiento;
            float py = ux * desplazamiento;

            PointF inicio = new PointF(a.X + ux * (Radio + 1) + px, a.Y + uy * (Radio + 1) + py);
            PointF fin = new PointF(b.X - ux * (Radio + 3) + px, b.Y - uy * (Radio + 3) + py);
            g.DrawLine(lapiz, inicio, fin);
        }

        // Escribe "Q costo · tiempo h" a la mitad de la ruta. Si ese espacio ya está
        // ocupado (ruta muy corta o zona con muchas ciudades) no se escribe, para no
        // tapar nombres; los valores siempre se ven en la lista de adyacencia.
        private void DibujarPeso(Graphics g, PointF a, PointF b, Arista arista, float desplazamiento, Espacios ocupados)
        {
            string texto = "Q" + arista.Costo.ToString("0.##") + " · " + arista.Tiempo.ToString("0.#") + " h";

            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float largo = Math.Max((float)Math.Sqrt(dx * dx + dy * dy), 1);
            float px = -dy / largo * desplazamiento;
            float py = dx / largo * desplazamiento;

            PointF medio = new PointF((a.X + b.X) / 2 + px, (a.Y + b.Y) / 2 + py);
            SizeF tamanio = g.MeasureString(texto, fuentePeso);
            RectangleF caja = new RectangleF(medio.X - tamanio.Width / 2, medio.Y - tamanio.Height / 2,
                                             tamanio.Width, tamanio.Height);

            if (ocupados.Choca(caja))
            {
                return;
            }
            ocupados.Agregar(caja);

            using SolidBrush fondo = new SolidBrush(Color.FromArgb(215, 255, 255, 255));
            using SolidBrush brocha = new SolidBrush(Paleta.TextoSecundario);
            g.FillRectangle(fondo, caja);
            g.DrawString(texto, fuentePeso, brocha, caja.Location);
        }

        private void DibujarCamino(Graphics g, int[] camino, Color color, bool punteado, float desplazamiento)
        {
            using Pen lapiz = new Pen(color, 4f);
            using AdjustableArrowCap flecha = new AdjustableArrowCap(2.5f, 3f);
            lapiz.CustomEndCap = flecha;

            if (punteado)
            {
                lapiz.DashStyle = DashStyle.Dash;
            }

            for (int k = 0; k < camino.Length - 1; k++)
            {
                DibujarLinea(g, posiciones[camino[k]], posiciones[camino[k + 1]], lapiz, desplazamiento);
            }
        }

        private void DibujarCiudades(Graphics g, int[] regiones, int[] numeroVisita, RectangleF[] etiquetas)
        {
            Grafo mapa = sistema!.Mapa;
            int n = mapa.Cantidad;

            using Pen borde = new Pen(Color.White, 2f);
            using SolidBrush brochaTexto = new SolidBrush(Paleta.Texto);
            using SolidBrush fondoTexto = new SolidBrush(Color.FromArgb(200, 255, 255, 255));

            for (int i = 0; i < n; i++)
            {
                PointF p = posiciones[i];
                Color color = Paleta.ColorRegion(regiones[i]);

                // En un recorrido, las ciudades que no se alcanzaron se ven apagadas
                if (ordenRecorrido != null && numeroVisita[i] == 0)
                {
                    color = Paleta.Aclarar(color, 0.7f);
                }

                RectangleF circulo = new RectangleF(p.X - Radio, p.Y - Radio, 2 * Radio, 2 * Radio);
                using (SolidBrush relleno = new SolidBrush(color))
                {
                    g.FillEllipse(relleno, circulo);
                }
                g.DrawEllipse(borde, circulo);

                // Anillo verde para el origen y rojo para el destino
                if (i == ciudadOrigen || i == ciudadDestino)
                {
                    using Pen anillo = new Pen(i == ciudadOrigen ? Paleta.Origen : Paleta.Destino, 3f);
                    RectangleF exterior = circulo;
                    exterior.Inflate(4, 4);
                    g.DrawEllipse(anillo, exterior);
                }

                if (numeroVisita[i] > 0)
                {
                    DibujarNumero(g, p, numeroVisita[i]);
                }

                g.FillRectangle(fondoTexto, etiquetas[i]);
                g.DrawString(mapa.ObtenerNombre(i), fuenteCiudad, brochaTexto, etiquetas[i].Location);
            }
        }

        // Prueba abajo, arriba, a la derecha y a la izquierda de la ciudad
        // y devuelve el primer espacio que no choca con nada
        private RectangleF UbicarEtiqueta(PointF p, SizeF tamanio, Espacios ocupados)
        {
            float w = tamanio.Width;
            float h = tamanio.Height;
            float separacion = Radio + 3;

            RectangleF[] candidatos =
            {
                new RectangleF(p.X - w / 2, p.Y + separacion, w, h),
                new RectangleF(p.X - w / 2, p.Y - separacion - h, w, h),
                new RectangleF(p.X + separacion, p.Y - h / 2, w, h),
                new RectangleF(p.X - separacion - w, p.Y - h / 2, w, h),
                new RectangleF(p.X + separacion - 4, p.Y + separacion - 4, w, h),
                new RectangleF(p.X - separacion - w + 4, p.Y - separacion - h + 4, w, h)
            };

            foreach (RectangleF candidato in candidatos)
            {
                bool dentro = candidato.Left >= 0 && candidato.Top >= 0 &&
                              candidato.Right <= Width && candidato.Bottom <= Height;

                if (dentro && !ocupados.Choca(candidato))
                {
                    return candidato;
                }
            }

            return candidatos[0];
        }

        // Círculo pequeño arriba a la derecha de la ciudad con su número de visita
        private RectangleF RectanguloNumero(PointF p)
        {
            return new RectangleF(p.X + Radio - 5, p.Y - Radio - 11, 16, 16);
        }

        private void DibujarNumero(Graphics g, PointF p, int numero)
        {
            RectangleF circulo = RectanguloNumero(p);

            using SolidBrush fondo = new SolidBrush(Paleta.Texto);
            using StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;

            g.FillEllipse(fondo, circulo);
            g.DrawString(numero.ToString(), fuenteNumero, Brushes.White, circulo, formato);
        }

        // Espacio que ocupa la leyenda según cuántas líneas va a mostrar
        private RectangleF RectanguloLeyenda()
        {
            int totalRegiones = sistema!.Regiones.ContarRegiones();
            int regionesVisibles = Math.Min(totalRegiones, 6);

            int lineas = regionesVisibles + 2;
            if (totalRegiones > regionesVisibles) lineas++;
            if (caminoPrincipal != null) lineas++;
            if (caminoSecundario != null) lineas++;
            if (ciudadOrigen != -1) lineas++;
            if (ciudadDestino != -1) lineas++;

            return new RectangleF(10, 10, 190, lineas * AltoLineaLeyenda + 10);
        }

        // Cuadro en la esquina superior izquierda que explica los colores
        private void DibujarLeyenda(Graphics g, int[] regiones)
        {
            int totalRegiones = sistema!.Regiones.ContarRegiones();
            int regionesVisibles = Math.Min(totalRegiones, 6);

            // Cantidad de ciudades de cada región
            int[] ciudadesPorRegion = new int[totalRegiones + 1];
            foreach (int region in regiones)
            {
                ciudadesPorRegion[region]++;
            }

            const float alto = AltoLineaLeyenda;
            RectangleF caja = RectanguloLeyenda();
            float x = caja.X;
            float y = caja.Y;

            using SolidBrush fondo = new SolidBrush(Color.FromArgb(235, 255, 255, 255));
            using Pen bordeCaja = new Pen(Color.FromArgb(226, 232, 240));
            using SolidBrush brochaTexto = new SolidBrush(Paleta.Texto);
            g.FillRectangle(fondo, caja);
            g.DrawRectangle(bordeCaja, caja.X, caja.Y, caja.Width, caja.Height);

            float fila = y + 5;

            for (int r = 1; r <= regionesVisibles; r++)
            {
                using (SolidBrush brochaRegion = new SolidBrush(Paleta.ColorRegion(r)))
                {
                    g.FillEllipse(brochaRegion, x + 8, fila + 3, 10, 10);
                }
                g.DrawString("Región " + r + " (" + ciudadesPorRegion[r] + " ciudades)", fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            if (totalRegiones > regionesVisibles)
            {
                g.DrawString("… y " + (totalRegiones - regionesVisibles) + " regiones más", fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            using (Pen lapiz = new Pen(Paleta.Arista, 1.6f))
            {
                g.DrawLine(lapiz, x + 6, fila + 8, x + 22, fila + 8);
                g.DrawString("Ruta de doble sentido", fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;

                using AdjustableArrowCap flecha = new AdjustableArrowCap(4f, 5f);
                lapiz.CustomEndCap = flecha;
                g.DrawLine(lapiz, x + 6, fila + 8, x + 22, fila + 8);
                g.DrawString("Ruta de un solo sentido", fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            if (caminoPrincipal != null)
            {
                using Pen lapiz = new Pen(Paleta.RutaPrincipal, 4f);
                g.DrawLine(lapiz, x + 6, fila + 8, x + 22, fila + 8);
                g.DrawString(textoPrincipal, fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            if (caminoSecundario != null)
            {
                using Pen lapiz = new Pen(Paleta.RutaSecundaria, 4f);
                lapiz.DashStyle = DashStyle.Dash;
                g.DrawLine(lapiz, x + 6, fila + 8, x + 22, fila + 8);
                g.DrawString(textoSecundario, fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            if (ciudadOrigen != -1)
            {
                using Pen anillo = new Pen(Paleta.Origen, 2.5f);
                g.DrawEllipse(anillo, x + 8, fila + 3, 10, 10);
                g.DrawString(ordenRecorrido != null ? "Inicio del recorrido" : "Origen", fuenteLeyenda, brochaTexto, x + 26, fila);
                fila += alto;
            }

            if (ciudadDestino != -1)
            {
                using Pen anillo = new Pen(Paleta.Destino, 2.5f);
                g.DrawEllipse(anillo, x + 8, fila + 3, 10, 10);
                g.DrawString("Destino", fuenteLeyenda, brochaTexto, x + 26, fila);
            }
        }

        // CLIC SOBRE LAS CIUDADES

        // Devuelve el índice de la ciudad que está bajo el puntero, o -1
        private int CiudadEn(Point punto)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                float dx = punto.X - posiciones[i].X;
                float dy = punto.Y - posiciones[i].Y;

                if (dx * dx + dy * dy <= (Radio + 4) * (Radio + 4))
                {
                    return i;
                }
            }

            return -1;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = CiudadClic != null && CiudadEn(e.Location) != -1 ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int indice = CiudadEn(e.Location);
            if (indice != -1 && sistema != null && indice < sistema.Mapa.Cantidad)
            {
                CiudadClic?.Invoke(this, sistema.Mapa.ObtenerNombre(indice));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fuenteCiudad.Dispose();
                fuentePeso.Dispose();
                fuenteNumero.Dispose();
                fuenteLeyenda.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
