using System;
using System.Globalization;
using System.IO;
using System.Text;

// SISTEMA DE LOGÍSTICA (integración de las cuatro estructuras)
// Responsable: Ángel
//
// Aquí las estructuras trabajan juntas en un mismo flujo:
//   Trie       -> sugiere el nombre de la ciudad o el código de rastreo
//   Tabla Hash -> localiza el envío completo por su código
//   Union-Find -> valida que origen y destino estén en la misma región
//   Grafo      -> calcula la ruta con Dijkstra (solo si Union-Find lo permite)
public class SistemaLogistica
{
    public TablaHash Envios { get; private set; }
    public Grafo Mapa { get; private set; }
    public UnionFind Regiones { get; private set; }
    public Trie TrieCiudades { get; private set; }
    public Trie TrieCodigos { get; private set; }

    public SistemaLogistica()
    {
        Envios = new TablaHash(7);
        Mapa = new Grafo(10);
        Regiones = new UnionFind(10);
        TrieCiudades = new Trie();
        TrieCodigos = new Trie();
    }

    // Carpeta "datos" del proyecto. Se busca subiendo desde la carpeta del
    // ejecutable (bin\Debug\...) hasta encontrar LogisticaEnvios.sln; así lo que
    // se guarda queda en los .csv del proyecto y no en la copia que hay en bin.
    // Si no se encuentra (por ejemplo, si solo se copió el ejecutable), se usa
    // la carpeta datos que está junto al ejecutable.
    public static string CarpetaDatosPorDefecto()
    {
        DirectoryInfo? carpeta = new DirectoryInfo(AppContext.BaseDirectory);

        while (carpeta != null)
        {
            if (File.Exists(Path.Combine(carpeta.FullName, "LogisticaEnvios.sln")) &&
                Directory.Exists(Path.Combine(carpeta.FullName, "datos")))
            {
                return Path.Combine(carpeta.FullName, "datos");
            }
            carpeta = carpeta.Parent;
        }

        return Path.Combine(AppContext.BaseDirectory, "datos");
    }

    // Carpeta "reportes" junto a la carpeta datos (ahí se exportan los reportes)
    public static string CarpetaReportesPorDefecto()
    {
        string datos = CarpetaDatosPorDefecto();
        DirectoryInfo? padre = Directory.GetParent(datos);
        return Path.Combine(padre != null ? padre.FullName : AppContext.BaseDirectory, "reportes");
    }

    // Convierte texto a número aceptando punto o coma decimal
    public static bool ConvertirNumero(string texto, out double valor)
    {
        return double.TryParse(texto.Trim().Replace(',', '.'),
                               NumberStyles.Float,
                               CultureInfo.InvariantCulture,
                               out valor);
    }

    // CIUDADES

    // Registra una ciudad sin coordenadas
    public bool RegistrarCiudad(string nombre, out string mensaje)
    {
        return RegistrarCiudad(nombre, double.NaN, double.NaN, out mensaje);
    }

    // Registra la ciudad en el grafo, en el Union-Find y en el Trie.
    // La latitud y la longitud son opcionales (double.NaN = sin coordenadas)
    // y solo sirven para ubicar la ciudad en el mapa de la interfaz gráfica.
    public bool RegistrarCiudad(string nombre, double latitud, double longitud, out string mensaje)
    {
        nombre = nombre.Trim();

        if (nombre == "")
        {
            mensaje = "El nombre de la ciudad no puede estar vacío.";
            return false;
        }

        if (nombre.Contains(","))
        {
            mensaje = "El nombre de la ciudad no puede contener comas.";
            return false;
        }

        if (double.IsNaN(latitud) != double.IsNaN(longitud))
        {
            mensaje = "Debe indicar la latitud y la longitud, o ninguna de las dos.";
            return false;
        }

        if (!double.IsNaN(latitud) &&
            (latitud < -90 || latitud > 90 || longitud < -180 || longitud > 180))
        {
            mensaje = "Coordenadas no válidas: la latitud va de -90 a 90 y la longitud de -180 a 180.";
            return false;
        }

        int indice = Mapa.AgregarCiudad(nombre, latitud, longitud);

        if (indice == -1)
        {
            mensaje = "La ciudad " + nombre + " ya existe.";
            return false;
        }

        // La ciudad nueva forma su propia región hasta que se le asignen rutas
        Regiones.AgregarElemento();
        TrieCiudades.Insertar(nombre);

        mensaje = "Ciudad " + nombre + " registrada.";
        return true;
    }

