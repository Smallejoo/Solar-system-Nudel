using Solar_system_Nudel.Classes.OpenGLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Solar_system_Nudel.Classes.Support
{
    public class Camera
    {
        private float CameraPlacment_X = 0.0f;
        private float CameraPlacment_Y = 0.0f;
        private float CameraPlacment_Z = 0.0f;

        private float EyeSight_X = 0.0f;
        private float EyeSight_Y = 0.0f;
        private float EyeSight_Z = 0.0f;

        private float CamRotate_X = 0.0f;
        private float CamRotate_Y = 0.0f;
        private float CamRotate_Z = 0.0f;

        public float Horizontal_Angle = 0.0f;
        public float Vertical_Angle = 0.0f;

        public Camera() {
            CameraPlacment_Y = 20f;
            CameraPlacment_Z = 30f;
            EyeSight_Y = 8.0f;
            EyeSight_Z = 1.0f;
            CamRotate_Y = 1.0f;


        }
        public void InitCamera()
        {
            this.CameraPlacment_Y = 20f;
            this.CameraPlacment_Z = 30f;
            this.EyeSight_Y = 8.0f;
            this.EyeSight_Z = 1.0f;
            this.CamRotate_Y = 1.0f;
            this.ApplyMovment();

        }
        public void MoveRight(float MovmentDirection)
        {
            // CameraPlacment_X += MovmentDirection;

            CameraPlacment_X -= (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * 1;
            CameraPlacment_Z -= (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * 1;
        }
        public void Moveleft(float MovmentDirection)
        {
            CameraPlacment_X += (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * 1;
            CameraPlacment_Z +=(float)Math.Sin(DegreesToRadians(Horizontal_Angle))*1;
        }
        public void MoveBack(float MovmentDirection)
        {
            //           CameraPlacment_Z += MovmentDirection;
            CameraPlacment_X -= (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * 1;

            CameraPlacment_Y -= (float)Math.Sin(DegreesToRadians(Vertical_Angle)) * 1;

            CameraPlacment_Z += (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * 1;

        }
        public void MoveForward(float MovmentDirection)
        {
            // CameraPlacment_Z -= MovmentDirection;

            CameraPlacment_X += (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                *(float)Math.Sin(DegreesToRadians(Horizontal_Angle))*1;

            CameraPlacment_Y += (float)Math.Sin(DegreesToRadians(Vertical_Angle))*1;

            CameraPlacment_Z -= (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                *(float)Math.Cos(DegreesToRadians(Horizontal_Angle))*1;
        }
        private float DegreesToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }
        public void RotateCameRight()
        {
            Horizontal_Angle += 1.0f;
        }
        public void RotateCameLeft()
        {
            Horizontal_Angle -= 1.0f;
        }
        public void UpCam()
        {
            CameraPlacment_Y += 1.0f;
        }
        public void DownCame()
        {
            CameraPlacment_Y -= 1.0f;
        }
        public void ApplyMovment()
        {
            EyeSight_X = CameraPlacment_X+(float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                        *(float)Math.Sin(DegreesToRadians(Horizontal_Angle));

            EyeSight_Y = CameraPlacment_Y + (float)Math.Sin(DegreesToRadians(Vertical_Angle)) ;

            EyeSight_Z = CameraPlacment_Z- (float)Math.Cos(DegreesToRadians(Horizontal_Angle))
                                           *(float)Math.Cos(DegreesToRadians(Horizontal_Angle));

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine($"Camera Position:    X: {CameraPlacment_X}, Y: {CameraPlacment_Y}, Z: {CameraPlacment_Z}");
            Console.WriteLine($"Eye Direction:      X: {EyeSight_X}, Y: {EyeSight_Y}, Z: {EyeSight_Z}");
            Console.WriteLine($"Camera Rotation:    X: {CamRotate_X}, Y: {CamRotate_Y}, Z: {CamRotate_Z}");
            Console.WriteLine("-------------------------------------------------------------");

            GLU.gluLookAt(CameraPlacment_X, CameraPlacment_Y,CameraPlacment_Z
                          ,EyeSight_X, EyeSight_Y,EyeSight_Z,
                           CamRotate_X, CamRotate_Y, CamRotate_Z);
        }

    }
}
