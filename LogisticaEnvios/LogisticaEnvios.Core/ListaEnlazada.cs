// Lista enlazada propia
// Se usa como apoyo para devolver varios resultados (sugerencias del Trie,
// envíos de la tabla hash, etc.) sin usar List<T> de .NET.

public class NodoLista<T>
{
    public T Dato;
    public NodoLista<T>? Siguiente;

    public NodoLista(T dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada<T>
{
    private NodoLista<T>? cabeza;
    private NodoLista<T>? cola;
    private int cantidad;

    public ListaEnlazada()
    {
        cabeza = null;
        cola = null;
        cantidad = 0;
    }

    public int Cantidad
    {
        get { return cantidad; }
    }

    // Agrega al final en O(1) gracias a la referencia a la cola
    public void Agregar(T dato)
    {
        NodoLista<T> nuevo = new NodoLista<T>(dato);

        if (cabeza == null)
        {
            cabeza = nuevo;
            cola = nuevo;
        }
        else
        {
            cola!.Siguiente = nuevo;
            cola = nuevo;
        }

        cantidad++;
    }

    // Copia los elementos a un arreglo para poder recorrerlos con índices
    public T[] ConvertirArreglo()
    {
        T[] arreglo = new T[cantidad];
        NodoLista<T>? actual = cabeza;
        int i = 0;

        while (actual != null)
        {
            arreglo[i] = actual.Dato;
            i++;
            actual = actual.Siguiente;
        }

        return arreglo;
    }
}
