// Estructura: UNION-FIND (conjuntos disjuntos)
// Responsable: Ángel
//
// Agrupa las ciudades en REGIONES DE DISTRIBUCIÓN: dos ciudades están en la
// misma región si existe alguna cadena de rutas que las conecte.
// Se usa para:
//   1. Avisar, antes de asignar una ruta nueva, si las dos ciudades ya
//      pertenecen a la misma región o si la ruta unirá dos regiones.
//   2. Validar la conectividad ANTES de ejecutar Dijkstra: si las ciudades
//      están en regiones distintas, no se ejecuta el algoritmo.
//
// Optimizaciones: Path Compression en Find y Union by Rank en Union.
// Cada ciudad usa el mismo índice que tiene en el grafo.
public class UnionFind
{
    // Guarda el padre de cada elemento
    private int[] padre;
    // Guarda el rango de cada árbol
    private int[] rango;
    private int cantidad;

    public UnionFind(int capacidadInicial)
    {
        padre = new int[capacidadInicial];
        rango = new int[capacidadInicial];
        cantidad = 0;
    }

    public int Cantidad
    {
        get { return cantidad; }
    }

    // Agrega un nuevo elemento que inicia en su propio conjunto
    public void AgregarElemento()
    {
        if (cantidad == padre.Length)
        {
            Ampliar();
        }

        padre[cantidad] = cantidad;
        rango[cantidad] = 0;
        cantidad++;
    }

    private void Ampliar()
    {
        int nuevoTamanio = padre.Length * 2;
        int[] nuevoPadre = new int[nuevoTamanio];
        int[] nuevoRango = new int[nuevoTamanio];

        for (int i = 0; i < cantidad; i++)
        {
            nuevoPadre[i] = padre[i];
            nuevoRango[i] = rango[i];
        }

        padre = nuevoPadre;
        rango = nuevoRango;
    }

    // Vuelve a dejar cada elemento en su propio conjunto.
    // Se usa cuando se elimina una ruta, porque Union-Find no puede separar
    // conjuntos; por eso las regiones se reconstruyen a partir del grafo.
    public void Reiniciar()
    {
        for (int i = 0; i < cantidad; i++)
        {
            padre[i] = i;
            rango[i] = 0;
        }
    }

    // Busca el representante del conjunto.
    // Utiliza Path Compression.
    public int Find(int x)
    {
        if (padre[x] != x)
        {
            // Busca la raíz y hace que el elemento apunte directamente hacia ella
            padre[x] = Find(padre[x]);
        }

        return padre[x];
    }

    // Une dos conjuntos.
    // Utiliza Union by Rank. Devuelve false si ya estaban en el mismo conjunto.
    public bool Union(int x, int y)
    {
        // Buscamos las raíces de ambos elementos
        int raizX = Find(x);
        int raizY = Find(y);

        // Si tienen la misma raíz, ya pertenecen al mismo conjunto
        if (raizX == raizY)
        {
            return false;
        }

        // El árbol de menor rango se coloca debajo del árbol de mayor rango
        if (rango[raizX] < rango[raizY])
        {
            padre[raizX] = raizY;
        }
        else if (rango[raizX] > rango[raizY])
        {
            padre[raizY] = raizX;
        }
        else
        {
            // Si tienen el mismo rango, la raíz es la del primer elemento
            padre[raizY] = raizX;

            // El rango de la nueva raíz aumenta
            rango[raizX]++;
        }

        return true;
    }

    // Comprueba si dos elementos pertenecen al mismo conjunto
    public bool EstanConectados(int x, int y)
    {
        return Find(x) == Find(y);
    }

    public int ContarRegiones()
    {
        int regiones = 0;
        for (int i = 0; i < cantidad; i++)
        {
            if (Find(i) == i)
            {
                regiones++;
            }
        }
        return regiones;
    }

    // Devuelve, para cada elemento, el número de su región (1, 2, 3...).
    // Cada raíz representa una región; se numeran en el orden de su índice.
    // Lo usan la consola para listar las regiones y la interfaz gráfica
    // para pintar cada región de un color.
    public int[] NumerarRegiones()
    {
        int[] numeroDeRaiz = new int[cantidad];
        int[] resultado = new int[cantidad];
        int contador = 0;

        for (int raiz = 0; raiz < cantidad; raiz++)
        {
            if (Find(raiz) == raiz)
            {
                contador++;
                numeroDeRaiz[raiz] = contador;
            }
        }

        for (int i = 0; i < cantidad; i++)
        {
            resultado[i] = numeroDeRaiz[Find(i)];
        }

        return resultado;
    }

    // Acceso a los arreglos tal como están en memoria (para imprimirlos)
    public int ObtenerPadre(int indice)
    {
        return padre[indice];
    }

    public int ObtenerRango(int indice)
    {
        return rango[indice];
    }
}
