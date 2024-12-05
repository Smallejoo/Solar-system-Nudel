using Microsoft.VisualBasic;
using Solar_system_Nudel.Classes.OpenGLFolder;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Solar_system_Nudel.Classes
{
    public class OpenGLRenderer
    {
        int Width = 800;
        int Height = 600;

        private nint _deviceContext; // Overall area to draw
        private nint _renderingContext; // Instance of GL
        private nint _windowHandle; // The window pointer

       

        public OpenGLRenderer(nint windowHandle)
        {
            _windowHandle = windowHandle;
            Console.WriteLine($"Window Handle: {_windowHandle}");
            InitializeOpenGL();
            InitRender();
        }

        public bool InitializeOpenGL()
        {
            Console.WriteLine($"Initializing OpenGL with Window Handle: {_windowHandle}");

            _deviceContext = OpenGL.GetDC(_windowHandle);


            OpenGL.SwapBuffers(_windowHandle);// not sure it is necessery 


            WGL.PIXELFORMATDESCRIPTOR Pixel = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor( ref Pixel );
            Pixel.nVersion = 1;
            Pixel.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            Pixel.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            Pixel.cColorBits = 32;
            Pixel.cDepthBits = 32;
            Pixel.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);
            Pixel.cStencilBits = 32;

            int PixelSucsedded = 0;
            PixelSucsedded = OpenGL.ChoosePixelFormat(_deviceContext, ref Pixel);
            if(PixelSucsedded==0)
            {
                MessageBox.Show("unable to retrive pixel format");
                return false;
            }

            if (_deviceContext == nint.Zero)
            {
                MessageBox.Show("Failed to get device context");
                return false;
            }
            if(WGL.SetPixelFormat(_deviceContext,PixelSucsedded,ref Pixel)==0 )
            {
                MessageBox.Show(" failed to set pixel format");
            }
            

            _renderingContext = OpenGL.wglCreateContext(_deviceContext);
            if (_renderingContext == nint.Zero)
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
            if (_deviceContext == nint.Zero || _renderingContext == nint.Zero)
                return;
        

            OpenGL.glShadeModel(OpenGL.GL_SMOOTH);

            OpenGL.glClearColor(0.0f, 0.0f, 0.0f, 0.0f); // Background color
           
            
        }

        public void Draw()
        {
            if (_deviceContext == nint.Zero || _renderingContext == nint.Zero)
                return;

            Console.WriteLine("Clearing buffers...");
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT); // Clear the color buffer

            Console.WriteLine("Drawing 2D features...");

           // OpenGL.glPointSize(10.0f); // Set point size for visibility

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
            if (_renderingContext != nint.Zero)
            {
                OpenGL.wglMakeCurrent(nint.Zero, nint.Zero);
                OpenGL.wglDeleteContext(_renderingContext);
            }
        }
    }
}
