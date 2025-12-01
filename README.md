# 🌍 Solar System Renderer (C# + OpenGL)

<p align="left">
  <img src="https://github.com/user-attachments/assets/06c2a674-ddad-41ac-ae0d-eca1f741229d" width="450">
</p>

## 📌 Description
- Real time Solar System simulation written in pure C#
- Uses OpenGL through DllImport (no OpenTK, no Unity, no engines)
- Manual camera math and manual rendering pipeline
- Planets and moon move using simple orbit + rotation logic

## 🚀 Features
- Sun at the center
- 4 planets orbiting the Sun
- Earth rotating around its axis
- Moon orbiting Earth
- Each object has its own orbit angle + rotation speed
- Smooth real-time animation

## 🎮 Camera System
- Move forward / backward
- Move left / right
- Move up / down
- Rotate left / right
- Look up / down
- Free look around the whole scene
- All transformations done using vectors + rotation matrices

## 🔧 Rendering (Pure OpenGL)
- GL / GLU / GLEW / WGL imported manually with `DllImport`
- Manual projection setup (`gluPerspective`)
- Manual transforms (`glRotatef`, `glTranslatef`)
- Textured spheres for planets
- No OpenTK or graphics frameworks

## 💻 Tech
- C#
- .NET
- WinForms (render window)
- OpenGL (GL, GLEW, GLU, WGL)
- DllImport bindings

