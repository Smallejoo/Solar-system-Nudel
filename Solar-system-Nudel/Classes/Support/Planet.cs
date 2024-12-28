using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes.Support
{
    public class Planet
    {
        public bool MainStar { get; set; }
        public string PlanetName { get; set; }
        public Vector3<double> PlantPosition { get; set; }
        public Planet PlanetOrbiting { get; set; }
        public uint PlanetTextureID { get; set; }
        public double PlanetSize { get; set; }
        public double PlanetSpeed { get; set; }
        public double PlanetOrbitAngle { get; set; }
        public double CenterRadius { get; set; }

        public double SelfRotationAngle { get; set; }
        public double SelfRotationSpeed { get; set; }
        public Vector3<float> PlanetColor { get; set; }

        public Planet(string Name, Vector3<double> cordinats, Planet Orbiting, double size,
            float col1, float col2, float col3
            ,double PlanetSpeed,bool MainStar ,double RotationSpeed)
        {
            this.SelfRotationAngle = 0;
            this.SelfRotationSpeed = RotationSpeed;
            this.MainStar = MainStar;
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;
            this.PlanetSpeed = PlanetSpeed;
            this.PlantPosition = new Vector3<double>();
            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3<float> color = new Vector3<float>(col1, col2, col3);
            PlanetColor = color;
            if(!this.MainStar)
            {
            double OrbitRadius = Math.Sqrt(
                   Math.Pow(this.PlanetOrbiting.PlantPosition.X - this.PlantPosition.X, 2) +
                   Math.Pow(this.PlanetOrbiting.PlantPosition.Z - this.PlantPosition.Z, 2)
               ); // Calculate orbit radius
                  this.CenterRadius = OrbitRadius;
            }

        }// pointers bugs ... 

        public Planet(string Name,Vector3<double> cordinats ,Planet Orbiting ,double size ,float col1,float col2,float col3)
        {
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;

            this.PlantPosition = new Vector3<double>();
            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3<float> color = new Vector3<float>(col1, col2, col3);
            PlanetColor=color;

        }// pointers bugs ... 
        public Planet(string Name, Vector3<double> cordinats, Planet Orbiting, double size)
        {
            this.PlanetOrbitAngle = 0;
            this.PlanetName = Name;
            this.PlantPosition = new Vector3<double>();

            PlantPosition.Put(cordinats);
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3<float> color = new Vector3<float>(0.0f, 0.0f, 0.0f);
            PlanetColor=color;

        }
        public Planet(string Name, double cord_X,double cord_Y,double cord_Z, Planet Orbiting, double size)
        {
            this.PlanetName = Name;

           
            this.PlanetOrbiting = Orbiting;
            this.PlanetSize = size;
            Vector3<double> cordinats = new Vector3<double>(cord_X,cord_Y,cord_Z);
            PlantPosition = cordinats;
            Vector3<float> color = new Vector3<float>(0.0f, 0.0f, 0.0f);
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
                this.PlanetOrbitAngle += DeltaTime * PlanetSpeed; // Increment the angle
                this.PlanetOrbitAngle %= 360; // Keep the angle within 0-360 degrees

                double RadientAngle = Math.PI * this.PlanetOrbitAngle / 180.0; // Convert to radians

               
                // Update X and Z positions
                this.PlantPosition.X = PlanetOrbiting.PlantPosition.X + this.CenterRadius * Math.Cos(RadientAngle);
                this.PlantPosition.Z = PlanetOrbiting.PlantPosition.Z + this.CenterRadius * Math.Sin(RadientAngle);

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
