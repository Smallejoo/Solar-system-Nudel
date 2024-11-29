using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes
{

    static internal class OpenGL

    {
                            //BASIC GL 
        //public const uint GL_COLOR_BUFFER_BIT = 0x00004000; // Clear the color buffer
        //public const uint PFD_SUPPORT_OPENGL = 0x00000004; // Support OpenGL rendering


        //PIXELFORMATDESCRIPTOR
        public const int PFD_DRAW_TO_WINDOW = 0x00000001;   // Render to window
        public const int PFD_SUPPORT_OPENGL = 0x00000004;   // OpenGL support
        public const int PFD_DOUBLEBUFFER = 0x00000020;     // Double buffering
        public const int PFD_TYPE_RGBA = 0;                 // RGBA color format
        public const int PFD_MAIN_PLANE = 0;                // Main drawing layer


        public const int COLOR_BITS = 32;                   // 32-bit color depth
        public const int DEPTH_BITS = 24;                   // 24-bit depth buffer
        public const int STENCIL_BITS = 8;                  // 8-bit stencil buffer
                        //RENDERING FIGURES

        public const uint GL_POINTS = 0x0000;               // Draw points
        public const uint GL_LINES = 0x0001;                // Draw lines
        public const uint GL_LINE_STRIP = 0x0003;           // Draw connected lines
        public const uint GL_TRIANGLES = 0x0004;            // Draw triangles
        public const uint GL_TRIANGLE_STRIP = 0x0005;       // Draw a triangle strip
        public const uint GL_TRIANGLE_FAN = 0x0006;         // Draw a triangle fan


        public const uint GL_MODELVIEW = 0x1700;            // Model-view matrix
        public const uint GL_PROJECTION = 0x1701;           // Projection matrix
        public const uint GL_TEXTURE = 0x1702;              // Texture matrix

        public const uint GL_COLOR_BUFFER_BIT_C = 0x4000;     // Color buffer
        public const uint GL_DEPTH_BUFFER_BIT = 0x0100;     // Depth buffer
                        //Enabling
        public const uint GL_DEPTH_TEST = 0x0B71;           // Enable depth testing
        public const uint GL_LIGHTING = 0x0B50;             // Enable lighting
        public const uint GL_BLEND = 0x0BE2;                // Enable blending
        public const uint GL_CULL_FACE = 0x0B44;            // Enable face culling
        public const uint GL_TEXTURE_2D = 0x0DE1;           // Enable 2D textures
        public const uint GL_TEXTURE_3D = 0x806F;           // Enable 3D textures

                        //Texture
        public const uint GL_LINEAR = 0x2601;               // Smooth scaling
        public const uint GL_NEAREST = 0x2600;              // Pixelated scaling

        public const uint GL_REPEAT = 0x2901;               // Repeat texture
        public const uint GL_CLAMP = 0x2900;                // Clamp to edges

        public const uint GL_RGB = 0x1907;                  // RGB format
        public const uint GL_RGBA = 0x1908;                 // RGBA format
                        //XXLightingXX
        public const uint GL_LIGHT0 = 0x4000;               // First light source
        public const uint GL_AMBIENT = 0x1200;              // Ambient light
        public const uint GL_DIFFUSE = 0x1201;              // Diffuse light
        public const uint GL_SPECULAR = 0x1202;             // Specular light
        public const uint GL_POSITION = 0x1203;             // Light position

        public const uint GL_SHININESS = 0x1601;            // Material shininess
        public const uint GL_EMISSION = 0x1600;             // Material emission
        public const uint GL_AMBIENT_AND_DIFFUSE = 0x1602;  // Ambient and diffuse


        public const uint GL_FRONT = 0x0404;                // Front face
        public const uint GL_BACK = 0x0405;                 // Back face
        public const uint GL_FRONT_AND_BACK = 0x0408;       // Both faces

        public const uint GL_LESS = 0x0201;                 // Pass if less
        public const uint GL_GREATER = 0x0204;              // Pass if greater


        [DllImport("opengl32.dll")]
        public static extern void glClear(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll")]
        public static extern void glBegin(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glEnd();

        [DllImport("opengl32.dll")]
        public static extern void glVertex2f(float x, float y);

        [DllImport("opengl32.dll")]
        public static extern void glColor3f(float red, float green, float blue);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern bool SwapBuffers(IntPtr hdc);

        [DllImport("opengl32.dll")]
        public static extern void glFlush();


        [DllImport("opengl32.dll")]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        [DllImport("opengl32.dll")]
        public static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

        [DllImport("opengl32.dll")]
        public static extern bool wglDeleteContext(IntPtr hglrc);

        [DllImport("gdi32.dll", EntryPoint = "ChoosePixelFormat")]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR p_pfd);

        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat")]
        public static extern bool SetPixelFormat(IntPtr hdc, int format, ref PIXELFORMATDESCRIPTOR p_pfd);

        [DllImport("opengl32.dll", EntryPoint = "glLoadIdentity")]
        public static extern void glListBase(uint basex);

        [DllImport("opengl32.dll", EntryPoint = "glLoadMatrixd")]
        public static extern void glLoadIdentity();
    }
}
