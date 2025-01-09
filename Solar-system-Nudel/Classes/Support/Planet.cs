using Solar_system_Nudel.Classes.OpenGLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes.Support
{
    public class Sun:Planet
    {

        public static float lightSTR = 0.5f;
        public static int lightUp = 1;
        public static int minDistance = 50;
        public static int maxDistance = 200;
       
        public uint SunBlureEffectID { get; set; }
        public uint SunFlameEffectID { get; set; }


        public Sun(string Name, Vector3 cordinats, Planet Orbiting, double size,
            float col1, float col2, float col3
            , double PlanetSpeed, bool MainStar, double RotationSpeed)
            :base(Name, cordinats, Orbiting, size, col1, col2, col3,PlanetSpeed,MainStar,RotationSpeed)
            {
            

            }
        public void RenderSunGlowSphere(GLUquadric qudric, float DistanceFromCenter)
        {
            // Enable blending for transparency
            OpenGL.glEnable(OpenGL.GL_BLEND);
            OpenGL.glBlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            // Enable textures
            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, this.SunBlureEffectID); // Glow texture

            OpenGL.glPushMatrix();

            // Translate to the sun's position
            OpenGL.glTranslatef(this.PlantPosition.X, this.PlantPosition.Y, this.PlantPosition.Z);

            // Calculate pulsating effect (optional)
            float time = (float)DateTime.Now.TimeOfDay.TotalSeconds;
            float scale = (float)this.PlanetSize * (1.2f + 0.1f * MathF.Sin(time * 2.0f));
            OpenGL.glScalef(scale, scale, scale);

            // Set color for transparency (yellowish glow)
            OpenGL.glColor4f(1.0f, 1.0f, 0.5f, 0.3f);

            // Render the semi-transparent sphere
            GLU.gluQuadricTexture(qudric, 1);
            GLU.gluQuadricNormals(qudric, GLU.GLU_SMOOTH);
            GLU.gluSphere(qudric, 1.0, 30, 30); // Unit sphere, scaled above

            OpenGL.glPopMatrix();

            // Disable blending and textures
            OpenGL.glDisable(OpenGL.GL_BLEND);
         //   OpenGL.glDisable(OpenGL.GL_TEXTURE_2D);
        }

        public void RenderSunGlow(GLUquadric qudric,float DistanceFromCenter)
        {

            OpenGL.glEnable(OpenGL.GL_BLEND);
            OpenGL.glBlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glTexEnvf(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);


            OpenGL.glDepthMask((byte)OpenGL.GL_FALSE);

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D,this.SunBlureEffectID);
            OpenGL.glPushMatrix();

            OpenGL.glTranslatef(this.PlantPosition.X, this.PlantPosition.Y, this.PlantPosition.Z);
            float size = 0;

            float minSize = (float)this.PlanetSize + 1f;
            float maxSize = (float)this.PlanetSize + 50f;



            if (DistanceFromCenter > maxDistance)
            {

                size = maxSize-32;
            }
            else if(DistanceFromCenter<80)
            {
                size=minSize;

            }
            else if(DistanceFromCenter>=80&&DistanceFromCenter<170)
            {
            float precent = (DistanceFromCenter -80)/(maxDistance- 80);   
            size =minSize+(precent/4) *(maxSize-minSize);
            

            }
            else if(DistanceFromCenter>170)
            {
                float precent = (DistanceFromCenter - 80) / (maxDistance - 80);
                size = precent/1.8f * (maxSize - minSize);
                //size = (float)this.PlanetSize;
            }


            Console.WriteLine($"distance from center :::{DistanceFromCenter}");
            OpenGL.glColor4f(1.0f, 1.0f, lightSTR, 0.5f);
            GLU.gluSphere(qudric, size, 30, 30);


            if(lightUp==1)
            {
                lightSTR += 0.009f;
            }
            else if(lightUp==0)
            {
                lightSTR -= 0.009f;
            }
            if(lightSTR>=1.0f)
            {
                lightUp = 0;
            }
            else if(lightSTR<=0.5f)
            {
                lightUp = 1;
            }


            OpenGL.glPopMatrix();
            OpenGL.glDisable(OpenGL.GL_BLEND);

           
         }


    }
    public class Planet
    {
        public bool MainStar { get; set; }
        public string PlanetName { get; set; }
        public Vector3 PlantPosition { get; set; }
        public Planet PlanetOrbiting { get; set; }
        public uint PlanetTextureID { get; set; }
        
        public double PlanetSize { get; set; }
        public double PlanetSpeed { get; set; }
        public float PlanetOrbitAngle { get; set; }
        public float CenterRadius { get; set; }

        public double SelfRotationAngle { get; set; }
        public double SelfRotationSpeed { get; set; }
        public Vector3 PlanetColor { get; set; }

        public Planet(string Name, Vector3 cordinats, Planet Orbiting, double size,
            float col1, float col2, float col3
            ,double PlanetSpeed,bool MainStar ,double RotationSpeed)
        {
            this.SelfRotationAngle = 0;
            this.SelfRotationSpeed = RotationSpeed;
            this.MainStar = MainStar;
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;
            this.PlanetSpeed = PlanetSpeed;
            this.PlantPosition = new Vector3();
            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3 color = new Vector3(col1, col2, col3);
            PlanetColor = color;
            if(!this.MainStar)
            {
            double OrbitRadius = Math.Sqrt(
                   Math.Pow(this.PlanetOrbiting.PlantPosition.X - this.PlantPosition.X, 2) +
                   Math.Pow(this.PlanetOrbiting.PlantPosition.Z - this.PlantPosition.Z, 2)
               ); // Calculate orbit radius
                  this.CenterRadius = (float)OrbitRadius;
            }

        }// pointers bugs ... 

        public Planet(string Name,Vector3 cordinats ,Planet Orbiting ,double size ,float col1,float col2,float col3)
        {
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;

            this.PlantPosition = new Vector3();
            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3 color = new Vector3(col1, col2, col3);
            PlanetColor=color;

        }// pointers bugs ... 
        public Planet(string Name, Vector3 cordinats, Planet Orbiting, double size)
        {
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;
            this.PlantPosition = new Vector3();

            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3 color = new Vector3(0.0f, 0.0f, 0.0f);
            PlanetColor=color;

        }
        public Planet(string Name, float cord_X,float cord_Y,float cord_Z, Planet Orbiting, double size)
        {
            this.PlanetName = Name;

           
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3 cordinats = new Vector3(cord_X,cord_Y,cord_Z);
            PlantPosition = cordinats;
            Vector3 color = new Vector3(0.0f, 0.0f, 0.0f);
            PlanetColor = color;

        }
    
        public void UpdateSelfRotation(double DeltaTime)
        {
            this.SelfRotationAngle += DeltaTime*this.SelfRotationSpeed;
            this.SelfRotationAngle %= 360;
            Console.WriteLine($"planet self engle {this.SelfRotationAngle:F2}");

        }
        public void UpdatePlanetPosition(double DeltaTime)
        {
            if(!(this.PlanetName=="Sun"))
            {
                this.PlanetOrbitAngle +=(float) DeltaTime * (float)PlanetSpeed; // Increment the angle
                this.PlanetOrbitAngle %= 360; // Keep the angle within 0-360 degrees

                double RadientAngle = Math.PI * this.PlanetOrbitAngle / 180.0; // Convert to radians

               
                // Update X and Z positions
                this.PlantPosition.X = PlanetOrbiting.PlantPosition.X + this.CenterRadius * (float)(Math.Cos(RadientAngle));
                this.PlantPosition.Z = PlanetOrbiting.PlantPosition.Z + this.CenterRadius * (float)(Math.Sin(RadientAngle));

                // For Y-axis, keep it the same height as the orbiting center
                this.PlantPosition.Y = PlanetOrbiting.PlantPosition.Y;
            }
           
            if (this.PlanetName=="Moon"||this.PlanetName=="Earth")
            {
                if(this.PlanetName == "Moon")
            Console.WriteLine($"\nPlanet Name {this.PlanetName}: x={this.PlantPosition.X:F2} y={this.PlantPosition.Y:F2} z={this.PlantPosition.Z:F2}");
            
            
                if (this.PlanetName == "Earth")
                    Console.WriteLine($"Planet Name {this.PlanetName}: x={this.PlantPosition.X:F2} y={this.PlantPosition.Y:F2} z={this.PlantPosition.Z:F2}");

            
            }

        }
    }
}
