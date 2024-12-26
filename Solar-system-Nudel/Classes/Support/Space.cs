using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.PixelFormats;
using Solar_system_Nudel.Classes.OpenGLFolder;
using Solar_system_Nudel.Textures;
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
        MotherPlanet= new Planet("Sun",BasicCords,null,3,1.0f,0.3f,0.3f);
            MotherPlanet.MainStar=true;
            MotherPlanet.PlanetTextureID = TextureRender.LoadTextur(MotherPlanet.PlanetName);

            EarthSolarSystem();
        }
        public Planet MotherPlanet;
        public List<Planet> PlanetList= new List<Planet>(); 
        public Bitmap SpaceTexture;
        public Vector3<double> BasicCords=new Vector3<double>(0.0,0.0,0.0);
        public uint SpaceImageID;
        
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
            Planet Fire = new Planet("Mercury", BasicCords, MotherPlanet, 1,1.0f,1.0f,0.0f);
            BasicCords.Z += 10;
            Planet Fire2 = new Planet("Venus", BasicCords, MotherPlanet, 1, 0.5f, 0.5f, 0.0f);
            BasicCords.Z += 10;
            Planet Earth = new Planet("Earth", BasicCords, MotherPlanet, 1, 0.0f, 0.0f, 1.0f);
            BasicCords.Z += 10;
            Planet Mars = new Planet("Mars", BasicCords, MotherPlanet, 1, 1.0f, 1.0f, 1.0f);


            Fire.PlanetTextureID=TextureRender.LoadTextur(Fire.PlanetName);
            //TextureRender.Dis();
           // ImgLoad.Dis(Fire.PlanetName);
            Fire2.PlanetTextureID = TextureRender.LoadTextur(Fire2.PlanetName);
            //TextureRender.Dis();
            //ImgLoad.Dis(Fire2.PlanetName);
            Earth.PlanetTextureID = TextureRender.LoadTextur(Earth.PlanetName);
            //TextureRender.Dis();
            //ImgLoad.Dis(Earth.PlanetName);
            Mars.PlanetTextureID = TextureRender.LoadTextur(Mars.PlanetName);
            //TextureRender.Dis();
            //ImgLoad.Dis(Mars.PlanetName);



            


            PlanetList.Add (Earth);
            PlanetList.Add (Mars);
            PlanetList.Add (Fire2);
            PlanetList.Add (Fire);

            SpaceImageID = TextureRender.LoadTextur("Space");
        }
    }
}
