using System;
using System.IO;
using System.Text;

// PROYECTO 2 - ESTRUCTURA DE DATOS II
// Escenario: Logística de envíos
//
// Responsable de todo el código: Ángel
// Responsable de la interfaz grafica: Johann
// Responsable de manual de usario y manual tecnico: Alexis
//
// Estructura de la solución:
//   LogisticaEnvios.Core     -> biblioteca con las estructuras y su integración
//     Tabla Hash         (TablaHash.cs)
//     Grafo              (Grafo.cs)
//     Union-Find         (UnionFind.cs)
//     Trie               (Trie.cs)
//     Integración        (SistemaLogistica.cs)
//   LogisticaEnvios.Consola  -> este menú de consola (Program.cs)
//   LogisticaEnvios.GUI      -> interfaz gráfica en Windows Forms
//
// Las estructuras no imprimen nada: devuelven datos y aquí se convierten en
// texto para la consola (la interfaz gráfica usa esos mismos datos).

class Program
{
    static SistemaLogistica sistema = new SistemaLogistica();

    // Carpeta datos del proyecto (ahí se leen y se guardan los .csv)
    static string carpetaPorDefecto = SistemaLogistica.CarpetaDatosPorDefecto();

    static void Main(string[] args)
    {
        // Para que se muestren bien las tildes y los símbolos → y ∞
        Console.OutputEncoding = Encoding.UTF8;

        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# LOGÍSTICA DE ENVÍOS");
            Console.WriteLine("Ciudades: " + sistema.Mapa.Cantidad +
                              " | Envíos: " + sistema.Envios.Cantidad +
                              " | Regiones: " + sistema.Regiones.ContarRegiones());
            Console.WriteLine();
            Console.WriteLine("1. Cargar datos desde archivos");
            Console.WriteLine("2. Ciudades y rutas (Grafo)");
            Console.WriteLine("3. Envíos (Tabla Hash)");
            Console.WriteLine("4. Regiones de distribución (Union-Find)");
            Console.WriteLine("5. Autocompletado (Trie)");
            Console.WriteLine("6. Rastrear envío y calcular su ruta (flujo integrado)");
            Console.WriteLine("7. Guardar datos en archivos");
            Console.WriteLine("8. Reportes para toma de decisiones");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1": MenuCargarArchivos(); break;
                case "2": MenuGrafo(); break;
                case "3": MenuEnvios(); break;
                case "4": MenuRegiones(); break;
                case "5": MenuTrie(); break;
                case "6": RastrearEnvio(); break;
                case "7": GuardarDatos(); break;
                case "8": MenuReportes(); break;
                case "0": Console.WriteLine("Programa finalizado."); break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != "0");
    }

    // FUNCIONES DE LECTURA

    static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    // Pide un número mayor o igual a cero hasta que sea válido
    static double LeerNumero(string mensaje)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (SistemaLogistica.ConvertirNumero(texto, out double valor) && valor >= 0)
            {
                return valor;
            }

            Console.WriteLine("Valor no válido, debe ser un número mayor o igual a 0.");
        }
    }

    // Pide una coordenada (puede ser negativa) hasta que sea un número válido
    static double LeerCoordenada(string mensaje)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (SistemaLogistica.ConvertirNumero(texto, out double valor))
            {
                return valor;
            }

            Console.WriteLine("Valor no válido, debe ser un número (ej. 14.63 o -90.51).");
        }
    }

    static bool LeerSiNo(string mensaje)
    {
        string respuesta = LeerTexto(mensaje + " (s/n): ").ToLower();
        return respuesta == "s" || respuesta == "si" || respuesta == "sí";
    }

    // Muestra las sugerencias del Trie y deja elegir una
    static string? ElegirSugerencia(string[] opciones)
    {
        if (opciones.Length == 1)
        {
            if (LeerSiNo("¿Se refiere a \"" + opciones[0] + "\"?"))
            {
                return opciones[0];
            }
            return null;
        }

        Console.WriteLine("Sugerencias:");
        for (int i = 0; i < opciones.Length; i++)
        {
            Console.WriteLine((i + 1) + ". " + opciones[i]);
        }

        while (true)
        {
            string texto = LeerTexto("Seleccione un número (0 para cancelar): ");

            if (int.TryParse(texto, out int numero) && numero >= 0 && numero <= opciones.Length)
            {
                if (numero == 0)
                {
                    return null;
                }
                return opciones[numero - 1];
            }

            Console.WriteLine("Opción no válida.");
        }
    }

    // Pide una ciudad. Si el nombre no está completo, el Trie sugiere opciones.
    static string? SeleccionarCiudad(string mensaje)
    {
        string texto = LeerTexto(mensaje);

        if (texto == "")
        {
            Console.WriteLine("No ingresó ninguna ciudad.");
            return null;
        }

        // Si el nombre está completo se devuelve tal como está registrado
        int indice = sistema.Mapa.BuscarIndice(texto);
        if (indice != -1)
        {
            return sistema.Mapa.ObtenerNombre(indice);
        }

        // Si no, se toma como prefijo y el Trie sugiere nombres
        string[] sugerencias = sistema.TrieCiudades.SugerirPorPrefijo(texto).ConvertirArreglo();

        if (sugerencias.Length == 0)
        {
            Console.WriteLine("No existe ninguna ciudad que empiece con \"" + texto + "\".");
            return null;
        }

        return ElegirSugerencia(sugerencias);
    }

    // Pide un código de rastreo. Si no está completo, el Trie sugiere códigos.
    static string? SeleccionarCodigo(string mensaje)
    {
        string texto = LeerTexto(mensaje).ToUpper();

        if (texto == "")
        {
            Console.WriteLine("No ingresó ningún código.");
            return null;
        }

        if (sistema.Envios.Buscar(texto) != null)
        {
            return texto;
        }

        string[] sugerencias = sistema.TrieCodigos.SugerirPorPrefijo(texto).ConvertirArreglo();

        if (sugerencias.Length == 0)
        {
            Console.WriteLine("No existe ningún envío con código que empiece con \"" + texto + "\".");
            return null;
        }

        return ElegirSugerencia(sugerencias);
    }

    // 1. CARGAR ARCHIVOS

    static void MenuCargarArchivos()
    {
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# CARGAR DATOS DESDE ARCHIVOS");
            Console.WriteLine("Carpeta por defecto: " + carpetaPorDefecto);
            Console.WriteLine();
            Console.WriteLine("1. Cargar todo desde una carpeta (ciudades.csv, rutas.csv, envios.csv)");
            Console.WriteLine("2. Cargar archivo de ciudades");
            Console.WriteLine("3. Cargar archivo de rutas");
            Console.WriteLine("4. Cargar archivo de envíos");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1":
                    string carpeta = LeerTexto("Ruta de la carpeta (Enter para usar la carpeta por defecto): ");
                    if (carpeta == "")
                    {
                        carpeta = carpetaPorDefecto;
                    }
                    CargarCarpeta(QuitarComillas(carpeta));
                    break;

                case "2":
                    MostrarCarga(sistema.CargarCiudades(PedirRutaArchivo("ciudades.csv")), "Ciudades cargadas");
                    break;

                case "3":
                    MostrarCarga(sistema.CargarRutas(PedirRutaArchivo("rutas.csv")), "Rutas cargadas");
                    break;

                case "4":
                    MostrarCarga(sistema.CargarEnvios(PedirRutaArchivo("envios.csv")), "Envíos cargados");
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != "0");
    }

    // Carga los tres archivos de la carpeta: primero ciudades, luego rutas y envíos
    static void CargarCarpeta(string carpeta)
    {
        ResultadoCarga[]? resultados = sistema.CargarCarpeta(carpeta);

        if (resultados == null)
        {
            Console.WriteLine("La carpeta no existe: " + carpeta);
            return;
        }

        Console.WriteLine();
        Console.WriteLine("# CARGANDO CIUDADES");
        MostrarCarga(resultados[0], "Ciudades cargadas");

        Console.WriteLine();
        Console.WriteLine("# CARGANDO RUTAS");
        MostrarCarga(resultados[1], "Rutas cargadas");

        Console.WriteLine();
        Console.WriteLine("# CARGANDO ENVÍOS");
        MostrarCarga(resultados[2], "Envíos cargados");
    }

    static string PedirRutaArchivo(string nombrePorDefecto)
    {
        string ruta = LeerTexto("Ruta del archivo (Enter para usar " + nombrePorDefecto + " de la carpeta por defecto): ");

        if (ruta == "")
        {
            return Path.Combine(carpetaPorDefecto, nombrePorDefecto);
        }

        return QuitarComillas(ruta);
    }

    // Windows agrega comillas al usar "Copiar como ruta de acceso"
    static string QuitarComillas(string ruta)
    {
        return ruta.Trim().Trim('"');
    }

    // 2. GRAFO

    static void MenuGrafo()
    {
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# CIUDADES Y RUTAS (GRAFO)");
            Console.WriteLine("1. Registrar ciudad");
            Console.WriteLine("2. Registrar ruta");
            Console.WriteLine("3. Eliminar ruta");
            Console.WriteLine("4. Mostrar ciudades y rutas");
            Console.WriteLine("5. Recorrido BFS");
            Console.WriteLine("6. Recorrido DFS");
            Console.WriteLine("7. Ruta más económica entre dos ciudades (Dijkstra por costo)");
            Console.WriteLine("8. Ruta más rápida entre dos ciudades (Dijkstra por tiempo)");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1": RegistrarCiudad(); break;
                case "2": RegistrarRuta(); break;
                case "3": EliminarRuta(); break;
                case "4": MostrarGrafo(); break;
                case "5": Recorrido(true); break;
                case "6": Recorrido(false); break;
                case "7": CalcularRuta(true); break;
                case "8": CalcularRuta(false); break;
                case "0": break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != "0");
    }

    static void RegistrarCiudad()
    {
        string nombre = LeerTexto("Nombre de la ciudad: ");

        // Las coordenadas son opcionales; solo sirven para el mapa de la interfaz gráfica
        double latitud = double.NaN;
        double longitud = double.NaN;
        if (LeerSiNo("¿Desea ingresar coordenadas para el mapa?"))
        {
            latitud = LeerCoordenada("Latitud (ej. 14.63): ");
            longitud = LeerCoordenada("Longitud (ej. -90.51): ");
        }

        sistema.RegistrarCiudad(nombre, latitud, longitud, out string mensaje);
        Console.WriteLine(mensaje);
    }

    static void RegistrarRuta()
    {
        if (sistema.Mapa.Cantidad < 2)
        {
            Console.WriteLine("Se necesitan al menos dos ciudades registradas.");
            return;
        }

        string? origen = SeleccionarCiudad("Ciudad de origen: ");
        if (origen == null) return;

        string? destino = SeleccionarCiudad("Ciudad de destino: ");
        if (destino == null) return;

        double costo = LeerNumero("Costo (Q): ");
        double tiempo = LeerNumero("Tiempo (horas): ");
        bool dobleSentido = LeerSiNo("¿La ruta es de doble sentido?");

        sistema.RegistrarRuta(origen, destino, costo, tiempo, dobleSentido, out string mensaje);
        Console.WriteLine(mensaje);
    }

    static void EliminarRuta()
    {
        string? origen = SeleccionarCiudad("Ciudad de origen: ");
        if (origen == null) return;

        string? destino = SeleccionarCiudad("Ciudad de destino: ");
        if (destino == null) return;

        bool ambos = LeerSiNo("¿Eliminar también la ruta de regreso (" + destino + " → " + origen + ")?");

        sistema.EliminarRuta(origen, destino, ambos, out string mensaje);
        Console.WriteLine(mensaje);
    }

    static void Recorrido(bool esBFS)
    {
        if (sistema.Mapa.Cantidad == 0)
        {
            Console.WriteLine("No hay ciudades registradas.");
            return;
        }

        string? inicio = SeleccionarCiudad("Ciudad de inicio: ");
        if (inicio == null) return;

        int indice = sistema.Mapa.BuscarIndice(inicio);

        if (esBFS)
        {
            MostrarRecorrido(sistema.Mapa.BFS(indice), "BFS");
        }
        else
        {
            MostrarRecorrido(sistema.Mapa.DFS(indice), "DFS");
        }
    }

    static void CalcularRuta(bool porCosto)
    {
        if (sistema.Mapa.Cantidad < 2)
        {
            Console.WriteLine("Se necesitan al menos dos ciudades registradas.");
            return;
        }

        string? origen = SeleccionarCiudad("Ciudad de origen: ");
        if (origen == null) return;

        string? destino = SeleccionarCiudad("Ciudad de destino: ");
        if (destino == null) return;

        MostrarRuta(sistema.CalcularRuta(origen, destino, porCosto), true);
    }

    // 3. ENVÍOS

    static void MenuEnvios()
    {
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# ENVÍOS (TABLA HASH)");
            Console.WriteLine("1. Registrar envío");
            Console.WriteLine("2. Buscar envío por código exacto");
            Console.WriteLine("3. Buscar envío por inicio del código (Trie + Tabla Hash)");
            Console.WriteLine("4. Eliminar envío");
            Console.WriteLine("5. Mostrar tabla hash");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1": RegistrarEnvio(); break;
                case "2": BuscarEnvioExacto(); break;
                case "3": BuscarEnvioPorPrefijo(); break;
                case "4": EliminarEnvio(); break;
                case "5": MostrarTablaHash(); break;
                case "0": break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != "0");
    }

    static void RegistrarEnvio()
    {
        if (sistema.Mapa.Cantidad < 2)
        {
            Console.WriteLine("Primero debe registrar al menos dos ciudades.");
            return;
        }

        string codigo = LeerTexto("Código de rastreo: ").ToUpper();

        if (codigo == "")
        {
            Console.WriteLine("El código no puede estar vacío.");
            return;
        }

        // El código es único: se verifica en la tabla hash antes de pedir lo demás
        if (sistema.Envios.Buscar(codigo) != null)
        {
            Console.WriteLine("Ya existe un envío con ese código.");
            return;
        }

        string remitente = LeerTexto("Remitente: ");
        string destinatario = LeerTexto("Destinatario: ");

        string? origen = SeleccionarCiudad("Ciudad de origen (puede escribir solo el inicio): ");
        if (origen == null) return;

        string? destino = SeleccionarCiudad("Ciudad de destino (puede escribir solo el inicio): ");
        if (destino == null) return;

        sistema.RegistrarEnvio(codigo, remitente, destinatario, origen, destino, out string mensaje);
        Console.WriteLine(mensaje);
    }

    static void BuscarEnvioExacto()
    {
        string codigo = LeerTexto("Ingrese el código: ").ToUpper();
        Envio? envio = sistema.Envios.Buscar(codigo);

        if (envio != null)
        {
            Console.WriteLine();
            Console.WriteLine("# ENVÍO ENCONTRADO");
            MostrarEnvio(envio);
        }
        else
        {
            Console.WriteLine("Envío no encontrado.");
        }
    }

    static void BuscarEnvioPorPrefijo()
    {
        string? codigo = SeleccionarCodigo("Ingrese el código o su inicio: ");
        if (codigo == null) return;

        Envio? envio = sistema.Envios.Buscar(codigo);

        if (envio != null)
        {
            Console.WriteLine();
            Console.WriteLine("# ENVÍO ENCONTRADO");
            MostrarEnvio(envio);
        }
    }

    static void EliminarEnvio()
    {
        string? codigo = SeleccionarCodigo("Código del envío a eliminar (o su inicio): ");
        if (codigo == null) return;

        if (!LeerSiNo("¿Seguro que desea eliminar el envío " + codigo + "?"))
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }

        if (sistema.EliminarEnvio(codigo))
        {
            Console.WriteLine("Envío eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("Envío no encontrado.");
        }
    }

    // 4. UNION-FIND

    static void MenuRegiones()
    {
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# REGIONES DE DISTRIBUCIÓN (UNION-FIND)");
            Console.WriteLine("1. Verificar si dos ciudades están en la misma región");
            Console.WriteLine("2. Mostrar regiones");
            Console.WriteLine("3. Mostrar arreglos padre y rango");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1": VerificarRegion(); break;
                case "2": MostrarRegiones(); break;
                case "3": MostrarArreglosUnionFind(); break;
                case "0": break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != "0");
    }

    static void VerificarRegion()
    {
        string? ciudad1 = SeleccionarCiudad("Primera ciudad: ");
        if (ciudad1 == null) return;

        string? ciudad2 = SeleccionarCiudad("Segunda ciudad: ");
        if (ciudad2 == null) return;

        int a = sistema.Mapa.BuscarIndice(ciudad1);
        int b = sistema.Mapa.BuscarIndice(ciudad2);

        Console.WriteLine("Representante de " + ciudad1 + ": " + sistema.Mapa.ObtenerNombre(sistema.Regiones.Find(a)));
        Console.WriteLine("Representante de " + ciudad2 + ": " + sistema.Mapa.ObtenerNombre(sistema.Regiones.Find(b)));

        if (sistema.Regiones.EstanConectados(a, b))
        {
            Console.WriteLine(ciudad1 + " y " + ciudad2 + " pertenecen a la misma región de distribución.");
        }
        else
        {
            Console.WriteLine(ciudad1 + " y " + ciudad2 + " pertenecen a regiones distintas.");
        }
    }

    // 5. TRIE

    static void MenuTrie()
    {
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# AUTOCOMPLETADO (TRIE)");
            Console.WriteLine("1. Sugerir ciudades por prefijo");
            Console.WriteLine("2. Sugerir códigos de rastreo por prefijo");
            Console.WriteLine("3. Verificar si una ciudad existe (búsqueda exacta)");
            Console.WriteLine("4. Verificar si existe algún nombre con un prefijo");
            Console.WriteLine("5. Mostrar todas las ciudades en orden alfabético");
            Console.WriteLine("6. Mostrar todos los códigos de rastreo");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            switch (opcion)
            {
                case "1": SugerirPrefijo(sistema.TrieCiudades, "ciudades"); break;
                case "2": SugerirPrefijo(sistema.TrieCodigos, "códigos"); break;
                case "3": BuscarCiudadExacta(); break;
                case "4": VerificarPrefijo(); break;
                case "5": MostrarTrie(sistema.TrieCiudades, "CIUDADES EN ORDEN ALFABÉTICO"); break;
                case "6": MostrarTrie(sistema.TrieCodigos, "CÓDIGOS DE RASTREO"); break;
                case "0": break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != "0");
    }

    static void SugerirPrefijo(Trie trie, string tipo)
    {
        string prefijo = LeerTexto("Ingrese el prefijo: ");
        string[] resultado = trie.SugerirPorPrefijo(prefijo).ConvertirArreglo();

        if (resultado.Length == 0)
        {
            Console.WriteLine("No se encontraron " + tipo + " con ese prefijo.");
            return;
        }

        Console.WriteLine("Sugerencias (" + resultado.Length + "):");
        foreach (string palabra in resultado)
        {
            Console.WriteLine("- " + palabra);
        }
    }

    static void BuscarCiudadExacta()
    {
        string nombre = LeerTexto("Nombre completo de la ciudad: ");

        if (sistema.TrieCiudades.Buscar(nombre))
        {
            Console.WriteLine("La ciudad existe.");
        }
        else
        {
            Console.WriteLine("La ciudad no existe.");
        }
    }

    static void VerificarPrefijo()
    {
        string prefijo = LeerTexto("Ingrese el prefijo: ");

        Console.WriteLine("¿Hay ciudades que empiecen así? " +
                          (sistema.TrieCiudades.EmpiezaCon(prefijo) ? "Sí" : "No"));
        Console.WriteLine("¿Hay códigos que empiecen así? " +
                          (sistema.TrieCodigos.EmpiezaCon(prefijo) ? "Sí" : "No"));
    }

    // 6. FLUJO INTEGRADO
    // Trie sugiere el código -> Tabla Hash localiza el envío ->
    // Union-Find valida la conectividad -> Grafo calcula la ruta con Dijkstra
    static void RastrearEnvio()
    {
        if (sistema.Envios.Cantidad == 0)
        {
            Console.WriteLine("No hay envíos registrados.");
            return;
        }

        // Paso 1: Trie
        string? codigo = SeleccionarCodigo("Código de rastreo (o su inicio): ");
        if (codigo == null) return;

        // Paso 2: Tabla Hash
        Envio? envio = sistema.Envios.Buscar(codigo);
        if (envio == null)
        {
            Console.WriteLine("Envío no encontrado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("# DATOS DEL ENVÍO");
        MostrarEnvio(envio);

        Console.WriteLine();
        Console.WriteLine("1. Ruta más económica");
        Console.WriteLine("2. Ruta más rápida");
        Console.WriteLine("3. Ambas");
        string criterio = LeerTexto("Seleccione el criterio: ");

        // Pasos 3 y 4: Union-Find y Dijkstra
        Console.WriteLine();
        if (criterio == "1" || criterio == "3")
        {
            MostrarRuta(sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, true), false);
        }
        if (criterio == "3")
        {
            Console.WriteLine();
        }
        if (criterio == "2" || criterio == "3")
        {
            MostrarRuta(sistema.CalcularRuta(envio.CiudadOrigen, envio.CiudadDestino, false), false);
        }
        if (criterio != "1" && criterio != "2" && criterio != "3")
        {
            Console.WriteLine("Opción no válida.");
        }
    }

    // 7. GUARDAR

    static void GuardarDatos()
    {
        string carpeta = LeerTexto("Carpeta donde guardar (Enter para usar la carpeta por defecto): ");

        if (carpeta == "")
        {
            carpeta = carpetaPorDefecto;
        }

        sistema.GuardarDatos(QuitarComillas(carpeta), out string mensaje);
        Console.WriteLine(mensaje);
    }

    // 8. REPORTES

    static void MenuReportes()
    {
        string[] nombres = GeneradorReportes.NombresReportes();
        string opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("# REPORTES PARA TOMA DE DECISIONES");
            for (int i = 0; i < nombres.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + nombres[i]);
            }
            Console.WriteLine("5. Mostrar todos los reportes");
            Console.WriteLine("6. Exportar todos los reportes a un archivo .txt");
            Console.WriteLine("7. Exportar un reporte a un archivo .csv");
            Console.WriteLine("0. Regresar");
            Console.WriteLine();
            opcion = LeerTexto("Seleccione una opción: ");

            // Los reportes se generan cada vez para que usen los datos actuales
            GeneradorReportes generador = new GeneradorReportes(sistema);

            switch (opcion)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    Console.WriteLine();
                    Console.Write(GeneradorReportes.ATexto(generador.Generar(int.Parse(opcion) - 1)));
                    break;

                case "5":
                    foreach (Reporte reporte in generador.GenerarTodos())
                    {
                        Console.WriteLine();
                        Console.Write(GeneradorReportes.ATexto(reporte));
                    }
                    break;

                case "6":
                    ExportarReportesTxt(generador);
                    break;

                case "7":
                    ExportarReporteCsv(generador, nombres);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != "0");
    }

    static void ExportarReportesTxt(GeneradorReportes generador)
    {
        string porDefecto = Path.Combine(SistemaLogistica.CarpetaReportesPorDefecto(), "reportes.txt");
        string ruta = LeerTexto("Ruta del archivo (Enter para usar " + porDefecto + "): ");

        if (ruta == "")
        {
            ruta = porDefecto;
        }

        GeneradorReportes.ExportarTxt(generador.GenerarTodos(), QuitarComillas(ruta), out string mensaje);
        Console.WriteLine(mensaje);
    }

    static void ExportarReporteCsv(GeneradorReportes generador, string[] nombres)
    {
        string texto = LeerTexto("Número del reporte (1 a " + nombres.Length + "): ");

        if (!int.TryParse(texto, out int numero) || numero < 1 || numero > nombres.Length)
        {
            Console.WriteLine("Número de reporte no válido.");
            return;
        }

        string porDefecto = Path.Combine(SistemaLogistica.CarpetaReportesPorDefecto(),
                                         GeneradorReportes.NombresArchivo()[numero - 1]);
        string ruta = LeerTexto("Ruta del archivo (Enter para usar " + porDefecto + "): ");

        if (ruta == "")
        {
            ruta = porDefecto;
        }

        GeneradorReportes.ExportarCsv(generador.Generar(numero - 1), QuitarComillas(ruta), out string mensaje);
        Console.WriteLine(mensaje);
    }

    // FUNCIONES DE IMPRESIÓN
    // Las estructuras devuelven datos; aquí se convierten en texto para la consola.

    static void MostrarEnvio(Envio envio)
    {
        Console.WriteLine("Código de rastreo: " + envio.CodigoRastreo);
        Console.WriteLine("Remitente: " + envio.Remitente);
        Console.WriteLine("Destinatario: " + envio.Destinatario);
        Console.WriteLine("Ciudad de origen: " + envio.CiudadOrigen);
        Console.WriteLine("Ciudad de destino: " + envio.CiudadDestino);
    }

    static void MostrarCarga(ResultadoCarga resultado, string textoCargados)
    {
        foreach (string mensaje in resultado.Mensajes.ConvertirArreglo())
        {
            Console.WriteLine(mensaje);
        }

        // Si el archivo no se pudo leer ya se mostró el motivo
        if (resultado.ArchivoLeido)
        {
            Console.WriteLine(textoCargados + ": " + resultado.Cargados +
                              " | Líneas con error: " + resultado.Errores);
        }
    }

    // Tabla hash: cada posición con su envío, VACÍO o ELIMINADO
    static void MostrarTablaHash()
    {
        TablaHash tabla = sistema.Envios;

        Console.WriteLine();
        Console.WriteLine("# TABLA HASH DE ENVÍOS");
        Console.WriteLine("Cantidad de envíos: " + tabla.Cantidad);
        Console.WriteLine("Tamaño de la tabla: " + tabla.Tamanio);
        Console.WriteLine("Factor de carga: " + tabla.FactorCarga().ToString("0.00"));
        Console.WriteLine("Rehashing realizados: " + tabla.CantidadRehashing);
        Console.WriteLine();

        for (int i = 0; i < tabla.Tamanio; i++)
        {
            Envio? envio = tabla.ObtenerPosicion(i);

            if (envio != null)
            {
                Console.WriteLine(
                    i + " → " +
                    envio.CodigoRastreo + " | " +
                    envio.Remitente + " | " +
                    envio.CiudadOrigen + " → " +
                    envio.CiudadDestino
                );
            }
            else if (tabla.PosicionEliminada(i))
            {
                Console.WriteLine(i + " → ELIMINADO");
            }
            else
            {
                Console.WriteLine(i + " → VACÍO");
            }
        }
    }

    // Grafo: lista de adyacencia de cada ciudad
    static void MostrarGrafo()
    {
        Grafo mapa = sistema.Mapa;

        Console.WriteLine();
        Console.WriteLine("# CIUDADES Y RUTAS");

        if (mapa.Cantidad == 0)
        {
            Console.WriteLine("No hay ciudades registradas.");
            return;
        }

        for (int i = 0; i < mapa.Cantidad; i++)
        {
            Console.Write(mapa.ObtenerNombre(i) + " -> ");

            Arista? actual = mapa.PrimeraArista(i);

            if (actual == null)
            {
                Console.WriteLine("(sin rutas de salida)");
                continue;
            }

            string texto = "";
            while (actual != null)
            {
                if (texto != "")
                {
                    texto += ", ";
                }
                texto += mapa.ObtenerNombre(actual.Destino) +
                         " (Q" + actual.Costo.ToString("0.00") +
                         ", " + actual.Tiempo.ToString("0.0") + " h)";
                actual = actual.Siguiente;
            }

            Console.WriteLine(texto);
        }
    }

    static void MostrarRecorrido(ResultadoRecorrido recorrido, string tipo)
    {
        Grafo mapa = sistema.Mapa;
        int[] orden = recorrido.Orden;
        string textoOrden = "";

        Console.WriteLine();
        Console.WriteLine("# RECORRIDO " + tipo + " DESDE " + mapa.ObtenerNombre(orden[0]).ToUpper());

        // En BFS también se muestra cómo va cambiando la cola
        if (recorrido.EstadoCola != null)
        {
            Console.WriteLine("Cola inicial: [" + mapa.ObtenerNombre(orden[0]) + "]");
        }

        for (int i = 0; i < orden.Length; i++)
        {
            if (recorrido.EstadoCola != null)
            {
                Console.WriteLine("Se visita: " + mapa.ObtenerNombre(orden[i]) +
                                  " -> Cola: [" + recorrido.EstadoCola[i] + "]");
            }
            else
            {
                Console.WriteLine("Se visita: " + mapa.ObtenerNombre(orden[i]));
            }

            if (textoOrden != "")
            {
                textoOrden += " -> ";
            }
            textoOrden += mapa.ObtenerNombre(orden[i]);
        }

        Console.WriteLine("Orden final " + tipo + ": " + textoOrden);
    }

    static void MostrarTablaDijkstra(ResultadoDijkstra dijkstra)
    {
        Grafo mapa = sistema.Mapa;
        string criterio = dijkstra.PorCosto ? "COSTO" : "TIEMPO";

        Console.WriteLine();
        Console.WriteLine("# DIJKSTRA DESDE " + mapa.ObtenerNombre(dijkstra.Origen).ToUpper() + " (POR " + criterio + ")");
        Console.WriteLine($"{"Ciudad",-20}{"Distancia",-14}{"Anterior"}");

        for (int i = 0; i < mapa.Cantidad; i++)
        {
            string distancia;
            if (dijkstra.Distancias[i] == double.MaxValue)
            {
                distancia = "∞";
            }
            else if (dijkstra.PorCosto)
            {
                distancia = "Q" + dijkstra.Distancias[i].ToString("0.00");
            }
            else
            {
                distancia = dijkstra.Distancias[i].ToString("0.0") + " h";
            }

            string anterior = dijkstra.Anteriores[i] == -1 ? "-" : mapa.ObtenerNombre(dijkstra.Anteriores[i]);

            Console.WriteLine($"{mapa.ObtenerNombre(i),-20}{distancia,-14}{anterior}");
        }
    }

    // Muestra el resultado de Union-Find + Dijkstra
    static void MostrarRuta(ResultadoRuta ruta, bool mostrarTabla)
    {
        if (!ruta.DatosValidos)
        {
            Console.WriteLine(ruta.Mensaje);
            return;
        }

        if (!ruta.MismaRegion)
        {
            Console.WriteLine("Union-Find: " + ruta.Mensaje);
            return;
        }

        Console.WriteLine("Union-Find: ambas ciudades están en la misma región. Se ejecuta Dijkstra.");

        if (mostrarTabla && ruta.Dijkstra != null)
        {
            MostrarTablaDijkstra(ruta.Dijkstra);
        }

        string criterio = ruta.PorCosto ? "más económica" : "más rápida";

        Console.WriteLine();
        Console.WriteLine("# RUTA " + criterio.ToUpper());

        if (ruta.Camino == null)
        {
            Console.WriteLine(ruta.Mensaje);
            return;
        }

        Console.WriteLine("Camino: " + sistema.TextoCamino(ruta.Camino));
        Console.WriteLine("Costo total: Q" + ruta.CostoTotal.ToString("0.00"));
        Console.WriteLine("Tiempo total: " + ruta.TiempoTotal.ToString("0.0") + " h");
    }

    // Union-Find: cada región con las ciudades que la forman
    static void MostrarRegiones()
    {
        Grafo mapa = sistema.Mapa;
        UnionFind regiones = sistema.Regiones;

        Console.WriteLine();
        Console.WriteLine("# REGIONES DE DISTRIBUCIÓN");

        if (mapa.Cantidad == 0)
        {
            Console.WriteLine("No hay ciudades registradas.");
            return;
        }

        int[] numeroRegion = regiones.NumerarRegiones();
        int total = regiones.ContarRegiones();

        for (int region = 1; region <= total; region++)
        {
            string integrantes = "";
            int representante = -1;

            for (int i = 0; i < mapa.Cantidad; i++)
            {
                if (numeroRegion[i] == region)
                {
                    if (integrantes != "")
                    {
                        integrantes += ", ";
                    }
                    integrantes += mapa.ObtenerNombre(i);
                    representante = regiones.Find(i);
                }
            }

            Console.WriteLine("Región " + region +
                              " (representante: " + mapa.ObtenerNombre(representante) + "): " +
                              integrantes);
        }

        Console.WriteLine("Total de regiones: " + total);
    }

    // Union-Find: arreglos padre y rango tal como están en memoria
    static void MostrarArreglosUnionFind()
    {
        Grafo mapa = sistema.Mapa;
        UnionFind regiones = sistema.Regiones;

        Console.WriteLine();
        Console.WriteLine("# ARREGLOS DEL UNION-FIND");
        Console.WriteLine($"{"Índice",-8}{"Ciudad",-20}{"Padre",-20}{"Rango"}");

        for (int i = 0; i < regiones.Cantidad; i++)
        {
            Console.WriteLine($"{i,-8}{mapa.ObtenerNombre(i),-20}{mapa.ObtenerNombre(regiones.ObtenerPadre(i)),-20}{regiones.ObtenerRango(i)}");
        }
    }

    // Trie: todas las palabras en orden alfabético
    static void MostrarTrie(Trie trie, string titulo)
    {
        Console.WriteLine();
        Console.WriteLine("# " + titulo);

        string[] palabras = trie.ObtenerTodas().ConvertirArreglo();

        if (palabras.Length == 0)
        {
            Console.WriteLine("El Trie está vacío.");
            return;
        }

        foreach (string palabra in palabras)
        {
            Console.WriteLine("- " + palabra);
        }

        Console.WriteLine("Total: " + palabras.Length);
    }
}
