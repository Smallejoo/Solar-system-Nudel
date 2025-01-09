using Microsoft.VisualBasic;
using OpenTK.Mathematics;
using Solar_system_Nudel.Classes.OpenGLFolder;
using Solar_system_Nudel.Classes.Support;
using Solar_system_Nudel.Textures;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Solar_system_Nudel.Classes
{
    public class OpenGLRenderer
    {
        int Width = 800;
        int Height = 600;

     
        //shadow related verbs .
        private uint depthFBO;
        private uint depthTexture;  
        private const int SHADOW_WIDTH = 1024;
        private const int SHADOW_HEIGHT = 1024;


        double[] Global_currMetrix = new double[16];

        private nint _deviceContext; // Overall area to draw
        private nint _renderingContext; // Instance of GL
        private nint _windowHandle; // The window pointer

        Space CurentSpace;
        public Solar_system_Nudel.Classes.Vector3 BasicCords = new Vector3(0.0f, 0.0f, 0.0f);
        private float rotationAngle = 0.0f;
        private float[] LightPosition = new float[4];
        int firstFrame = 0;
        Camera currentCam = new Camera();
        public int OutCommands = 0;
        bool isTextureEnabled = false;
        GLUquadric GlobalQuadric;
        private uint lastBoundTexture = 0;
        private double DeltaTime = 0.03;



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


         //   OpenGL.SwapBuffers(_windowHandle);// not sure it is necessery 


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
            OpenGL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);
           OpenGL.glClearDepth(1.0f);

            OpenGL.glEnable(OpenGL.GL_DEPTH_TEST);
            OpenGL.glDepthFunc(OpenGL.GL_LEQUAL);
            ////////////////////// Shadow //////////////////////////////
           //OpenGL.glEnable(OpenGL.GL_STENCIL_TEST);  // Enable stencil testing
            //OpenGL.glClearStencil(0);
            //////////////////////////////
            OpenGL.glEnable(OpenGL.GL_LIGHTING);
            OpenGL.glEnable(OpenGL.GL_LIGHT0);
            float[] ambientLight = { 0.5f, 0.5f, 0.5f, 1.0f };  // Ambient light (soft glow)
            float[] diffuseLight = { 1.0f, 1.0f, 1.0f, 1.0f };  // Diffuse light (stronger)
            float[] specularLight = { 1.0f, 1.0f, 1.0f, 1.0f }; // Specular highlight

            OpenGL.glLightfv(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambientLight);
            OpenGL.glLightfv(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, diffuseLight);
            OpenGL.glLightfv(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, specularLight);

           
            //LightPosition[0] = 0; LightPosition[1] = 5; LightPosition[2] = 10; LightPosition[3] = 1;
            float[] lightPos = { 0.0f, 0.0f, 0.0f, 1.0f };
            OpenGL.glLightfv(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPos);

            OpenGL.glEnable(OpenGL.GL_COLOR_MATERIAL);
            OpenGL.glColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE);

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D); // Enable textures
            OpenGL.glTexEnvf(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);


            OpenGL.glEnable(OpenGL.GL_DEPTH_TEST);
            OpenGL.glDepthFunc(OpenGL.GL_LEQUAL);
            OpenGL.glHint(OpenGL.GL_PERSPECTIVE_CORRECTION_Hint, OpenGL.GL_NICEST); 

            OpenGL.glViewport(0,0, Width, Height);  

            OpenGL.glMatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.glLoadIdentity();
            GLU.gluPerspective(45.0, 1.0, 0.4, 1000.0);


            OpenGL.glMatrixMode(OpenGL.GL_MODELVIEW);
            OpenGL.glLoadIdentity();

            //OpenGL.glEnable(OpenGL.GL_BLEND);
            OpenGL.glBlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            GlobalQuadric = GLU.gluNewQuadric();
            GLU.gluQuadricNormals(GlobalQuadric, GLU.GLU_SMOOTH);
            GLU.gluQuadricTexture(GlobalQuadric, 1);

            currentCam.InitCamera();
            CurentSpace = new Space();
            OpenGL.glLoadIdentity();
            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX,Global_currMetrix);
            InitSpace(); // putted it here cos it was eating up memory

        }
        public void RenderBackGround()
        {
            if(!isTextureEnabled)
            {
            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
                isTextureEnabled = true;
            }
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D,CurentSpace.SpaceImageID);
            OpenGL.glColor3f(1.0f, 1.0f, 1.0f);

            OpenGL.glPushMatrix();
            OpenGL.glTranslated(0.0f, 0.0f, 0.0f);
          //
          //OpenGL.glTranslated(currentCam.CameraPlacment_X, currentCam.CameraPlacment_Y, currentCam.CameraPlacment_Z);
            GLU.gluQuadricOrientation(GlobalQuadric, GLU.GLU_INSIDE);

            GLU.gluSphere(GlobalQuadric, 400, 30, 30);

            OpenGL.glPopMatrix();


            GLU.gluQuadricOrientation(GlobalQuadric, GLU.GLU_OUTSIDE);

        }


        public void updateMovmentCords()
        {
            switch (OutCommands)
            {
                case 1:
                    {
                        currentCam.MoveForward(0.3f);
                    }
                    break;

                case 2:
                    {
                        currentCam.MoveBack(0.3f);
                    }
                    break;

                case 3:
                    {
                        currentCam.MoveLeft(0.3f);
                    }
                    break;

                case 4:
                    {
                        currentCam.MoveRight(0.3f);
                    }
                    break;
                case 5:
                    {
                        currentCam.RotateCameLeft();
                        break;
                    }
                case 6:
                    {
                        currentCam.RotateCameRight();
                        break;
                    }

                case 7:
                    {
                        currentCam.UpCam();
                        break;
                    }
                case 8: 
                    {
                        currentCam.DownCam();
                        break;
                    }
                case 9:
                    {
                        currentCam.camUP();
                        break;
                    }
                    case 10:
                    {
                        currentCam.camDown();   
                        break;
                    }

                    case 11:
                    {
                        currentCam.camReset();
                        break;
                    }
            }
            OutCommands = 0; 
        }
        public void InitSpace()
        {
            CurentSpace = new Space();
            //OpenGL.glPushMatrix();
            

        }
        public int color = 0;
        public void PlanetShadows(Planet CurrPlanet)
        {
             Vector3 LightNormal= LightDirection(CurrPlanet.PlantPosition);

            OpenGL.glEnable(OpenGL.GL_LIGHTING);
            OpenGL.glEnable(OpenGL.GL_LIGHT0);

            float[] matAmbient = { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] matDiffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            OpenGL.glMaterialfv(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, matAmbient);
            OpenGL.glMaterialfv(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, matDiffuse);

            float[] lightDirection = { LightNormal.X, LightNormal.Y, LightNormal.Z, 0.0f }; // Directional light

          
           
            OpenGL.glLightfv(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightDirection);

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glTexEnvf(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, CurrPlanet.PlanetTextureID);
           // lastBoundTexture = CurrPlanet.PlanetTextureID;

          
        }

        public void RenderPlanet(Planet curentPlanet)
        {
            // PlanetShadows(curentPlanet);
            
                 if(!isTextureEnabled)
                    {
                     OpenGL.glEnable(OpenGL.GL_TEXTURE_2D); // Enable texturing
                     isTextureEnabled = true;
                    }

                 if (lastBoundTexture != curentPlanet.PlanetTextureID)
                   {
                     OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, curentPlanet.PlanetTextureID);
                    lastBoundTexture = curentPlanet.PlanetTextureID;
                   }
                     
            OpenGL.glColor3f(1.0f, 1.0f, 1.0f);
            

            OpenGL.glPushMatrix();

            OpenGL.glTranslated(curentPlanet.PlantPosition.X, curentPlanet.PlantPosition.Y, curentPlanet.PlantPosition.Z);
            OpenGL.glRotated(curentPlanet.SelfRotationAngle, 0, 1, 0);

           
            GLU.gluSphere(GlobalQuadric, curentPlanet.PlanetSize, 30, 30);


            OpenGL.glPopMatrix();

           

        }
       

        public void DrawSpace()
        {

            if (_deviceContext == nint.Zero || _renderingContext == nint.Zero)
                return;
            
            // the clear some times fucks with memorry too
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            OpenGL.glLoadIdentity(); // here too

            
            double[] Befor_Change_Metrix = new double[16]; // here too about 20 but rarly 
            double[] current_Change_Metrix = new double[16];

            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, Befor_Change_Metrix);  // here too i am leaking some 
            OpenGL.glDepthMask((byte)OpenGL.GL_TRUE);
            OpenGL.glDisable(OpenGL.GL_STENCIL_TEST);

            RenderBackGround();
            updateMovmentCords();
            currentCam.ApplyMovment();


            
            RenderPlanet(CurentSpace.MotherPlanet);// i think ate here too 
            RenderPlanets();

           // OpenGL.glDepthMask((byte)OpenGL.GL_FALSE);
            Sun deSun=(Sun)CurentSpace.MotherPlanet;
            deSun.RenderSunGlow(GlobalQuadric,(currentCam.CenterDis()));
            deSun.RenderSunGlowSphere(GlobalQuadric, (currentCam.CenterDis()));

            OpenGL.glDepthMask((byte)OpenGL.GL_TRUE); // importent 


            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, current_Change_Metrix);
            if(firstFrame==0)
            {
                OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX,Global_currMetrix);
                firstFrame++;
            }
            else
            {
                OpenGL.glMultMatrixd(Global_currMetrix);
            }
           OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, Global_currMetrix);

            OpenGL.glLoadMatrixd(Befor_Change_Metrix);
            OpenGL.glMultMatrixd(Global_currMetrix);

            OpenGL.glFlush();
            OpenGL.SwapBuffers(_deviceContext);
            

        }
        public void RenderPlanets()
        {
           
            foreach (Planet Curplanet in CurentSpace.PlanetList)// at some point eats about 20 memory for render planet 
            {
                RenderPlanet(Curplanet); // over 50 some times   
                Curplanet.UpdatePlanetPosition(DeltaTime);
                Curplanet.UpdateSelfRotation(DeltaTime);
                CurentSpace.MotherPlanet.UpdateSelfRotation(DeltaTime);
            }


        }
        public Vector3 LightDirection(Vector3 PlanetPos)
        {
            Vector3 sunPos = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 LightDirection = (sunPos.SubReturn(PlanetPos));
            LightDirection.Normalize();
            return LightDirection;
            
        }

        public void Draw()
        {
            if (_deviceContext == nint.Zero || _renderingContext == nint.Zero)
                return;
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT| OpenGL.GL_DEPTH_BUFFER_BIT| OpenGL.GL_STENCIL_BUFFER_BIT);
            OpenGL.glLoadIdentity();

            double[] Befor_Change_Metrix= new double[16];
            double[] current_Change_Metrix= new double[16];

            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, Befor_Change_Metrix); // need to  check the situation with
            // all the metrix and when to update them cos i just saved the befor one need to update the global one too . 
            OpenGL.glDepthMask((byte)OpenGL.GL_TRUE);
            OpenGL.glDisable(OpenGL.GL_STENCIL_TEST);
            //GLU.gluLookAt(0, 20, 50,
            //              0, 8 ,  1,
            //              0, 1 ,  0);
            updateMovmentCords();
            currentCam.ApplyMovment();
            DrawFloor();
    

            OpenGL.glRotatef(rotationAngle,0,1,0);
            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, current_Change_Metrix);

            //OpenGL.glLoadMatrixd(Global_currMetrix);
            //OpenGL.glMultMatrixd(current_Change_Metrix);
            if(firstFrame == 0)
            {
            OpenGL.glMultMatrixd(Global_currMetrix);
                firstFrame++;
            }

            OpenGL.glGetDoublev(OpenGL.GL_MODELVIEW_MATRIX, Global_currMetrix);

            OpenGL.glLoadMatrixd(Befor_Change_Metrix);



            OpenGL.glMultMatrixd(Global_currMetrix);

            rotationAngle++;   
            DrawFigure();
            OpenGL.glFlush();
            OpenGL.SwapBuffers(_deviceContext);
        }
        void DrawFloor()
        {
            OpenGL.glEnable(OpenGL.GL_LIGHTING);
            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glColor4d(1, 1, 1, 1);
            OpenGL.glVertex3d(5, 0, 5);
            OpenGL.glVertex3d(5, 0,-5);
            OpenGL.glVertex3d(-5, 0, -5);
            OpenGL.glVertex3d(-5,0 , 5);
            OpenGL.glEnd();

        }
        void DrawBigWall()
        {
            OpenGL.glPushMatrix();
            OpenGL.glTranslated(-50,0,0);
            OpenGL.glEnable(OpenGL.GL_LIGHTING);
            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glColor4d(1, 1, 1, 1);
            OpenGL.glVertex3d(0, 30, 30);
            OpenGL.glVertex3d(0, 30, -30);
            OpenGL.glVertex3d(0, -30, -30);
            OpenGL.glVertex3d(0, -30, 30);
            OpenGL.glEnd();
            OpenGL.glPopMatrix();
        }
        void DrawBigFloor()
        {
            OpenGL.glEnable(OpenGL.GL_LIGHTING);
            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glColor4d(1, 1, 1, 1);
            OpenGL.glVertex3d(30, 0, 30);
            OpenGL.glVertex3d(30, 0, -30);
            OpenGL.glVertex3d(-30, 0, -30);
            OpenGL.glVertex3d(-30, 0, 30);
            OpenGL.glEnd();

        }

        void DrawFigure()
        {
            OpenGL.glPushMatrix();
            // OpenGL.glDisable(OpenGL.GL_LIGHTING);

            OpenGL.glColor3f(1, 1, 0);
            OpenGL.glTranslatef(0, 2, 2);
            OpenGL.glColor3f(0,1,0);
            BasicFiguresGL.glutSolidTeapot(1.0f);

            OpenGL.glPopMatrix();

        }

        private void RenderSphere(double radius)
        {
            var quadric = GLU.gluNewQuadric();
            GLU.gluQuadricNormals(quadric, GLU.GLU_SMOOTH); // Smooth lighting
            GLU.gluSphere(quadric, radius, 30, 30); // Render the sphere
            GLU.gluDeleteQuadric(quadric); // Clean up
        }

        public void Draw3Shapes()
        {
            if (_deviceContext == nint.Zero || _renderingContext == nint.Zero)
                return;

            // Clear the screen and depth buffer
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            OpenGL.glLoadIdentity(); // Reset the MODELVIEW matrix

            // Apply the camera movement
            updateMovmentCords();
            currentCam.ApplyMovment();

            // Render Figure 1 (Sphere 1)
            OpenGL.glPushMatrix();
            OpenGL.glTranslated(-5.0, 0.0, -10.0); // Move left and slightly back
            OpenGL.glColor3f(1.0f, 0.0f, 0.0f); // Red
            RenderSphere(1.0);
            OpenGL.glPopMatrix();

            // Render Figure 2 (Sphere 2)
            OpenGL.glPushMatrix();
            OpenGL.glTranslated(0.0, 0.0, -10.0); // Center
            OpenGL.glColor3f(0.0f, 1.0f, 0.0f); // Green
            RenderSphere(1.5);
            OpenGL.glPopMatrix();

            // Render Figure 3 (Sphere 3)
            OpenGL.glPushMatrix();
            OpenGL.glTranslated(5.0, 0.0, -10.0); // Move right and slightly back
            OpenGL.glColor3f(0.0f, 0.0f, 1.0f); // Blue
            RenderSphere(2.0);
            OpenGL.glPopMatrix();

            // Finalize rendering
            OpenGL.glFlush();
            OpenGL.SwapBuffers(_deviceContext); // Display the frame
        }


        public void Resize(int newWidth, int newHeight)
        {
            Width = newWidth;
            Height = newHeight;

            // Update the viewport
            OpenGL.glViewport(0, 0, Width, Height);

            // Update the projection matrix
            SetupPerspective();
        }
        public void SetupPerspective()
        {
            OpenGL.glMatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.glLoadIdentity();
            GLU.gluPerspective(45.0f, (double)Width / Height, 0.1f, 100.0f);
            OpenGL.glMatrixMode(OpenGL.GL_MODELVIEW);
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