    // RUTAS

    public bool RegistrarRuta(string origen, string destino, double costo, double tiempo,
                              bool dobleSentido, out string mensaje)
    {
        int o = Mapa.BuscarIndice(origen);
        int d = Mapa.BuscarIndice(destino);

        if (o == -1 || d == -1)
        {
            if (o == -1 && d == -1)
            {
                mensaje = "Las ciudades " + origen.Trim() + " y " + destino.Trim() + " no existen.";
            }
            else if (o == -1)
            {
                mensaje = "La ciudad " + origen.Trim() + " no existe.";
            }
            else
            {
                mensaje = "La ciudad " + destino.Trim() + " no existe.";
            }
            return false;
        }

        if (o == d)
        {
            mensaje = "Una ciudad no puede tener una ruta hacia sí misma.";
            return false;
        }

        // Dijkstra no funciona con pesos negativos
        if (costo < 0 || tiempo < 0)
        {
            mensaje = "El costo y el tiempo no pueden ser negativos.";
            return false;
        }

        if (Mapa.BuscarArista(o, d) != null)
        {
            mensaje = "Ya existe una ruta de " + Mapa.ObtenerNombre(o) + " hacia " + Mapa.ObtenerNombre(d) + ".";
            return false;
        }

        // Antes de asignar la ruta se consulta el Union-Find
        string region;
        if (Regiones.EstanConectados(o, d))
        {
            region = "Las ciudades ya pertenecían a la misma región de distribución.";
        }
        else
        {
            region = "La ruta une dos regiones de distribución distintas.";
        }

        Mapa.AgregarRuta(o, d, costo, tiempo);

        string ruta = "Ruta " + Mapa.ObtenerNombre(o) + " → " + Mapa.ObtenerNombre(d);
        string detalle;

        if (!dobleSentido)
        {
            detalle = ruta + " registrada en un solo sentido.";
        }
        else if (Mapa.AgregarRuta(d, o, costo, tiempo))
        {
            detalle = ruta + " registrada en doble sentido (ida y vuelta).";
        }
        else
        {
            // La vuelta ya existía: no se sobrescribe y se avisa con qué valores quedó
            Arista vuelta = Mapa.BuscarArista(d, o)!;
            detalle = ruta + " registrada. La ruta de regreso " + Mapa.ObtenerNombre(d) + " → " +
                      Mapa.ObtenerNombre(o) + " ya existía, así que se conservó con sus propios valores (Q" +
                      vuelta.Costo.ToString("0.00") + ", " + vuelta.Tiempo.ToString("0.0") + " h).";
        }

        Regiones.Union(o, d);

        mensaje = detalle + " " + region;
        return true;
    }

    public bool EliminarRuta(string origen, string destino, bool ambosSentidos, out string mensaje)
    {
        int o = Mapa.BuscarIndice(origen);
        int d = Mapa.BuscarIndice(destino);

        if (o == -1 || d == -1)
        {
            mensaje = "Una o ambas ciudades no existen.";
            return false;
        }

        bool ida = Mapa.EliminarRuta(o, d);
        bool vuelta = false;

        if (ambosSentidos)
        {
            vuelta = Mapa.EliminarRuta(d, o);
        }

        if (!ida && !vuelta)
        {
            mensaje = "No existe esa ruta.";
            return false;
        }

        // Union-Find no puede separar regiones, por eso se reconstruyen
        ReconstruirRegiones();

        mensaje = "Ruta eliminada. Las regiones de distribución fueron recalculadas.";
        return true;
    }

    // Vuelve a armar las regiones recorriendo todas las rutas del grafo
    public void ReconstruirRegiones()
    {
        Regiones.Reiniciar();

        for (int i = 0; i < Mapa.Cantidad; i++)
        {
            Arista? arista = Mapa.PrimeraArista(i);
            while (arista != null)
            {
                Regiones.Union(i, arista.Destino);
                arista = arista.Siguiente;
            }
        }
    }

    // ENVÍOS

