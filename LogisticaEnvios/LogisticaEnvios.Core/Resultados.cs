// CLASES DE RESULTADO
// Responsable: Ángel
//
// Las estructuras y el sistema no escriben en la consola: devuelven estos
// objetos con los datos del resultado. Así la aplicación de consola los
// imprime como texto y la interfaz gráfica los muestra en tablas o los
// dibuja en el mapa, sin repetir la lógica.

// Resultado de un recorrido BFS o DFS
public class ResultadoRecorrido
{
    // Índices de las ciudades en el orden en que se visitaron
    public int[] Orden;

    // Solo para BFS: cómo quedó la cola después de visitar cada ciudad
    public string[]? EstadoCola;

    public ResultadoRecorrido(int[] orden, string[]? estadoCola)
    {
        Orden = orden;
        EstadoCola = estadoCola;
    }
}

// Resultado completo del algoritmo de Dijkstra
public class ResultadoDijkstra
{
    public int Origen;
    public bool PorCosto;

    // Menor distancia conocida desde el origen hasta cada ciudad.
    // double.MaxValue significa que no se puede llegar (infinito).
    public double[] Distancias;

    // Ciudad anterior en el mejor camino (-1 si no tiene)
    public int[] Anteriores;

    // Camino desde el origen hasta el destino pedido, o null si no hay camino
    public int[]? Camino;

    public ResultadoDijkstra(int origen, bool porCosto, double[] distancias,
                             int[] anteriores, int[]? camino)
    {
        Origen = origen;
        PorCosto = porCosto;
        Distancias = distancias;
        Anteriores = anteriores;
        Camino = camino;
    }
}

// Resultado de calcular la ruta entre dos ciudades (Union-Find + Dijkstra)
public class ResultadoRuta
{
    // false si alguna ciudad no existe o si el origen y el destino son iguales
    public bool DatosValidos;

    // Respuesta del Union-Find: true si están en la misma región
    public bool MismaRegion;

    public bool PorCosto;

    // Datos de Dijkstra; queda en null si no se ejecutó
    public ResultadoDijkstra? Dijkstra;

    // Camino encontrado (null si no hay forma de llegar)
    public int[]? Camino;

    public double CostoTotal;
    public double TiempoTotal;

    // Explicación del resultado para mostrarla al usuario
    public string Mensaje = "";
}

// Resultado de cargar un archivo .csv
public class ResultadoCarga
{
    public string Archivo;
    public bool ArchivoLeido;
    public int Cargados;
    public int Errores;

    // Avisos de las líneas con error (o del archivo que no se encontró)
    public ListaEnlazada<string> Mensajes;

    public ResultadoCarga(string archivo)
    {
        Archivo = archivo;
        ArchivoLeido = false;
        Cargados = 0;
        Errores = 0;
        Mensajes = new ListaEnlazada<string>();
    }
}
