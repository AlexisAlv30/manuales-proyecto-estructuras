// Estructura: TRIE (árbol de prefijos)
// Responsable: Ángel
//
// Se usa para el AUTOCOMPLETADO: sugiere nombres de ciudad (y códigos de
// rastreo) a partir de las primeras letras que escribe el usuario.
// Las claves se guardan en minúsculas para que "que" encuentre "Quetzaltenango",
// pero en el nodo final se guarda el texto original para mostrarlo bien.
//
// Como no se permite Dictionary, los hijos de cada nodo se guardan como una
// lista enlazada: PrimerHijo apunta al primer hijo y Hermano al siguiente.
// Los hermanos se mantienen ordenados por letra, así las sugerencias
// salen en orden alfabético.

// NODO DEL TRIE
public class NodoTrie
{
    public char Letra;
    public NodoTrie? PrimerHijo;
    public NodoTrie? Hermano;
    // Indica si aquí termina una palabra
    public bool EsFinPalabra;
    // Texto original de la palabra que termina en este nodo
    public string? PalabraOriginal;

    public NodoTrie(char letra)
    {
        Letra = letra;
        PrimerHijo = null;
        Hermano = null;
        EsFinPalabra = false;
        PalabraOriginal = null;
    }
}

public class Trie
{
    private NodoTrie raiz;
    private int cantidad;

    public Trie()
    {
        raiz = new NodoTrie(' ');
        cantidad = 0;
    }

    public int Cantidad
    {
        get { return cantidad; }
    }

    // Nodo raíz; la interfaz gráfica lo recorre para dibujar el árbol
    public NodoTrie Raiz
    {
        get { return raiz; }
    }

    // Busca entre los hijos de un nodo el que tiene la letra indicada
    private NodoTrie? BuscarHijo(NodoTrie nodo, char letra)
    {
        NodoTrie? actual = nodo.PrimerHijo;

        while (actual != null)
        {
            if (actual.Letra == letra)
            {
                return actual;
            }

            // Como los hermanos están ordenados, si ya nos pasamos la letra no existe
            if (actual.Letra > letra)
            {
                return null;
            }

            actual = actual.Hermano;
        }

        return null;
    }

    // Devuelve el hijo con esa letra; si no existe lo crea en su posición ordenada
    private NodoTrie ObtenerOCrearHijo(NodoTrie padre, char letra)
    {
        NodoTrie? anterior = null;
        NodoTrie? actual = padre.PrimerHijo;

        while (actual != null && actual.Letra < letra)
        {
            anterior = actual;
            actual = actual.Hermano;
        }

        if (actual != null && actual.Letra == letra)
        {
            return actual;
        }

        NodoTrie nuevo = new NodoTrie(letra);
        nuevo.Hermano = actual;

        if (anterior == null)
        {
            padre.PrimerHijo = nuevo;
        }
        else
        {
            anterior.Hermano = nuevo;
        }

        return nuevo;
    }

    // Inserta una palabra reutilizando los caracteres que ya existen
    public void Insertar(string palabra)
    {
        string original = palabra.Trim();
        string clave = original.ToLower();

        if (clave == "")
        {
            return;
        }

        NodoTrie actual = raiz;
        foreach (char letra in clave)
        {
            actual = ObtenerOCrearHijo(actual, letra);
        }

        if (!actual.EsFinPalabra)
        {
            cantidad++;
        }

        actual.EsFinPalabra = true;
        actual.PalabraOriginal = original;
    }

    // Recorre el Trie letra por letra hasta el último carácter del prefijo
    private NodoTrie? BuscarNodo(string prefijo)
    {
        NodoTrie actual = raiz;

        foreach (char letra in prefijo)
        {
            NodoTrie? siguiente = BuscarHijo(actual, letra);
            if (siguiente == null)
            {
                return null;
            }
            actual = siguiente;
        }

        return actual;
    }

    // Devuelve true solo si la palabra completa existe
    public bool Buscar(string palabra)
    {
        NodoTrie? nodo = BuscarNodo(palabra.Trim().ToLower());
        return nodo != null && nodo.EsFinPalabra;
    }

    // Devuelve true si existe algún camino con ese prefijo
    public bool EmpiezaCon(string prefijo)
    {
        return BuscarNodo(prefijo.Trim().ToLower()) != null;
    }

    // Devuelve todas las palabras que comienzan con el prefijo
    public ListaEnlazada<string> SugerirPorPrefijo(string prefijo)
    {
        ListaEnlazada<string> resultado = new ListaEnlazada<string>();
        NodoTrie? nodo = BuscarNodo(prefijo.Trim().ToLower());

        if (nodo != null)
        {
            Recolectar(nodo, resultado);
        }

        return resultado;
    }

    // Desde el nodo del prefijo baja por todas las ramas guardando las palabras
    private void Recolectar(NodoTrie nodo, ListaEnlazada<string> resultado)
    {
        if (nodo.EsFinPalabra)
        {
            resultado.Agregar(nodo.PalabraOriginal!);
        }

        NodoTrie? hijo = nodo.PrimerHijo;
        while (hijo != null)
        {
            Recolectar(hijo, resultado);
            hijo = hijo.Hermano;
        }
    }

    // Elimina una palabra. Los nodos que quedan sin uso se quitan del Trie.
    public bool Eliminar(string palabra)
    {
        string clave = palabra.Trim().ToLower();

        if (clave == "")
        {
            return false;
        }

        // Guardamos el camino desde la raíz hasta el último nodo
        NodoTrie[] camino = new NodoTrie[clave.Length + 1];
        camino[0] = raiz;

        for (int i = 0; i < clave.Length; i++)
        {
            NodoTrie? siguiente = BuscarHijo(camino[i], clave[i]);
            if (siguiente == null)
            {
                return false;
            }
            camino[i + 1] = siguiente;
        }

        NodoTrie ultimo = camino[clave.Length];

        if (!ultimo.EsFinPalabra)
        {
            return false;
        }

        ultimo.EsFinPalabra = false;
        ultimo.PalabraOriginal = null;
        cantidad--;

        // Subimos quitando los nodos que no tienen hijos ni terminan otra palabra
        for (int i = clave.Length; i > 0; i--)
        {
            NodoTrie nodo = camino[i];

            if (nodo.PrimerHijo != null || nodo.EsFinPalabra)
            {
                break;
            }

            QuitarHijo(camino[i - 1], nodo);
        }

        return true;
    }

    private void QuitarHijo(NodoTrie padre, NodoTrie hijo)
    {
        if (padre.PrimerHijo == hijo)
        {
            padre.PrimerHijo = hijo.Hermano;
            return;
        }

        NodoTrie? actual = padre.PrimerHijo;
        while (actual != null && actual.Hermano != hijo)
        {
            actual = actual.Hermano;
        }

        if (actual != null)
        {
            actual.Hermano = hijo.Hermano;
        }
    }

    // Devuelve todas las palabras en orden alfabético (prefijo vacío)
    public ListaEnlazada<string> ObtenerTodas()
    {
        return SugerirPorPrefijo("");
    }
}