    public bool RegistrarEnvio(string codigo, string remitente, string destinatario,
                               string origen, string destino, out string mensaje)
    {
        codigo = codigo.Trim().ToUpper();
        remitente = remitente.Trim();
        destinatario = destinatario.Trim();

        if (codigo == "" || remitente == "" || destinatario == "")
        {
            mensaje = "El código, el remitente y el destinatario son obligatorios.";
            return false;
        }

        if (codigo.Contains(",") || remitente.Contains(",") || destinatario.Contains(","))
        {
            mensaje = "Los datos del envío no pueden contener comas.";
            return false;
        }

        if (Envios.Buscar(codigo) != null)
        {
            mensaje = "Ya existe un envío con el código " + codigo + ".";
            return false;
        }

        int o = Mapa.BuscarIndice(origen);
        int d = Mapa.BuscarIndice(destino);

        if (o == -1 || d == -1)
        {
            mensaje = "La ciudad de origen o de destino no existe.";
            return false;
        }

        if (o == d)
        {
            mensaje = "La ciudad de origen y de destino no pueden ser la misma.";
            return false;
        }

        // Se guardan los nombres tal como están registrados en el grafo
        Envio envio = new Envio(codigo, remitente, destinatario,
                                Mapa.ObtenerNombre(o), Mapa.ObtenerNombre(d));

        Envios.Insertar(envio);
        TrieCodigos.Insertar(codigo);

        mensaje = "Envío " + codigo + " registrado correctamente.";

        if (!Regiones.EstanConectados(o, d))
        {
            mensaje += " Advertencia: por ahora " + envio.CiudadOrigen + " y " +
                       envio.CiudadDestino + " están en regiones distintas, no hay ruta disponible.";
        }

        return true;
    }

    public bool EliminarEnvio(string codigo)
    {
        codigo = codigo.Trim().ToUpper();

        if (!Envios.Eliminar(codigo))
        {
            return false;
        }

        TrieCodigos.Eliminar(codigo);
        return true;
    }

    // CÁLCULO DE RUTAS (Union-Find + Dijkstra)

    // Primero valida la conectividad con Union-Find y solo si las dos ciudades
    // están en la misma región ejecuta Dijkstra.
    public ResultadoRuta CalcularRuta(string origen, string destino, bool porCosto)
    {
        ResultadoRuta resultado = new ResultadoRuta();
        resultado.PorCosto = porCosto;

        int o = Mapa.BuscarIndice(origen);
        int d = Mapa.BuscarIndice(destino);

        if (o == -1 || d == -1)
        {
            resultado.Mensaje = "La ciudad de origen o de destino no existe.";
            return resultado;
        }

        if (o == d)
        {
            resultado.Mensaje = "El origen y el destino son la misma ciudad.";
            return resultado;
        }

        resultado.DatosValidos = true;

        // Primero se valida la conectividad con Union-Find
        if (!Regiones.EstanConectados(o, d))
        {
            resultado.Mensaje = Mapa.ObtenerNombre(o) + " y " + Mapa.ObtenerNombre(d) +
                                " pertenecen a regiones distintas. No existe ninguna ruta" +
                                " entre ellas, no se ejecuta Dijkstra.";
            return resultado;
        }

        resultado.MismaRegion = true;
        resultado.Dijkstra = Mapa.Dijkstra(o, d, porCosto);
        resultado.Camino = resultado.Dijkstra.Camino;

        // Pueden estar en la misma región pero sin camino por las rutas de un solo sentido
        if (resultado.Camino == null)
        {
            resultado.Mensaje = "Las ciudades están en la misma región, pero por las rutas de un solo" +
                                " sentido no hay forma de llegar de " + Mapa.ObtenerNombre(o) +
                                " a " + Mapa.ObtenerNombre(d) + ".";
            return resultado;
        }

        // Se calcula el costo y el tiempo total del camino encontrado
        int[] camino = resultado.Camino;
        for (int i = 0; i < camino.Length - 1; i++)
        {
            Arista arista = Mapa.BuscarArista(camino[i], camino[i + 1])!;
            resultado.CostoTotal += arista.Costo;
            resultado.TiempoTotal += arista.Tiempo;
        }

        resultado.Mensaje = "Ambas ciudades están en la misma región. Se ejecutó Dijkstra.";
        return resultado;
    }

