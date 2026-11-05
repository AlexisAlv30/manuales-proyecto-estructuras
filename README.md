# Logística de Envíos — Proyecto 2 · Estructura de Datos II

Sistema de logística de envíos para ciudades de Guatemala que integra **cuatro estructuras de datos** en un mismo flujo: rastreo de paquetes, validación de regiones y cálculo de la ruta más barata entre ciudades.

Incluye una **interfaz gráfica en Windows Forms**, un **menú de consola** y los **manuales de usuario y técnico** en PDF.

---

## 👥 Integrantes

| Integrante | Responsabilidad |
|---|---|
| **Ángel** | Código del núcleo (estructuras de datos e integración) |
| **Johann** | Interfaz gráfica (Windows Forms) |
| **Alexis** | Manual de usuario y manual técnico |

---

## 🧩 Estructuras de datos implementadas

| Estructura | Archivo | Uso en el sistema |
|---|---|---|
| **Tabla Hash** | `TablaHash.cs` | Guarda los envíos y los localiza por su código de rastreo. |
| **Grafo** | `Grafo.cs` | Representa ciudades y rutas; calcula la ruta óptima con **Dijkstra** y recorridos **BFS**. |
| **Union-Find** | `UnionFind.cs` | Agrupa las ciudades en regiones y valida que origen y destino estén conectados. |
| **Trie** | `Trie.cs` | Autocompletado de nombres de ciudades y códigos de rastreo. |
| **Lista enlazada** | `ListaEnlazada.cs` | Estructura auxiliar propia para manejar colecciones sin usar las de .NET. |

### Flujo integrado

```
Trie  ──►  Tabla Hash  ──►  Union-Find  ──►  Grafo (Dijkstra)
sugiere     localiza el      valida que la     calcula la ruta
código      envío completo   ruta sea posible  de menor costo
```

La ruta solo se calcula si **Union-Find** confirma que origen y destino pertenecen a la misma región.

---

## 📁 Estructura del repositorio

```
LogisticaEnvios/
├── LogisticaEnvios.sln
├── LogisticaEnvios.Core/      # Biblioteca: estructuras de datos e integración
│   ├── TablaHash.cs
│   ├── Grafo.cs
│   ├── UnionFind.cs
│   ├── Trie.cs
│   ├── ListaEnlazada.cs
│   ├── Envio.cs
│   ├── SistemaLogistica.cs    # Une las cuatro estructuras en un solo flujo
│   ├── Reportes.cs
│   └── Resultados.cs
├── LogisticaEnvios.Consola/   # Menú de consola
├── LogisticaEnvios.GUI/       # Interfaz gráfica (Windows Forms)
├── datos/                     # Archivos CSV de ejemplo
│   ├── ciudades.csv
│   ├── rutas.csv
│   └── envios.csv
└── docs/
    ├── Manual_Usuario.pdf
    ├── Manual_Tecnico.pdf
    └── fuentes/               # HTML, estilos, capturas y diagramas de los manuales
```

---

## ⚙️ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Windows** para la interfaz gráfica (usa Windows Forms, `net8.0-windows`)
- La versión de consola funciona en Windows, Linux y macOS
- Visual Studio 2022 (opcional, recomendado)

---

## ▶️ Cómo ejecutar

### Desde Visual Studio
1. Abrir `LogisticaEnvios/LogisticaEnvios.sln`.
2. Seleccionar como proyecto de inicio **LogisticaEnvios.GUI** (o **LogisticaEnvios.Consola**).
3. Presionar **F5**.

### Desde la terminal
```bash
cd LogisticaEnvios

# Interfaz gráfica (solo Windows)
dotnet run --project LogisticaEnvios.GUI

# Menú de consola
dotnet run --project LogisticaEnvios.Consola
```

---

## 🖥️ Interfaz gráfica

La ventana principal se organiza en pestañas:

| Pestaña | Función |
|---|---|
| **Rastreo** | Buscar un envío por código (con autocompletado) y ver su ruta. |
| **Envíos** | Registrar, consultar y eliminar envíos. |
| **Grafo** | Visualizar el mapa de ciudades y rutas; recorridos BFS. |
| **Rutas** | Agregar rutas y calcular el camino más barato entre dos ciudades. |
| **Regiones** | Ver las regiones conectadas (Union-Find). |
| **Trie** | Probar el autocompletado de ciudades y códigos. |
| **Reportes** | Reportes de ciudades, costos y envíos sin ruta. |

Desde el menú **Archivo** se pueden cargar los datos predeterminados, cargar una carpeta o archivos individuales, guardar y vaciar los datos.

---

## 📄 Formato de los archivos de datos

Los archivos se encuentran en `LogisticaEnvios/datos/` y se cargan en este orden: **ciudades → rutas → envíos**.

**`ciudades.csv`**
```csv
nombre,latitud,longitud
Guatemala,14.6349,-90.5069
```

**`rutas.csv`** — `sentido`: `D` = dirigida (un solo sentido), `U` = no dirigida (ambos sentidos)
```csv
origen,destino,costo,tiempo,sentido
Guatemala,Antigua Guatemala,35,1,D
```

**`envios.csv`**
```csv
codigo,remitente,destinatario,origen,destino
ENV-1001,Carlos Pérez,María López,Guatemala,Quetzaltenango
```

**Reglas de validación de envíos**
- El código no puede repetirse.
- Código, remitente y destinatario son obligatorios y **no pueden contener comas**.
- Las ciudades de origen y destino deben existir en `ciudades.csv`.
- Origen y destino no pueden ser la misma ciudad.

---

## 📚 Documentación

- 📘 [Manual de Usuario](LogisticaEnvios/docs/Manual_Usuario.pdf)
- 📗 [Manual Técnico](LogisticaEnvios/docs/Manual_Tecnico.pdf)

Las fuentes de los manuales (HTML, CSS, capturas y diagramas) están en `LogisticaEnvios/docs/fuentes/`.

---

## 🎓 Curso

**Estructura de Datos II** — Proyecto 2: Escenario de logística de envíos.
