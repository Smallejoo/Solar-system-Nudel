using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes.Support
{
    public class Space
    {
       
        public Space()
        { 
        MotherPlanet= new Planet("SUN",BasicCords,null,3,1.0f,0.3f,0.3f);
            MotherPlanet.MainStar=true;
            
        }
        public Planet MotherPlanet;
        public List<Planet> PlanetList= new List<Planet>(); 
        public Bitmap SpaceTexture;
        public Vector3<double> BasicCords=new Vector3<double>(0.0,0.0,0.0);
        
        
        public bool AddNewPlant(Planet newPlanet)
        {
            try
            {

                PlanetList.Add(newPlanet);
                
            }catch
            {
                return false;
            }
            return true;
        }
              public void EarthSolarSystem ()
             {
            BasicCords.Z = 5;
            Planet Fire = new Planet("fire", BasicCords, MotherPlanet, 1,1.0f,1.0f,0.0f);
            BasicCords.Z += 10;
            Planet Fire2 = new Planet("fire2", BasicCords, MotherPlanet, 1, 0.5f, 0.5f, 0.0f);
            BasicCords.Z += 10;
            Planet Earth = new Planet("earth", BasicCords, MotherPlanet, 1, 0.0f, 0.0f, 1.0f);
            BasicCords.Z += 10;
            Planet Platon = new Planet("platon", BasicCords, MotherPlanet, 1, 1.0f, 1.0f, 1.0f);

            
            PlanetList.Add (Earth);
            PlanetList.Add (Platon);
            PlanetList.Add (Fire2);
            PlanetList.Add (Fire);
        }
    }
}
