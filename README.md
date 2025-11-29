# Solar-system

<p align="left">
  <img src="https://github.com/user-attachments/assets/06c2a674-ddad-41ac-ae0d-eca1f741229d" width="450">
</p>

🌍 **Solar System Renderer (C# + OpenGL)**  

A small real-time Solar System simulation written in C# without any game engine.  
All rendering is done manually using OpenGL bindings (GL, GLEW, WGL, GLU) via imported native functions — **no OpenTK, no Unity, no external toolkit**.

This project showcases **3D graphics, camera math, transformations, and orbital motion**.

---

## 🚀 Features

### 🌞 Solar System Simulation
- The Sun in the center  
- 4 planets orbiting the sun  
- Earth orbiting with a realistic rotation  
- The Moon orbiting around Earth  
- Each object moves using its own rotation & orbit angle  

---

## 🎮 3D Camera System
Fully manual camera coded using algebra (vectors + rotation matrices):

- Move forward / backward  
- Move left / right  
- Move up / down  
- Rotate camera left / right  
- Look up / down  
- Free-look around the entire scene  

---

## 🔧 OpenGL Rendering (in pure C#)
- Imported GL, GLU, GLEW, WGL functions via `DllImport`  
- Manual projection setup (`gluPerspective`, `glFrustum`)  
- Manual matrix transforms (`glRotate`, `glTranslate`)  
- Basic texture rendering  
- **No engine, no OpenTK — raw OpenGL bindings**  

---
