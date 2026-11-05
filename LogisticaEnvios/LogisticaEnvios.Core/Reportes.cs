using System;
using System.Globalization;
using System.IO;
using System.Text;

// REPORTES PARA TOMA DE DECISIONES
// Responsable: Ángel
//
// Usan las cuatro estructuras para dar información útil a la empresa:
//   1. Costo y tiempo estimado de cada envío  (Tabla Hash + Union-Find + Dijkstra)
//   2. Envíos sin ruta posible y qué hacer     (Union-Find + Dijkstra)
//   3. Envíos por región de distribución       (Union-Find)
//   4. Ciudades con más conexiones             (Grafo)
// Cada reporte es una tabla (columnas y filas de texto) más un resumen con
// conclusiones. La consola lo imprime, la interfaz gráfica lo muestra en una
// tabla y ambos pueden exportarlo a .txt o .csv.

public class Reporte
{
    public string Titulo;
    public string Descripcion;
    public string[] Columnas;
    public ListaEnlazada<string[]> Filas;

    // Conclusiones y recomendaciones del reporte
    public ListaEnlazada<string> Resumen;

    public Reporte(string titulo, string descripcion, string[] columnas)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        Columnas = columnas;
        Filas = new ListaEnlazada<string[]>();
        Resumen = new ListaEnlazada<string>();
    }
}

public class GeneradorReportes
{
    public const int CantidadReportes = 4;

    private readonly SistemaLogistica sistema;

    public GeneradorReportes(SistemaLogistica sistema)
    {
        this.sistema = sistema;
    }

    public static string[] NombresReportes()
    {
        return new string[]
        {
            "Costo y tiempo estimado por envío",
            "Envíos sin ruta posible",
            "Envíos por región de distribución",
            "Ciudades con más conexiones"
        };
    }

    // Nombre sugerido del archivo .csv de cada reporte
    public static string[] NombresArchivo()
    {
        return new string[]
        {
            "costos_por_envio.csv",
            "envios_sin_ruta.csv",
            "envios_por_region.csv",
            "ciudades_mas_conectadas.csv"
        };
    }

    // numero va de 0 a CantidadReportes - 1
    public Reporte Generar(int numero)
    {
        switch (numero)
        {
            case 0: return CostosPorEnvio();
            case 1: return EnviosSinRuta();
            case 2: return EnviosPorRegion();
            default: return CiudadesConectadas();
        }
    }

    public Reporte[] GenerarTodos()
    {
        Reporte[] reportes = new Reporte[CantidadReportes];
        for (int i = 0; i < CantidadReportes; i++)
        {
            reportes[i] = Generar(i);
        }
        return reportes;
    }

    // REPORTE 1: COSTO Y TIEMPO ESTIMADO POR ENVÍO

    private Reporte CostosPorEnvio()
    {
        Reporte reporte = new Reporte(
            "Costo y tiempo estimado por envío",
            "Para cada envío se calcula con Dijkstra la ruta más económica y la más rápida " +
            "(antes, el Union-Find confirma que origen y destino estén conectados).",
            new string[] { "Código", "Origen", "Destino", "Ruta más económica", "Costo", "Tiempo",
                           "Tiempo ruta rápida", "Costo ruta rápida" });

        Envio[] envios = EnviosOrdenados();
        int conRuta = 0;
        double costoTotal = 0;
        double tiempoTotal = 0;
        string masCostoso = "";
        double costoMayor = -1;
        int conAhorro = 0;
        string mayorAhorro = "";
        double horasAhorro = 0;
        double costoExtra = 0;

        foreach (Envio envio in envios)
        {
            ResultadoRuta economica = sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, true);

            if (economica.Camino == null)
            {
                reporte.Filas.Agregar(new string[] { envio.CodigoRastreo, envio.CiudadOrigen, envio.CiudadDestino,
                                                     "Sin ruta (" + MotivoSinRuta(economica) + ")", "-", "-", "-", "-" });
                continue;
            }

            ResultadoRuta rapida = sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, false);

            reporte.Filas.Agregar(new string[] { envio.CodigoRastreo, envio.CiudadOrigen, envio.CiudadDestino,
                                                 sistema.TextoCamino(economica.Camino),
                                                 Dinero(economica.CostoTotal), Horas(economica.TiempoTotal),
                                                 Horas(rapida.TiempoTotal), Dinero(rapida.CostoTotal) });

