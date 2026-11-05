using System;
using System.Collections.Generic;

// Estructura: GRAFO (dirigido y ponderado)
// Responsable: Ángel
//
// Los vértices son las ciudades y las aristas son las rutas de transporte.
// Cada ruta tiene dos pesos: costo (en quetzales) y tiempo (en horas).
// El grafo es DIRIGIDO porque algunas rutas son de un solo sentido;
// una ruta de doble sentido se guarda como dos aristas (ida y vuelta).
//
// Representación: lista de adyacencia propia.
// ciudades[i] guarda el nombre de la ciudad i y adyacencia[i] apunta a la
// primera arista que sale de ella (lista enlazada de aristas).
// Cada ciudad puede tener coordenadas (latitud y longitud) opcionales;
// solo se usan para dibujar el mapa en la interfaz gráfica.

// NODO ARISTA
public class Arista
{
    public int Destino;
    public double Costo;
    public double Tiempo;
    public Arista? Siguiente;

    public Arista(int destino, double costo, double tiempo)
    {
        Destino = destino;
        Costo = costo;
        Tiempo = tiempo;
        Siguiente = null;
    }
}

public class Grafo
{
    private string[] ciudades;
    // Coordenadas de cada ciudad; double.NaN significa que no tiene
    private double[] latitudes;
    private double[] longitudes;
    private Arista?[] adyacencia;
    private int cantidad;

    public Grafo(int capacidadInicial)
    {
        ciudades = new string[capacidadInicial];
        latitudes = new double[capacidadInicial];
        longitudes = new double[capacidadInicial];
        adyacencia = new Arista?[capacidadInicial];
        cantidad = 0;
    }

    public int Cantidad
    {
        get { return cantidad; }
    }

    public string ObtenerNombre(int indice)
    {
        return ciudades[indice];
    }

    public double ObtenerLatitud(int indice)
    {
        return latitudes[indice];
    }

    public double ObtenerLongitud(int indice)
    {
        return longitudes[indice];
    }

    public bool TieneCoordenadas(int indice)
    {
        return !double.IsNaN(latitudes[indice]) && !double.IsNaN(longitudes[indice]);
    }

    public Arista? PrimeraArista(int indice)
    {
        return adyacencia[indice];
    }

