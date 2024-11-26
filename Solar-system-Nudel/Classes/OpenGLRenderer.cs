using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes
{
    public class OpenGLRenderer
    {
        private IntPtr _deviceContext; // over all area you gonna draw
        private IntPtr _renderingContext; // instance of GL
        private IntPtr _windowHandle;       //the window pointer 
        [DllImport("gdi32.dll", EntryPoint = "ChoosePixelFormat")]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR p_pfd);

        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat")]
        public static extern bool SetPixelFormat(IntPtr hdc, int format, ref PIXELFORMATDESCRIPTOR p_pfd);
      
        private bool SetOpenGLPixelFormat(IntPtr deviceContext)
        {
            PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR();
            pfd.nSize = (ushort)Marshal.SizeOf(pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = 0x00000004 | 0x00000020;
            pfd.iPixelType = 0; // PFD_TYPE_RGBA
            pfd.cColorBits = 32;
            pfd.cDepthBits = 24;
            pfd.iLayerType = 0; // PFD_MAIN_PLANE
            int pixelFormatIndex = ChoosePixelFormat(deviceContext, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Failed to choose a pixel format.");
                return false;
            }

            if (!SetPixelFormat(deviceContext, pixelFormatIndex, ref pfd))
            {
                MessageBox.Show("Failed to set the pixel format.");
                return false;
            }

            return true;
        }

        public OpenGLRenderer(IntPtr windowHandle)   // constructor for the instance we are moving 
        {                                            // a refference of the form . 
            _windowHandle = windowHandle;
        }
        public bool InitializeOpenGL()
        {

            _deviceContext = OpenGL.GetDC(_windowHandle);//from the window we give context where to draw
            if( _deviceContext == IntPtr.Zero ) 
            {
                
                    MessageBox.Show("Failed to get device context");
                    return false;
            }
            if(!SetOpenGLPixelFormat(_deviceContext))
            {
                MessageBox.Show("Failed to set the pixel format");
                return false;
            }
            _renderingContext = OpenGL.wglCreateContext(_deviceContext); // we create the functionality for this window 
                                                                         
            if (_renderingContext == IntPtr.Zero)
            {
                MessageBox.Show("Failed to create OpenGL rendering context");
                return false;
            }

            if (!OpenGL.wglMakeCurrent(_deviceContext, _renderingContext)) // we make it the current working on 
            {
                MessageBox.Show("Failed to make OpenGL context current");
                return false;
            }

            return true;                    //if all worked till now return true . 
        }
        public void Render()
        {
            OpenGL.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            OpenGL.glClear(0x00004000);

            OpenGL.glBegin(0x0004);

            OpenGL.glColor3f(1.0f, 0.0f, 0.0f); // Red color
            OpenGL.glVertex2f(-0.5f, -0.5f);

            OpenGL.glColor3f(0.0f, 1.0f, 0.0f); // Green color
            OpenGL.glVertex2f(0.5f, -0.5f);

            OpenGL.glColor3f(0.0f, 0.0f, 1.0f); // Blue color
            OpenGL.glVertex2f(0.0f, 0.5f); // point cords 

            OpenGL.glEnd();
            OpenGL.glFlush();
            OpenGL.SwapBuffers(_deviceContext);

        }

        public void Cleanup()
        {
            if(_renderingContext !=IntPtr.Zero)
            {
                OpenGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                OpenGL.wglDeleteContext(_renderingContext);


            }
        }


    }
}