    // Convierte un camino de índices en texto: Guatemala → Escuintla → ...
    public string TextoCamino(int[] camino)
    {
        string texto = Mapa.ObtenerNombre(camino[0]);

        for (int i = 1; i < camino.Length; i++)
        {
            texto += " → " + Mapa.ObtenerNombre(camino[i]);
        }

        return texto;
    }

    // CARGA DE ARCHIVOS

    // Lee todas las líneas de un archivo validando que exista
    private string[]? LeerArchivo(string ruta, ResultadoCarga resultado)
    {
        if (!File.Exists(ruta))
        {
            resultado.Mensajes.Agregar("No se encontró el archivo: " + ruta);
            return null;
        }

        try
        {
            string[] lineas = File.ReadAllLines(ruta, Encoding.UTF8);
            resultado.ArchivoLeido = true;
            return lineas;
        }
        catch (Exception ex)
        {
            resultado.Mensajes.Agregar("No se pudo leer el archivo: " + ex.Message);
            return null;
        }
    }

    // La primera línea puede ser un encabezado; si empieza con la palabra indicada se salta
    private bool EsEncabezado(string linea, string primeraColumna)
    {
        return linea.ToLower().StartsWith(primeraColumna);
    }

    // Formato: nombre[,latitud,longitud]  (las coordenadas son opcionales)
    public ResultadoCarga CargarCiudades(string ruta)
    {
        ResultadoCarga resultado = new ResultadoCarga(ruta);
        string[]? lineas = LeerArchivo(ruta, resultado);
        if (lineas == null) return resultado;

        for (int i = 0; i < lineas.Length; i++)
        {
            string linea = lineas[i].Trim();

            if (linea == "" || (i == 0 && EsEncabezado(linea, "nombre")))
            {
                continue;
            }

            string[] partes = linea.Split(',');
            string textoLatitud = partes.Length > 1 ? partes[1].Trim() : "";
            string textoLongitud = partes.Length > 2 ? partes[2].Trim() : "";
            double latitud = double.NaN;
            double longitud = double.NaN;

            // Si la línea trae coordenadas, las dos deben ser números válidos
            if (textoLatitud != "" || textoLongitud != "")
            {
                if (!ConvertirNumero(textoLatitud, out latitud) || !ConvertirNumero(textoLongitud, out longitud))
                {
                    resultado.Errores++;
                    resultado.Mensajes.Agregar("Línea " + (i + 1) + ": la latitud o la longitud no es un número válido.");
                    continue;
                }
            }

            if (RegistrarCiudad(partes[0], latitud, longitud, out string mensaje))
            {
                resultado.Cargados++;
            }
            else
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": " + mensaje);
            }
        }

        return resultado;
    }

    // Formato: origen,destino,costo,tiempo,sentido (D = doble sentido, U = un solo sentido)
    public ResultadoCarga CargarRutas(string ruta)
    {
        ResultadoCarga resultado = new ResultadoCarga(ruta);
        string[]? lineas = LeerArchivo(ruta, resultado);
        if (lineas == null) return resultado;

        for (int i = 0; i < lineas.Length; i++)
        {
            string linea = lineas[i].Trim();

            if (linea == "" || (i == 0 && EsEncabezado(linea, "origen")))
            {
                continue;
            }

            string[] partes = linea.Split(',');

            if (partes.Length < 4)
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": faltan datos (origen,destino,costo,tiempo,sentido).");
                continue;
            }

            if (!ConvertirNumero(partes[2], out double costo) || !ConvertirNumero(partes[3], out double tiempo))
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": el costo o el tiempo no es un número válido.");
                continue;
            }

            // Si no se indica el sentido, se toma como doble sentido
            bool dobleSentido = true;
            if (partes.Length >= 5 && partes[4].Trim().ToUpper() == "U")
            {
                dobleSentido = false;
            }

            if (RegistrarRuta(partes[0], partes[1], costo, tiempo, dobleSentido, out string mensaje))
            {
                resultado.Cargados++;
            }
            else
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": " + mensaje);
            }
        }

        return resultado;
    }

    // Formato: codigo,remitente,destinatario,origen,destino
    public ResultadoCarga CargarEnvios(string ruta)
    {
        ResultadoCarga resultado = new ResultadoCarga(ruta);
        string[]? lineas = LeerArchivo(ruta, resultado);
        if (lineas == null) return resultado;

        for (int i = 0; i < lineas.Length; i++)
        {
            string linea = lineas[i].Trim();

            if (linea == "" || (i == 0 && EsEncabezado(linea, "codigo")))
            {
                continue;
            }

            string[] partes = linea.Split(',');

            if (partes.Length < 5)
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": faltan datos (codigo,remitente,destinatario,origen,destino).");
                continue;
            }

            if (RegistrarEnvio(partes[0], partes[1], partes[2], partes[3], partes[4], out string mensaje))
            {
                resultado.Cargados++;
            }
            else
            {
                resultado.Errores++;
                resultado.Mensajes.Agregar("Línea " + (i + 1) + ": " + mensaje);
            }
        }

        return resultado;
    }

    // Carga los tres archivos de una carpeta en el orden correcto:
    // primero ciudades, luego rutas y por último envíos.
    // Devuelve null si la carpeta no existe.
    public ResultadoCarga[]? CargarCarpeta(string carpeta)
    {
        if (!Directory.Exists(carpeta))
        {
            return null;
        }

        ResultadoCarga[] resultados = new ResultadoCarga[3];
        resultados[0] = CargarCiudades(Path.Combine(carpeta, "ciudades.csv"));
        resultados[1] = CargarRutas(Path.Combine(carpeta, "rutas.csv"));
        resultados[2] = CargarEnvios(Path.Combine(carpeta, "envios.csv"));
        return resultados;
    }

    // GUARDAR EN ARCHIVOS

    public bool GuardarDatos(string carpeta, out string mensaje)
    {
        try
        {
            Directory.CreateDirectory(carpeta);

            // Ciudades (las coordenadas solo se escriben si la ciudad las tiene)
            using (StreamWriter escritor = new StreamWriter(Path.Combine(carpeta, "ciudades.csv"), false, Encoding.UTF8))
            {
                escritor.WriteLine("nombre,latitud,longitud");
                for (int i = 0; i < Mapa.Cantidad; i++)
                {
                    string linea = Mapa.ObtenerNombre(i);
                    if (Mapa.TieneCoordenadas(i))
                    {
                        linea += "," + Mapa.ObtenerLatitud(i).ToString(CultureInfo.InvariantCulture) +
                                 "," + Mapa.ObtenerLongitud(i).ToString(CultureInfo.InvariantCulture);
                    }
                    escritor.WriteLine(linea);
                }
            }

            // Rutas: si la ida y la vuelta tienen los mismos valores se guarda una sola línea con D
            using (StreamWriter escritor = new StreamWriter(Path.Combine(carpeta, "rutas.csv"), false, Encoding.UTF8))
            {
                escritor.WriteLine("origen,destino,costo,tiempo,sentido");
                for (int i = 0; i < Mapa.Cantidad; i++)
                {
                    Arista? arista = Mapa.PrimeraArista(i);
                    while (arista != null)
                    {
                        Arista? vuelta = Mapa.BuscarArista(arista.Destino, i);
                        bool esDoble = vuelta != null &&
                                       vuelta.Costo == arista.Costo &&
                                       vuelta.Tiempo == arista.Tiempo;

                        // La ruta doble se escribe solo una vez
                        if (!esDoble || i < arista.Destino)
                        {
                            escritor.WriteLine(
                                Mapa.ObtenerNombre(i) + "," +
                                Mapa.ObtenerNombre(arista.Destino) + "," +
                                arista.Costo.ToString(CultureInfo.InvariantCulture) + "," +
                                arista.Tiempo.ToString(CultureInfo.InvariantCulture) + "," +
                                (esDoble ? "D" : "U"));
                        }

                        arista = arista.Siguiente;
                    }
                }
            }

            // Envíos
            using (StreamWriter escritor = new StreamWriter(Path.Combine(carpeta, "envios.csv"), false, Encoding.UTF8))
            {
                escritor.WriteLine("codigo,remitente,destinatario,origen,destino");
                Envio[] envios = Envios.ObtenerEnvios().ConvertirArreglo();
                foreach (Envio envio in envios)
                {
                    escritor.WriteLine(envio.ALineaCsv());
                }
            }

            mensaje = "Datos guardados en: " + carpeta;
            return true;
        }
        catch (Exception ex)
        {
            mensaje = "No se pudieron guardar los datos: " + ex.Message;
            return false;
        }
    }
}
