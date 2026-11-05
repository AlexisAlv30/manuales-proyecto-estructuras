// Estructura: TABLA HASH
// Responsable: Ángel
//
// Guarda los envíos usando como clave el código de rastreo.
// Se eligió porque la operación más frecuente es localizar un envío exacto
// por su código, y la tabla hash lo hace en O(1) en promedio.
//
// Técnica: direccionamiento abierto con sondeo lineal.
// Eliminación: se marca la posición como ELIMINADA para no cortar el sondeo.
// Rehashing: cuando el factor de carga proyectado supera 0.70, el tamaño pasa
// a ser el primer primo válido mayor o igual a tamaño * 2 + 1 (7 → 17 → 37 → 79...)
// y se reinsertan los envíos.
public class TablaHash
{
    // Multiplicador de la función hash
    private const int Base = 31;

    private Envio?[] tabla;
    private bool[] eliminado;
    private int cantidad;
    private int tamanio;
    private int cantidadRehashing;

    public TablaHash(int tamanioInicial)
    {
        tamanio = TamanioValido(tamanioInicial);
        tabla = new Envio?[tamanio];
        eliminado = new bool[tamanio];
        cantidad = 0;
        cantidadRehashing = 0;
    }

    public int Cantidad
    {
        get { return cantidad; }
    }

    public int Tamanio
    {
        get { return tamanio; }
    }

    public int CantidadRehashing
    {
        get { return cantidadRehashing; }
    }

    // Función hash para texto: convierte el código (ej. ENV-1001) en un número
    // y luego aplica h(k) = k mod m
    private int FuncionHash(string codigo)
    {
        int valor = 0;
        foreach (char c in codigo)
        {
            valor = (valor * Base + c) % tamanio;
        }
        return valor;
    }

    // Devuelve el primer tamaño válido mayor o igual a n.
    // El tamaño debe ser primo para repartir mejor los códigos, y además
    // Base mod m no puede ser 0 ni 1:
    //   - si 31 mod m = 0 (m = 31), la posición dependería solo del último carácter;
    //   - si 31 mod m = 1 (m = 3, 5, 15...), dependería solo de la suma de los caracteres.
    // En ambos casos muchos códigos caerían en la misma posición.
    private static int TamanioValido(int n)
    {
        if (n < 3)
        {
            n = 3;
        }

        while (!EsPrimo(n) || Base % n == 0 || Base % n == 1)
        {
            n++;
        }

        return n;
    }

    private static bool EsPrimo(int n)
    {
        if (n < 2)
        {
            return false;
        }

        for (int divisor = 2; divisor * divisor <= n; divisor++)
        {
            if (n % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

    public double FactorCarga()
    {
        return (double)cantidad / tamanio;
    }

    // Inserta un envío. Devuelve false si el código ya existe.
    public bool Insertar(Envio envio)
    {
        // El código de rastreo es único
        if (Buscar(envio.CodigoRastreo) != null)
        {
            return false;
        }

        // Factor de carga proyectado con el nuevo envío
        double factorProyectado = (double)(cantidad + 1) / tamanio;

        if (factorProyectado > 0.70)
        {
            Rehashing();
        }

        InsertarSinRehashing(envio);
        return true;
    }

    private void InsertarSinRehashing(Envio envio)
    {
        int indiceInicial = FuncionHash(envio.CodigoRastreo);

        // Sondeo lineal: se usa la primera posición vacía o eliminada
        for (int intento = 0; intento < tamanio; intento++)
        {
            int indice = (indiceInicial + intento) % tamanio;

            if (tabla[indice] == null)
            {
                tabla[indice] = envio;
                eliminado[indice] = false;
                cantidad++;
                return;
            }
        }
    }

    private void Rehashing()
    {
        Envio?[] tablaAnterior = tabla;

        // Nuevo tamaño = primer primo válido desde tamaño anterior * 2 + 1
        tamanio = TamanioValido(tamanio * 2 + 1);
        tabla = new Envio?[tamanio];
        eliminado = new bool[tamanio];
        cantidad = 0;
        cantidadRehashing++;

        // Se reinsertan los envíos para recalcular su índice con el nuevo tamaño.
        // Las posiciones eliminadas no se copian, así la tabla queda limpia.
        foreach (Envio? envio in tablaAnterior)
        {
            if (envio != null)
            {
                InsertarSinRehashing(envio);
            }
        }
    }

    public Envio? Buscar(string codigo)
    {
        int indice = BuscarPosicion(codigo);

        if (indice == -1)
        {
            return null;
        }

        return tabla[indice];
    }

    // Devuelve la posición de la tabla donde está el código, o -1 si no existe
    public int BuscarPosicion(string codigo)
    {
        int indiceInicial = FuncionHash(codigo);

        // Se sigue el mismo sondeo lineal de la inserción
        for (int intento = 0; intento < tamanio; intento++)
        {
            int indice = (indiceInicial + intento) % tamanio;

            if (tabla[indice] != null && tabla[indice]!.CodigoRastreo == codigo)
            {
                return indice;
            }

            // Si la posición nunca fue utilizada, el código no existe
            if (tabla[indice] == null && !eliminado[indice])
            {
                return -1;
            }
        }

        return -1;
    }

    // Posición que calcula la función hash antes del sondeo lineal: h(k) = k mod m
    public int PosicionInicial(string codigo)
    {
        return FuncionHash(codigo);
    }

    // Elimina un envío. Devuelve false si no existe.
    public bool Eliminar(string codigo)
    {
        int indiceInicial = FuncionHash(codigo);

        for (int intento = 0; intento < tamanio; intento++)
        {
            int indice = (indiceInicial + intento) % tamanio;

            if (tabla[indice] != null && tabla[indice]!.CodigoRastreo == codigo)
            {
                tabla[indice] = null;

                // Marcamos la posición como eliminada
                eliminado[indice] = true;
                cantidad--;
                return true;
            }

            if (tabla[indice] == null && !eliminado[indice])
            {
                return false;
            }
        }

        return false;
    }

    // Devuelve todos los envíos guardados (se usa para guardar el archivo)
    public ListaEnlazada<Envio> ObtenerEnvios()
    {
        ListaEnlazada<Envio> resultado = new ListaEnlazada<Envio>();

        for (int i = 0; i < tamanio; i++)
        {
            if (tabla[i] != null)
            {
                resultado.Agregar(tabla[i]!);
            }
        }

        return resultado;
    }

    // Acceso a cada posición para poder imprimir la tabla completa
    // (la consola la muestra como texto y la interfaz gráfica en una tabla)
    public Envio? ObtenerPosicion(int indice)
    {
        return tabla[indice];
    }

    public bool PosicionEliminada(int indice)
    {
        return eliminado[indice];
    }
}
