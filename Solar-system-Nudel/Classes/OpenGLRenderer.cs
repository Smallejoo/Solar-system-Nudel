using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Solar_system_Nudel.Classes
{
    public class OpenGLRenderer
    {
        int Width = 800;
        int Height = 600;

        private IntPtr _deviceContext; // Overall area to draw
        private IntPtr _renderingContext; // Instance of GL
        private IntPtr _windowHandle; // The window pointer

        private bool SetOpenGLPixelFormat(IntPtr deviceContext)
        {
            PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR();
            pfd.nSize = (ushort)Marshal.SizeOf(pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = OpenGL.PFD_SUPPORT_OPENGL | OpenGL.PFD_DOUBLEBUFFER | OpenGL.PFD_DRAW_TO_WINDOW;
            pfd.iPixelType = 0; // PFD_TYPE_RGBA
            pfd.cColorBits = 32;
            pfd.cDepthBits = 24;
            pfd.cStencilBits = 8;
            pfd.iLayerType = 0; // PFD_MAIN_PLANE
            int pixelFormatIndex = OpenGL.ChoosePixelFormat(deviceContext, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Failed to choose a pixel format.");
                return false;
            }

            if (!OpenGL.SetPixelFormat(deviceContext, pixelFormatIndex, ref pfd))
            {
                MessageBox.Show("Failed to set the pixel format.");
                return false;
            }

            return true;
        }

        public OpenGLRenderer(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
            Console.WriteLine($"Window Handle: {_windowHandle}");
        }

        public bool InitializeOpenGL()
        {
            Console.WriteLine($"Initializing OpenGL with Window Handle: {_windowHandle}");

            _deviceContext = OpenGL.GetDC(_windowHandle);
            if (_deviceContext == IntPtr.Zero)
            {
                MessageBox.Show("Failed to get device context");
                return false;
            }

            if (!SetOpenGLPixelFormat(_deviceContext))
            {
                MessageBox.Show("Failed to set the pixel format");
                return false;
            }

            _renderingContext = OpenGL.wglCreateContext(_deviceContext);
            if (_renderingContext == IntPtr.Zero)
            {
                MessageBox.Show("Failed to create OpenGL rendering context");
                return false;
            }

            if (!OpenGL.wglMakeCurrent(_deviceContext, _renderingContext))
            {
                MessageBox.Show("Failed to make OpenGL context current");
                return false;
            }

            return true;
        }

        public void InitRender()
        {
            OpenGL.glClearColor(0.2f, 0.2f, 0.2f, 1.0f); // Background color
            OpenGL.glDisable(OpenGL.GL_DEPTH_TEST); // Disable depth testing for 2D
            OpenGL.glViewport(0, 0, Width, Height);

            OpenGL.glMatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.glLoadIdentity();
            OpenGL.glOrtho(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f); // Set orthographic projection

            OpenGL.glMatrixMode(OpenGL.GL_MODELVIEW);
            OpenGL.glLoadIdentity();
        }

        public void Draw()
        {
            if (_deviceContext == IntPtr.Zero || _renderingContext == IntPtr.Zero)
                return;

            Console.WriteLine("Clearing buffers...");
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT); // Clear the color buffer

            Console.WriteLine("Drawing 2D features...");

            OpenGL.glPointSize(10.0f); // Set point size for visibility

            OpenGL.glBegin(OpenGL.GL_POINTS); // Start rendering points
            OpenGL.glColor3f(1.0f, 0.0f, 0.0f); // Red
            OpenGL.glVertex2f(-0.5f, -0.5f);    // Bottom-left point

            OpenGL.glColor3f(0.0f, 1.0f, 0.0f); // Green
            OpenGL.glVertex2f(0.5f, -0.5f);     // Bottom-right point

            OpenGL.glColor3f(0.0f, 0.0f, 1.0f); // Blue
            OpenGL.glVertex2f(0.0f, 0.5f);      // Top-center point
            OpenGL.glEnd();

            OpenGL.glFlush();
            OpenGL.SwapBuffers(_deviceContext);
        }

        public void Cleanup()
        {
            if (_renderingContext != IntPtr.Zero)
            {
                OpenGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                OpenGL.wglDeleteContext(_renderingContext);
            }
        }
    }
}
