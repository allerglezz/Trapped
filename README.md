# 🕯️ #Trapped — Código Fuente (Unity)

<p align="center">

  <img src="https://img.shields.io/badge/Unity-2021.x%20%2F%202022.x-black?logo=unity" />
  <img src="https://img.shields.io/badge/Language-C%23-239120?logo=csharp" />
  <img src="https://img.shields.io/badge/Estado-Código%20Completo-brightgreen" />
  <img src="https://img.shields.io/badge/Plataforma-Windows-lightgrey?logo=windows" />
  <img src="https://img.shields.io/badge/Licencia-MIT-green?logo=open-source-initiative" />


</p>

> **Este repositorio contiene únicamente el *código fuente* del proyecto #Trapped.  
No incluye escenas, assets, prefabs ni configuración completa de Unity.**

---

## 📌 Sobre el proyecto

**#Trapped** es un *escape room narrativo en primera persona* desarrollado en Unity para la asignatura **Proyecto 2**.

Este repositorio existe para documentar la **arquitectura de código**, la lógica de puzles, la interacción y los sistemas narrativos del juego.

👉 *El proyecto no se puede abrir directamente en Unity tal como está.*  
👉 *El contenido funciona como documentación técnica y portfolio.*

---

# 🗂️ Documentación Interna

A continuación se detalla el propósito de cada carpeta del repositorio, con una descripción técnica pensada para desarrolladores.

---

## 📁 `/Player`

**Scripts principales incluidos:**

- `PlayerController` — Movimiento, cámara y gestión de inputs básicos.  
- `PlayerInteraction` — Raycasts, detección de objetos interactuables y prompts de interacción.  
- `PlayerInventoryBridge` — Conexión entre inventario y jugador.

**Responsabilidad del módulo:**  
Control directo del jugador y la detección de objetos interactivos.

---

## 📁 `/Interaction`

**Scripts principales:**

- `Interactable` — Interfaz o clase base para objetos interactivos.  
- `InteractionManager` — Coordina activación y uso de objetos interactuables.  
- `TriggerArea`, `LookAtTrigger` — Activadores basados en presencia o en mirar a un objeto.

**Responsabilidad del módulo:**  
Crear un sistema modular de interacción reutilizable para puzles y eventos.

---

## 📁 `/Items`

**Scripts principales:**

- `Item` — Objeto clave con identificador único.  
- `ItemPickup` — Lógica de recogida de objetos.  
- `InventoryManager` — Gestión de objetos obtenidos.  
- `ItemUse` — Uso de objetos en ubicaciones específicas del juego.

**Responsabilidad del módulo:**  
Gestionar el flujo de obtención, uso y verificación de ítems necesarios para avanzar.

---

## 📁 `/Puzzles`

**Incluye lógica de puzles como:**

### 🔔 Campanas  
- Secuencias y verificación de patrón.

### 🔮 Panel de Símbolos  
- Selección, orden y validación.

### ✝️ Ritual / Pentarritual  
- Encadenado de fases y activadores.

### 🔐 Común  
- `PuzzleBase` — Clase padre para comportamientos comunes.  
- `PuzzleProgression` — Sistema de llaves lógicas y progreso.

**Responsabilidad del módulo:**  
Implementación modular de puzles con lógica clara y expandible.

---

## 📁 `/UI`

**Scripts principales:**

- `GrimoireUI` — Gestor del libro interactivo, páginas y desbloqueo.  
- `HintPopup` — Mensajes breves de pista.  
- `InteractPrompt` — UI de “Pulsa E para interactuar”.  
- `CanvasManager` — Activación y desactivación de elementos UI.

**Responsabilidad del módulo:**  
Gestión de la interfaz, tanto diegética como de soporte al jugador.

---

## 📁 `/Narrative`

**Scripts principales:**

- `PortraitDialogue` — Diálogos vinculados a retratos de figuras históricas.  
- `LoreEntry` — Pistas narrativas desbloqueables.  
- `NarrativeManager` — Control central de eventos narrativos y progresión.

**Responsabilidad del módulo:**  
Gestionar narrativa reactiva, pistas y desencadenantes de historia.

---

## 📁 `/Managers`

**Scripts principales:**

- `GameManager` — Estado global del juego.  
- `EventManager` — Sistema centralizado de eventos.  
- `PuzzleManager` — Orquestación del progreso de puzles.  
- `AudioManager` — Control de música y sonidos.  
- `SceneFlowManager` — Cambios de escena y finales.

**Responsabilidad del módulo:**  
Coordinar y comunicar módulos independientes del proyecto.

---

# 🛠️ Cómo reutilizar este código

1. Crear un proyecto nuevo en Unity  
2. Copiar la carpeta `Scripts` dentro de `Assets/`  
3. Incorporar tus propios prefabs, escenas y assets  
4. Asignar los scripts según la estructura del juego  
5. Personalizar managers y UI según tu implementación

---

# 👤 Autora

Desarrollado por **<tu nombre>**  
Proyecto académico — Diseño de Videojuegos

---

# 📜 Licencia

Este repositorio se distribuye bajo la **MIT License**.

Puedes usar, copiar, modificar, fusionar, publicar, distribuir, sublicenciar y/o vender copias del software, siempre que se incluya el aviso de copyright original y esta nota de permiso en todas las copias o partes sustanciales del software.

Para más detalles, consulta el archivo `LICENSE` incluido en este repositorio.


