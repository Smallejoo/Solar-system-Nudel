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

## Build and run the complete simulation

The complete planetary simulation lives on **LapTopBranch**. The historical `main` branch contains an earlier triangle-rendering prototype.

Requires Windows, the .NET 8 SDK and an OpenGL-capable graphics driver.

```powershell
git clone --branch LapTopBranch https://github.com/Smallejoo/Solar-system-Nudel.git
cd Solar-system-Nudel
dotnet build Solar-system-Nudel/Solar-system-Nudel.csproj
dotnet run --project Solar-system-Nudel/Solar-system-Nudel.csproj
```

Textures are embedded resources; no separate image download is required.

## Controls

| Keys | Action |
| --- | --- |
| W / S | Move forward / backward |
| A / D | Move sideways |
| Q / E | Rotate horizontally |
| Z / C | Move camera up / down |
| R / F | Look up / down |
| Tab | Switch/reset camera viewpoints, including the overview |

## Code map and scope

`Form1` creates the WinForms window and dispatches keyboard input. `OpenGLRenderer` draws the scene. `Classes/Support` contains camera, planet and space objects; `Textures` loads embedded images. Rendering uses native OpenGL bindings; the project file also retains OpenTK and ImageSharp package dependencies.

This is a visual programming project, not an accurate astronomical model. There is no automated rendering test suite. Verify planet animation, texture loading, camera movement and Tab viewpoints on a Windows machine with graphics support. Existing compiler warnings should be cleaned up separately.
