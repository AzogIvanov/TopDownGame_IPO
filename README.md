# 🧟‍♂️ Outbreak 45

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=flat&logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-blue?style=flat&logo=csharp)
![Status](https://img.shields.io/badge/Status-Finished-green)
![License](https://img.shields.io/badge/License-Academic-orange)

**Outbreak 45** es un videojuego *Top-Down Shooter 2D* desarrollado en **Unity**, donde el objetivo es sobrevivir a oleadas de enemigos gestionando munición, espacio y estrategia en un entorno hostil.

Este proyecto ha sido desarrollado como **trabajo académico**, enfocándose en la arquitectura de código, IA con NavMesh y gestión avanzada de audio.

---

## 📸 Descripción General

El jugador controla a un superviviente armado que debe defender una zona de un brote biológico. Los enemigos persiguen al jugador utilizando Inteligencia Artificial, emiten sonidos ambientales para alertar de su posición y presentan diferentes comportamientos (ataque cuerpo a cuerpo, explosiones, etc.).

**Condiciones de fin de partida:**
* ❌ **Derrota:** El jugador pierde toda su vida.
* ✅ **Victoria:** Se eliminan todos los objetivos enemigos de la zona.

---

## 🎮 Características Principales

### 🕹️ Jugabilidad
* **Vista Top-Down:** Movimiento libre en 2D.
* **Combate en tiempo real:** Sistema de apuntado con el ratón (`LookAt`).
* **Gestión de salud:** Sistema de vida independiente para el jugador y los enemigos.

### 🧟 Enemigos e IA
* **NavMeshAgent 2D:** Pathfinding inteligente para persecución.
* **Variedad de enemigos:**
    * *Zombis Estándar:* Persecución y ataque melee.
    * *Zombis Explosivos:* Detonan al morir o al acercarse.
    * *Boss Final:* Mayor resistencia y patrones de ataque.

### 🔫 Arsenal
* **Cuerpo a cuerpo:** Estado inicial sin munición.
* **Escopeta:** Daño disperso a corta distancia.
* **Rifle Automático:** Alta cadencia de fuego.
* **Lanzagranadas:** Daño en área (AoE) con físicas de explosión.

### 💥 Efectos Visuales
* Sistema de partículas para explosiones.
* Feedback visual al recibir daño.

---

## 🔊 Sistema de Audio

El audio es un pilar fundamental de este proyecto, gestionado mediante scripts dedicados para evitar solapamientos y saturación.

* **Enemigos:**
    * `AudioSource` ambiental (gruñidos aleatorios).
    * `AudioSource` independiente para muerte/impacto.
    * Lógica para detener sonidos al morir el objeto.
* **Jugador:**
    * Canales separados para pasos, disparos y recarga.
* **Ambiente:**
    * Sonidos metálicos al impactar balas en superficies.
    * Explosiones con atenuación espacial.

---
## ⬇️ Instalación y Ejecución

### 1. Clonar el repositorio

Abre tu terminal o consola de comandos y ejecuta el siguiente comando:
```bash
git clone https://github.com/USUARIO/Outbreak45.git
```

### 2. Ejecutar en Unity

1. Abre **Unity Hub**.
2. Pulsa en **Open Project**.
3. Selecciona la carpeta descargada `Outbreak45`.
4. Espera a que Unity compile los scripts e importe los assets.
5. Abre una escena desde la carpeta `Assets/Scenes/`.
6. Pulsa el botón **Play ▶️**.

## 🎮 Controles

| Acción | Tecla / Input |
|--------|---------------|
| Moverse | `W`, `A`, `S`, `D` |
| Apuntar | Ratón (Cursor) |
| Disparar | Click Izquierdo |
| Cambiar Arma | `1`, `2`, `3`, `4` |
| Pausa | `ESC` |

## 🛠️ Atajos de Desarrollo (Debug)

Para facilitar las pruebas y la corrección, se han habilitado teclas rápidas para cargar directamente los distintos niveles:

| Acción | Tecla |
|--------|-------|
| Cargar Nivel 1 | `7` |
| Cargar Nivel 2 | `8` |
| Cargar Nivel 3 | `9` |
