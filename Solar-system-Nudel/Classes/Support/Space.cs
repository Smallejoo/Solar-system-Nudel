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
        MotherPlanet= new Planet("Sun",BasicCordsSun,null,10,1.0f,0.3f,0.3f,0,true,0.5);
            MotherPlanet.MainStar=true;
            MotherPlanet.PlanetTextureID = TextureRender.LoadTextur(MotherPlanet.PlanetName);

            EarthSolarSystem();
        }
        public Planet MotherPlanet;
        public List<Planet> PlanetList= new List<Planet>(); 
        public Bitmap SpaceTexture;
        public Vector3<double> BasicCords=new Vector3<double>(0.0,0.0,0.0);
        public uint SpaceImageID;
        public Vector3<double> BasicCordsSun = new Vector3<double>(0.0, 0.0, 0.0);

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
            BasicCords.Z = -20;
            Planet Fire = new Planet("Mercury", BasicCords, MotherPlanet, 2,1.0f,1.0f,0.0f,5,false,1);
            BasicCords.Z -= 20;
            Planet Fire2 = new Planet("Venus", BasicCords, MotherPlanet, 3, 0.5f, 0.5f, 0.0f,4,false,1);
            BasicCords.Z -= 20;
            Planet Earth = new Planet("Earth", BasicCords, MotherPlanet, 3, 0.0f, 0.0f, 1.0f,3,false,0.5);
            BasicCords.Z -= 20;
            Planet Mars = new Planet("Mars", BasicCords, MotherPlanet, 4, 1.0f, 1.0f, 1.0f,1,false,0.5);
            BasicCords.Put(Earth.PlantPosition.X +8 , Earth.PlantPosition.Y, Earth.PlantPosition.Z);
            Planet Moon = new Planet("Moon", BasicCords, Earth, 1, 1.0f, 1.0f, 1.0f, 4,false,0.7);

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
            Moon.PlanetTextureID = TextureRender.LoadTextur(Moon.PlanetName);


            


            PlanetList.Add (Earth);
            PlanetList.Add (Mars);
            PlanetList.Add (Fire2);
            PlanetList.Add (Fire);
            PlanetList.Add(Moon);

            SpaceImageID = TextureRender.LoadTextur("Space");

        }

        public void EarthSolarSystem1()
        {
            BasicCords.Z = -5;
            Planet Fire = new Planet("Mercury", BasicCords, MotherPlanet, 1, 1.0f, 1.0f, 0.0f);
            BasicCords.Z -= 10;
            Planet Fire2 = new Planet("Venus", BasicCords, MotherPlanet, 1, 0.5f, 0.5f, 0.0f);
            BasicCords.Z -= 10;
            Planet Earth = new Planet("Earth", BasicCords, MotherPlanet, 1, 0.0f, 0.0f, 1.0f);
            BasicCords.Z -= 10;
            Planet Mars = new Planet("Mars", BasicCords, MotherPlanet, 1, 1.0f, 1.0f, 1.0f);


            Fire.PlanetTextureID = TextureRender.LoadTextur(Fire.PlanetName);
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






            PlanetList.Add(Earth);
            PlanetList.Add(Mars);
            PlanetList.Add(Fire2);
            PlanetList.Add(Fire);

            SpaceImageID = TextureRender.LoadTextur("Space");
        }

    }

}