    // Devuelve el índice de la ciudad (sin importar mayúsculas) o -1 si no existe
    public int BuscarIndice(string nombre)
    {
        nombre = nombre.Trim();

        for (int i = 0; i < cantidad; i++)
        {
            if (string.Equals(ciudades[i], nombre, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    // Registra una ciudad sin coordenadas
    public int AgregarCiudad(string nombre)
    {
        return AgregarCiudad(nombre, double.NaN, double.NaN);
    }

    // Registra una ciudad (vértice). Devuelve su índice o -1 si ya existe.
    public int AgregarCiudad(string nombre, double latitud, double longitud)
    {
        nombre = nombre.Trim();

        if (nombre == "" || BuscarIndice(nombre) != -1)
        {
            return -1;
        }

        // Si los arreglos se llenaron, se duplica su tamaño
        if (cantidad == ciudades.Length)
        {
            Ampliar();
        }

        ciudades[cantidad] = nombre;
        latitudes[cantidad] = latitud;
        longitudes[cantidad] = longitud;
        adyacencia[cantidad] = null;
        cantidad++;

        return cantidad - 1;
    }

    private void Ampliar()
    {
        int nuevoTamanio = ciudades.Length * 2;
        string[] nuevasCiudades = new string[nuevoTamanio];
        double[] nuevasLatitudes = new double[nuevoTamanio];
        double[] nuevasLongitudes = new double[nuevoTamanio];
        Arista?[] nuevaAdyacencia = new Arista?[nuevoTamanio];

        for (int i = 0; i < cantidad; i++)
        {
            nuevasCiudades[i] = ciudades[i];
            nuevasLatitudes[i] = latitudes[i];
            nuevasLongitudes[i] = longitudes[i];
            nuevaAdyacencia[i] = adyacencia[i];
        }

        ciudades = nuevasCiudades;
        latitudes = nuevasLatitudes;
        longitudes = nuevasLongitudes;
        adyacencia = nuevaAdyacencia;
    }

    // Busca la arista origen → destino; devuelve null si no existe
    public Arista? BuscarArista(int origen, int destino)
    {
        Arista? actual = adyacencia[origen];

        while (actual != null)
        {
            if (actual.Destino == destino)
            {
                return actual;
            }
            actual = actual.Siguiente;
        }

        return null;
    }

    // Agrega una ruta en UN solo sentido (origen → destino).
    // Devuelve false si esa ruta ya existe.
    public bool AgregarRuta(int origen, int destino, double costo, double tiempo)
    {
        if (BuscarArista(origen, destino) != null)
        {
            return false;
        }

        // Se inserta al final para conservar el orden en que se registraron
        Arista nueva = new Arista(destino, costo, tiempo);

        if (adyacencia[origen] == null)
        {
            adyacencia[origen] = nueva;
        }
        else
        {
            Arista actual = adyacencia[origen]!;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nueva;
        }

        return true;
    }

    // Elimina la ruta origen → destino. Devuelve false si no existe.
    public bool EliminarRuta(int origen, int destino)
    {
        Arista? anterior = null;
        Arista? actual = adyacencia[origen];

        while (actual != null)
        {
            if (actual.Destino == destino)
            {
                if (anterior == null)
                {
                    adyacencia[origen] = actual.Siguiente;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;
                }
                return true;
            }

            anterior = actual;
            actual = actual.Siguiente;
        }

        return false;
    }

    // RECORRIDO BFS (en anchura)
    // Queue<int> se usa únicamente como estructura de control del orden de visita
    public ResultadoRecorrido BFS(int inicio)
    {
        Queue<int> cola = new Queue<int>();
        bool[] visitados = new bool[cantidad];
        ListaEnlazada<int> orden = new ListaEnlazada<int>();
        ListaEnlazada<string> estados = new ListaEnlazada<string>();

        cola.Enqueue(inicio);
        visitados[inicio] = true;

        while (cola.Count > 0)
        {
            int actual = cola.Dequeue();
            orden.Agregar(actual);

            // Se agregan a la cola los vecinos que todavía no se han visitado
            Arista? arista = adyacencia[actual];
            while (arista != null)
            {
                if (!visitados[arista.Destino])
                {
                    visitados[arista.Destino] = true;
                    cola.Enqueue(arista.Destino);
                }
                arista = arista.Siguiente;
            }

            // Se guarda cómo quedó la cola después de esta visita
            estados.Agregar(TextoCola(cola));
        }

        return new ResultadoRecorrido(orden.ConvertirArreglo(), estados.ConvertirArreglo());
    }

    private string TextoCola(Queue<int> cola)
    {
        string texto = "";
        foreach (int indice in cola)
        {
            if (texto != "")
            {
                texto += ", ";
            }
            texto += ciudades[indice];
        }
        return texto;
    }

    // RECORRIDO DFS (en profundidad), de forma recursiva
    public ResultadoRecorrido DFS(int inicio)
    {
        bool[] visitados = new bool[cantidad];
        ListaEnlazada<int> orden = new ListaEnlazada<int>();

        DFSRecursivo(inicio, visitados, orden);

        return new ResultadoRecorrido(orden.ConvertirArreglo(), null);
    }

    private void DFSRecursivo(int actual, bool[] visitados, ListaEnlazada<int> orden)
    {
        visitados[actual] = true;
        orden.Agregar(actual);

        Arista? arista = adyacencia[actual];
        while (arista != null)
        {
            if (!visitados[arista.Destino])
            {
                DFSRecursivo(arista.Destino, visitados, orden);
            }
            arista = arista.Siguiente;
        }
    }

    // ALGORITMO DE DIJKSTRA
    // porCosto = true  -> ruta más económica
    // porCosto = false -> ruta más rápida
    // Devuelve la tabla de distancias y el camino (null si no hay camino).
    public ResultadoDijkstra Dijkstra(int origen, int destino, bool porCosto)
    {
        // Menor distancia conocida desde el origen hasta cada ciudad
        double[] distancias = new double[cantidad];
        // Ciudad anterior en el mejor camino
        int[] anteriores = new int[cantidad];
        // Ciudades ya procesadas
        bool[] visitados = new bool[cantidad];

        // Inicialmente no se puede llegar a ninguna ciudad (infinito)
        for (int i = 0; i < cantidad; i++)
        {
            distancias[i] = double.MaxValue;
            anteriores[i] = -1;
            visitados[i] = false;
        }

        // El origen tiene distancia cero
        distancias[origen] = 0;

        for (int paso = 0; paso < cantidad; paso++)
        {
            // Buscamos la ciudad no visitada con la menor distancia conocida
            int actual = -1;
            double menorDistancia = double.MaxValue;

            for (int i = 0; i < cantidad; i++)
            {
                if (!visitados[i] && distancias[i] < menorDistancia)
                {
                    menorDistancia = distancias[i];
                    actual = i;
                }
            }

            // Si no queda ninguna ciudad alcanzable, terminamos
            if (actual == -1)
            {
                break;
            }

            visitados[actual] = true;

            // Revisamos sus vecinos
            Arista? arista = adyacencia[actual];
            while (arista != null)
            {
                double peso = porCosto ? arista.Costo : arista.Tiempo;

                // Costo de llegar al vecino pasando por la ciudad actual
                double nuevaDistancia = distancias[actual] + peso;

                // Si encontramos un camino mejor, lo guardamos
                if (nuevaDistancia < distancias[arista.Destino])
                {
                    distancias[arista.Destino] = nuevaDistancia;
                    anteriores[arista.Destino] = actual;
                }

                arista = arista.Siguiente;
            }
        }

        if (distancias[destino] == double.MaxValue)
        {
            return new ResultadoDijkstra(origen, porCosto, distancias, anteriores, null);
        }

        // Reconstruimos el camino yendo hacia atrás con los anteriores
        int longitud = 0;
        int nodo = destino;
        while (nodo != -1)
        {
            longitud++;
            nodo = anteriores[nodo];
        }

        int[] camino = new int[longitud];
        nodo = destino;
        for (int i = longitud - 1; i >= 0; i--)
        {
            camino[i] = nodo;
            nodo = anteriores[nodo];
        }

        return new ResultadoDijkstra(origen, porCosto, distancias, anteriores, camino);
    }
}