            conRuta++;
            costoTotal += economica.CostoTotal;
            tiempoTotal += economica.TiempoTotal;

            if (economica.CostoTotal > costoMayor)
            {
                costoMayor = economica.CostoTotal;
                masCostoso = envio.CodigoRastreo;
            }

            // ¿La ruta rápida ahorra tiempo a cambio de pagar más?
            double ahorro = economica.TiempoTotal - rapida.TiempoTotal;
            if (ahorro > 0.0001)
            {
                conAhorro++;
                if (ahorro > horasAhorro)
                {
                    horasAhorro = ahorro;
                    costoExtra = rapida.CostoTotal - economica.CostoTotal;
                    mayorAhorro = envio.CodigoRastreo;
                }
            }
        }

        int sinRuta = envios.Length - conRuta;
        reporte.Resumen.Agregar("Envíos analizados: " + envios.Length + " (" + conRuta + " con ruta y " +
                                sinRuta + " sin ruta).");

        if (conRuta > 0)
        {
            reporte.Resumen.Agregar("Costo total estimado con las rutas más económicas: " + Dinero(costoTotal) +
                                    "; tiempo total: " + Horas(tiempoTotal) + ".");
            reporte.Resumen.Agregar("Costo promedio por envío despachable: " + Dinero(costoTotal / conRuta) + ".");
            reporte.Resumen.Agregar("Envío más costoso: " + masCostoso + " (" + Dinero(costoMayor) + ").");

            if (conAhorro > 0)
            {
                reporte.Resumen.Agregar("En " + conAhorro + " envío(s) la ruta más rápida ahorra tiempo pagando más. " +
                                        "El mayor ahorro es " + mayorAhorro + ": " + Horas(horasAhorro) + " menos por " +
                                        Dinero(costoExtra) + " adicionales.");
            }
            else
            {
                reporte.Resumen.Agregar("En todos los envíos con ruta, la ruta más económica también es la más rápida.");
            }
        }

        if (sinRuta > 0)
        {
            reporte.Resumen.Agregar("Hay " + sinRuta + " envío(s) que no se pueden despachar; vea el reporte «Envíos sin ruta posible».");
        }

        return reporte;
    }

    // REPORTE 2: ENVÍOS SIN RUTA POSIBLE

    private Reporte EnviosSinRuta()
    {
        Reporte reporte = new Reporte(
            "Envíos sin ruta posible",
            "Envíos que hoy no se pueden despachar, el motivo y la acción recomendada para atenderlos.",
            new string[] { "Código", "Origen", "Destino", "Motivo", "Recomendación" });

        Envio[] envios = EnviosOrdenados();
        int[] numeroRegion = sistema.Regiones.NumerarRegiones();
        int totalRegiones = sistema.Regiones.ContarRegiones();

        // Cuántos envíos quedarían resueltos al conectar cada par de regiones
        int[,] porPar = new int[totalRegiones + 1, totalRegiones + 1];
        int sinRuta = 0;

        foreach (Envio envio in envios)
        {
            ResultadoRuta ruta = sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, true);
            if (ruta.Camino != null)
            {
                continue;
            }

            sinRuta++;
            int o = sistema.Mapa.BuscarIndice(envio.CiudadOrigen);
            int d = sistema.Mapa.BuscarIndice(envio.CiudadDestino);
            string motivo;
            string recomendacion;

            if (!ruta.MismaRegion)
            {
                int ro = numeroRegion[o];
                int rd = numeroRegion[d];
                porPar[Math.Min(ro, rd), Math.Max(ro, rd)]++;

                motivo = "Origen en la región " + ro + " y destino en la región " + rd + " (sin conexión entre ellas)";
                recomendacion = "Registrar una ruta entre ambas regiones. Conexión más corta posible: " +
                                ConexionSugerida(ro, rd, numeroRegion) + ".";
            }
            else
            {
                motivo = "Misma región, pero las rutas de un solo sentido no permiten llegar";
                recomendacion = "Registrar una ruta de doble sentido (o la de regreso que falta) para poder ir de " +
                                envio.CiudadOrigen + " a " + envio.CiudadDestino + ".";
            }

            reporte.Filas.Agregar(new string[] { envio.CodigoRastreo, envio.CiudadOrigen, envio.CiudadDestino,
                                                 motivo, recomendacion });
        }

        if (sinRuta == 0)
        {
            reporte.Resumen.Agregar("Todos los envíos registrados tienen al menos una ruta posible.");
            return reporte;
        }

        reporte.Resumen.Agregar(sinRuta + " de " + envios.Length + " envíos no se pueden despachar con la red actual.");

        // Par de regiones cuya conexión destrabaría más envíos
        int mejorA = 0, mejorB = 0, mayor = 0;
        for (int a = 1; a <= totalRegiones; a++)
        {
            for (int b = a + 1; b <= totalRegiones; b++)
            {
                if (porPar[a, b] > mayor)
                {
                    mayor = porPar[a, b];
                    mejorA = a;
                    mejorB = b;
                }
            }
        }

        if (mayor > 0)
        {
            reporte.Resumen.Agregar("Prioridad: conectar la región " + mejorA + " con la región " + mejorB +
                                    " resolvería " + mayor + " envío(s). Conexión sugerida: " +
                                    ConexionSugerida(mejorA, mejorB, numeroRegion) + ".");
        }

        return reporte;
    }

    // REPORTE 3: ENVÍOS POR REGIÓN DE DISTRIBUCIÓN

    private Reporte EnviosPorRegion()
    {
        Reporte reporte = new Reporte(
            "Envíos por región de distribución",
            "Cantidad de ciudades y de envíos de cada región que forma el Union-Find.",
            new string[] { "Región", "Representante", "Ciudades", "Envíos que salen", "Envíos que llegan",
                           "Envíos internos", "Envíos hacia otra región" });

        Grafo mapa = sistema.Mapa;
        int[] numeroRegion = sistema.Regiones.NumerarRegiones();
        int total = sistema.Regiones.ContarRegiones();

        int[] ciudades = new int[total + 1];
        int[] representante = new int[total + 1];
        int[] salen = new int[total + 1];
        int[] llegan = new int[total + 1];
        int[] internos = new int[total + 1];
        int[] haciaOtra = new int[total + 1];

        for (int i = 0; i < mapa.Cantidad; i++)
        {
            ciudades[numeroRegion[i]]++;
            representante[numeroRegion[i]] = sistema.Regiones.Find(i);
        }

        int entreRegiones = 0;
        foreach (Envio envio in EnviosOrdenados())
        {
            int ro = numeroRegion[mapa.BuscarIndice(envio.CiudadOrigen)];
            int rd = numeroRegion[mapa.BuscarIndice(envio.CiudadDestino)];

            salen[ro]++;
            llegan[rd]++;

            if (ro == rd)
            {
                internos[ro]++;
            }
            else
            {
                haciaOtra[ro]++;
                entreRegiones++;
            }
        }

        int regionMayor = 0;
        int movimientoMayor = -1;

        for (int r = 1; r <= total; r++)
        {
            reporte.Filas.Agregar(new string[] { "Región " + r, mapa.ObtenerNombre(representante[r]),
                                                 ciudades[r].ToString(), salen[r].ToString(), llegan[r].ToString(),
                                                 internos[r].ToString(), haciaOtra[r].ToString() });

            if (salen[r] + llegan[r] > movimientoMayor)
            {
                movimientoMayor = salen[r] + llegan[r];
                regionMayor = r;
            }
        }

        if (total == 0)
        {
            reporte.Resumen.Agregar("No hay ciudades registradas.");
            return reporte;
        }

        reporte.Resumen.Agregar("Hay " + total + " región(es) de distribución.");
        reporte.Resumen.Agregar("La región " + regionMayor + " (representante: " + mapa.ObtenerNombre(representante[regionMayor]) +
                                ") concentra el mayor movimiento: " + salen[regionMayor] + " envíos salen y " +
                                llegan[regionMayor] + " llegan.");

        if (entreRegiones > 0)
        {
            reporte.Resumen.Agregar(entreRegiones + " envío(s) van de una región a otra; no se pueden despachar " +
                                    "hasta que esas regiones se conecten con una ruta.");
        }
        else
        {
            reporte.Resumen.Agregar("Ningún envío necesita pasar de una región a otra.");
        }

        return reporte;
    }

    // REPORTE 4: CIUDADES CON MÁS CONEXIONES

    private Reporte CiudadesConectadas()
    {
        Reporte reporte = new Reporte(
            "Ciudades con más conexiones",
            "Ciudades ordenadas por la cantidad de ciudades vecinas en el grafo; las primeras son " +
            "candidatas a centro de distribución y las de una sola conexión son puntos críticos.",
            new string[] { "Posición", "Ciudad", "Región", "Ciudades vecinas", "Rutas de salida",
                           "Rutas de entrada", "Envíos que salen", "Envíos que llegan" });

        Grafo mapa = sistema.Mapa;
        int n = mapa.Cantidad;
        int[] numeroRegion = sistema.Regiones.NumerarRegiones();

        int[] salida = new int[n];
        int[] entrada = new int[n];
        int[] vecinas = new int[n];
        int[] enviosSalen = new int[n];
        int[] enviosLlegan = new int[n];

        // Rutas de salida y de entrada de cada ciudad (lista de adyacencia)
        for (int i = 0; i < n; i++)
        {
            Arista? arista = mapa.PrimeraArista(i);
            while (arista != null)
            {
                salida[i]++;
                entrada[arista.Destino]++;
                arista = arista.Siguiente;
            }
        }

        // Ciudades vecinas: unidas por una ruta en cualquier sentido
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j && (mapa.BuscarArista(i, j) != null || mapa.BuscarArista(j, i) != null))
                {
                    vecinas[i]++;
                }
            }
        }

        foreach (Envio envio in EnviosOrdenados())
        {
            enviosSalen[mapa.BuscarIndice(envio.CiudadOrigen)]++;
            enviosLlegan[mapa.BuscarIndice(envio.CiudadDestino)]++;
        }

        // Orden: más vecinas, luego más rutas, luego más envíos, luego por nombre
        int[] orden = new int[n];
        for (int i = 0; i < n; i++)
        {
            orden[i] = i;
        }

        // Ordenamiento por inserción
        for (int i = 1; i < n; i++)
        {
            int actual = orden[i];
            int j = i - 1;
            while (j >= 0 && VaDespues(orden[j], actual, vecinas, salida, entrada, enviosSalen, enviosLlegan))
            {
                orden[j + 1] = orden[j];
                j--;
            }
            orden[j + 1] = actual;
        }

        for (int k = 0; k < n; k++)
        {
            int c = orden[k];
            reporte.Filas.Agregar(new string[] { (k + 1).ToString(), mapa.ObtenerNombre(c), "Región " + numeroRegion[c],
                                                 vecinas[c].ToString(), salida[c].ToString(), entrada[c].ToString(),
                                                 enviosSalen[c].ToString(), enviosLlegan[c].ToString() });
        }

        if (n == 0)
        {
            reporte.Resumen.Agregar("No hay ciudades registradas.");
            return reporte;
        }

        int centro = orden[0];
        reporte.Resumen.Agregar("Centro de distribución sugerido: " + mapa.ObtenerNombre(centro) + ", conectada con " +
                                vecinas[centro] + " ciudades (" + (enviosSalen[centro] + enviosLlegan[centro]) +
                                " envíos salen o llegan ahí).");

        string unaConexion = "";
        string sinRutas = "";
        string sinEntrada = "";
        string sinSalida = "";

        for (int k = 0; k < n; k++)
        {
            int c = orden[k];
            string nombre = mapa.ObtenerNombre(c);

            if (vecinas[c] == 0) sinRutas = Agregar(sinRutas, nombre);
            else if (vecinas[c] == 1) unaConexion = Agregar(unaConexion, nombre);

            if (vecinas[c] > 0 && entrada[c] == 0) sinEntrada = Agregar(sinEntrada, nombre);
            if (vecinas[c] > 0 && salida[c] == 0) sinSalida = Agregar(sinSalida, nombre);
        }

        if (unaConexion != "")
        {
            reporte.Resumen.Agregar("Dependen de una sola conexión (si esa ruta falla quedan aisladas): " + unaConexion + ".");
        }
        if (sinEntrada != "")
        {
            reporte.Resumen.Agregar("Ninguna ruta llega a: " + sinEntrada + " (ahí no se pueden recibir paquetes).");
        }
        if (sinSalida != "")
        {
            reporte.Resumen.Agregar("Ninguna ruta sale de: " + sinSalida + " (desde ahí no se pueden despachar paquetes).");
        }
        if (sinRutas != "")
        {
            reporte.Resumen.Agregar("Ciudades sin ninguna ruta: " + sinRutas + ".");
        }

        return reporte;
    }

    // true si la ciudad a debe ir después de la ciudad b en el ranking
    private bool VaDespues(int a, int b, int[] vecinas, int[] salida, int[] entrada,
                           int[] enviosSalen, int[] enviosLlegan)
    {
        if (vecinas[a] != vecinas[b]) return vecinas[a] < vecinas[b];

        int rutasA = salida[a] + entrada[a];
        int rutasB = salida[b] + entrada[b];
        if (rutasA != rutasB) return rutasA < rutasB;

        int enviosA = enviosSalen[a] + enviosLlegan[a];
        int enviosB = enviosSalen[b] + enviosLlegan[b];
        if (enviosA != enviosB) return enviosA < enviosB;

        return string.Compare(sistema.Mapa.ObtenerNombre(a), sistema.Mapa.ObtenerNombre(b), StringComparison.CurrentCulture) > 0;
    }

    // FUNCIONES DE APOYO

    // Envíos de la tabla hash ordenados por código (ordenamiento por inserción)
    private Envio[] EnviosOrdenados()
    {
        Envio[] envios = sistema.Envios.ObtenerEnvios().ConvertirArreglo();

        for (int i = 1; i < envios.Length; i++)
        {
            Envio actual = envios[i];
            int j = i - 1;
            while (j >= 0 && string.CompareOrdinal(envios[j].CodigoRastreo, actual.CodigoRastreo) > 0)
            {
                envios[j + 1] = envios[j];
                j--;
            }
            envios[j + 1] = actual;
        }

        return envios;
    }

    private string MotivoSinRuta(ResultadoRuta ruta)
    {
        return ruta.MismaRegion ? "rutas de un solo sentido" : "regiones distintas";
    }

    // Busca el par de ciudades (una de cada región) más cercano en línea recta.
    // Si las ciudades no tienen coordenadas, sugiere unir a los representantes.
    private string ConexionSugerida(int regionA, int regionB, int[] numeroRegion)
    {
        Grafo mapa = sistema.Mapa;
        int mejorA = -1;
        int mejorB = -1;
        double menorDistancia = double.MaxValue;

        for (int i = 0; i < mapa.Cantidad; i++)
        {
            if (numeroRegion[i] != regionA || !mapa.TieneCoordenadas(i)) continue;

            for (int j = 0; j < mapa.Cantidad; j++)
            {
                if (numeroRegion[j] != regionB || !mapa.TieneCoordenadas(j)) continue;

                double distancia = DistanciaKm(mapa.ObtenerLatitud(i), mapa.ObtenerLongitud(i),
                                               mapa.ObtenerLatitud(j), mapa.ObtenerLongitud(j));
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    mejorA = i;
                    mejorB = j;
                }
            }
        }

        if (mejorA != -1)
        {
            return mapa.ObtenerNombre(mejorA) + " – " + mapa.ObtenerNombre(mejorB) +
                   " (≈ " + menorDistancia.ToString("0", CultureInfo.InvariantCulture) + " km en línea recta)";
        }

        // Sin coordenadas: se unen los representantes de cada región
        int representanteA = -1;
        int representanteB = -1;
        for (int i = 0; i < mapa.Cantidad; i++)
        {
            if (numeroRegion[i] == regionA && representanteA == -1) representanteA = sistema.Regiones.Find(i);
            if (numeroRegion[i] == regionB && representanteB == -1) representanteB = sistema.Regiones.Find(i);
        }

        return mapa.ObtenerNombre(representanteA) + " – " + mapa.ObtenerNombre(representanteB);
    }

    // Distancia entre dos coordenadas sobre la superficie de la Tierra (fórmula del haverseno)
    private static double DistanciaKm(double lat1, double lon1, double lat2, double lon2)
    {
        const double RadioTierra = 6371;
        double dLat = (lat2 - lat1) * Math.PI / 180;
        double dLon = (lon2 - lon1) * Math.PI / 180;
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        return 2 * RadioTierra * Math.Asin(Math.Sqrt(a));
    }

    private static string Agregar(string lista, string nombre)
    {
        return lista == "" ? nombre : lista + ", " + nombre;
    }

    // Los números se escriben siempre con punto decimal para que el .csv no se rompa
    private static string Dinero(double valor)
    {
        return "Q" + valor.ToString("0.00", CultureInfo.InvariantCulture);
    }

    private static string Horas(double valor)
    {
        return valor.ToString("0.0", CultureInfo.InvariantCulture) + " h";
    }

    // TEXTO Y EXPORTACIÓN

    // Convierte el reporte en texto con columnas alineadas (consola y archivo .txt)
    public static string ATexto(Reporte reporte)
    {
        string[][] filas = reporte.Filas.ConvertirArreglo();
        int columnas = reporte.Columnas.Length;

        // Ancho de cada columna: el texto más largo entre el encabezado y las filas
        int[] anchos = new int[columnas];
        for (int c = 0; c < columnas; c++)
        {
            anchos[c] = reporte.Columnas[c].Length;
            foreach (string[] fila in filas)
            {
                anchos[c] = Math.Max(anchos[c], fila[c].Length);
            }
        }

        StringBuilder texto = new StringBuilder();
        texto.AppendLine("# " + reporte.Titulo.ToUpper());
        texto.AppendLine(reporte.Descripcion);
        texto.AppendLine();

        if (filas.Length == 0)
        {
            texto.AppendLine("(sin filas)");
        }
        else
        {
            texto.AppendLine(LineaTabla(reporte.Columnas, anchos));

            string[] separadores = new string[columnas];
            for (int c = 0; c < columnas; c++)
            {
                separadores[c] = new string('-', anchos[c]);
            }
            texto.AppendLine(LineaTabla(separadores, anchos));

            foreach (string[] fila in filas)
            {
                texto.AppendLine(LineaTabla(fila, anchos));
            }
        }

        texto.AppendLine();
        texto.AppendLine("Resumen:");
        foreach (string linea in reporte.Resumen.ConvertirArreglo())
        {
            texto.AppendLine("- " + linea);
        }

        return texto.ToString();
    }

    private static string LineaTabla(string[] celdas, int[] anchos)
    {
        string linea = "";
        for (int c = 0; c < celdas.Length; c++)
        {
            linea += (c == 0 ? "" : "  ") + celdas[c].PadRight(anchos[c]);
        }
        return linea.TrimEnd();
    }

    // Guarda todos los reportes en un solo archivo de texto
    public static bool ExportarTxt(Reporte[] reportes, string ruta, out string mensaje)
    {
        try
        {
            CrearCarpeta(ruta);
            using (StreamWriter escritor = new StreamWriter(ruta, false, new UTF8Encoding(true)))
            {
                escritor.WriteLine("REPORTES PARA TOMA DE DECISIONES - LOGÍSTICA DE ENVÍOS");
                escritor.WriteLine("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                escritor.WriteLine();

                foreach (Reporte reporte in reportes)
                {
                    escritor.WriteLine(ATexto(reporte));
                }
            }

            mensaje = "Reportes exportados en: " + ruta;
            return true;
        }
        catch (Exception ex)
        {
            mensaje = "No se pudo exportar: " + ex.Message;
            return false;
        }
    }

    // Guarda un reporte como tabla .csv (se puede abrir con Excel)
    public static bool ExportarCsv(Reporte reporte, string ruta, out string mensaje)
    {
        try
        {
            CrearCarpeta(ruta);
            using (StreamWriter escritor = new StreamWriter(ruta, false, new UTF8Encoding(true)))
            {
                escritor.WriteLine(LineaCsv(reporte.Columnas));
                foreach (string[] fila in reporte.Filas.ConvertirArreglo())
                {
                    escritor.WriteLine(LineaCsv(fila));
                }
            }

            mensaje = "Reporte exportado en: " + ruta;
            return true;
        }
        catch (Exception ex)
        {
            mensaje = "No se pudo exportar: " + ex.Message;
            return false;
        }
    }

    private static void CrearCarpeta(string rutaArchivo)
    {
        string? carpeta = Path.GetDirectoryName(Path.GetFullPath(rutaArchivo));
        if (carpeta != null)
        {
            Directory.CreateDirectory(carpeta);
        }
    }

    // Une las celdas con comas; si una celda trae comas o comillas va entre comillas
    private static string LineaCsv(string[] celdas)
    {
        string linea = "";
        for (int c = 0; c < celdas.Length; c++)
        {
            string celda = celdas[c];
            if (celda.Contains(",") || celda.Contains("\""))
            {
                celda = "\"" + celda.Replace("\"", "\"\"") + "\"";
            }
            linea += (c == 0 ? "" : ",") + celda;
        }
        return linea;
    }
}
