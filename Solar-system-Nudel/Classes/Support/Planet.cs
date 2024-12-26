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
        public Planet(string Name,Vector3<double> cordinats ,Planet Orbiting ,double size ,float col1,float col2,float col3)
        {
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
        public bool MainStar { get; set; }  
        public string PlanetName { get; set; }
        public Vector3<double> PlantPosition { get; set; }
        public Planet PlanetOrbiting { get; set; }
        public uint PlanetTextureID { get; set; } 
        public double PlanetSize { get; set; }  
        public int PlanetSpeed { get; set; } 
        

        public Vector3<float> PlanetColor {  get; set; }

    }
}
