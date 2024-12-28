using Solar_system_Nudel.Classes.OpenGLFolder;
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
        private float CamRotate_Y = 1.0f; // Default "up" direction
        private float CamRotate_Z = 0.0f;

        public float Horizontal_Angle = 0.0f; // Yaw
        public float Vertical_Angle = 0.0f;   // Pitch

        public Camera()
        {
            InitCamera();
        }

        public void InitCamera()
        {
            CameraPlacment_Y = 20f;
            CameraPlacment_Z = 30f;

            EyeSight_Y = 8.0f;
            EyeSight_Z = 1.0f;

            Horizontal_Angle = 0.0f;
            Vertical_Angle = 0.0f;

            CamRotate_X = 0.0f;
            CamRotate_Y = 1.0f; // Default up vector
            CamRotate_Z = 0.0f;

            ApplyMovment();
        }

        public void MoveRight(float MovmentDirection)
        {
            CameraPlacment_X -= (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
            CameraPlacment_Z -= (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
        }

        public void MoveLeft(float MovmentDirection)
        {
            CameraPlacment_X += (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
            CameraPlacment_Z += (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
        }

        public void MoveBack(float MovmentDirection)
        {
            CameraPlacment_X -= (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;

            CameraPlacment_Y -= (float)Math.Sin(DegreesToRadians(Vertical_Angle)) * MovmentDirection;

            CameraPlacment_Z += (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
        }

        public void MoveForward(float MovmentDirection)
        {
            CameraPlacment_X += (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Sin(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;

            CameraPlacment_Y += (float)Math.Sin(DegreesToRadians(Vertical_Angle)) * MovmentDirection;

            CameraPlacment_Z -= (float)Math.Cos(DegreesToRadians(Vertical_Angle))
                                * (float)Math.Cos(DegreesToRadians(Horizontal_Angle)) * MovmentDirection;
        }

        public void RotateCameRight()
        {
            Horizontal_Angle += 3.0f;
            Horizontal_Angle %= 360.0f; // Wrap around to stay within 0-360 degrees
        }

        public void RotateCameLeft()
        {
            Horizontal_Angle -= 3.0f;
            if (Horizontal_Angle < 0) Horizontal_Angle += 360.0f; // Wrap around to positive
        }

        public void UpCam()
        {
            Vertical_Angle += 1.0f;
            Vertical_Angle = Math.Clamp(Vertical_Angle, -89.0f, 89.0f); // Prevent flipping
        }

        public void DownCam()
        {
            Vertical_Angle -= 1.0f;
            Vertical_Angle = Math.Clamp(Vertical_Angle, -89.0f, 89.0f); // Prevent flipping
        }
        public void camUP()
        {
            CameraPlacment_Y += 2;
        }
        public void camReset()
        {


          CameraPlacment_X = 0.0f;

        EyeSight_X = 0.0f;


        Horizontal_Angle = 0.0f; // Yaw
        Vertical_Angle = 0.0f;   // Pitch

            CameraPlacment_Y = 20f;
            CameraPlacment_Z = 30f;

            EyeSight_Y = 8.0f;
            EyeSight_Z = 1.0f;


            CamRotate_X = 0.0f;
            CamRotate_Y = 1.0f; // Default up vector
            CamRotate_Z = 0.0f;


        }
        public void camDown()
        {
            CameraPlacment_Y -= 2;
        }
        public void ApplyMovment()
        {
            // Calculate EyeSight (camera direction vector)
            EyeSight_X = CameraPlacment_X + (float)(Math.Cos(DegreesToRadians(Vertical_Angle))
                                                    * Math.Sin(DegreesToRadians(Horizontal_Angle)));

            EyeSight_Y = CameraPlacment_Y + (float)Math.Sin(DegreesToRadians(Vertical_Angle));

            EyeSight_Z = CameraPlacment_Z - (float)(Math.Cos(DegreesToRadians(Vertical_Angle))
                                                    * Math.Cos(DegreesToRadians(Horizontal_Angle)));

            // Keep the Up Vector simple for now
            float upX = 0.0f;
            float upY = 1.0f; // Y-axis is "up"
            float upZ = 0.0f;

            // Debugging
            //Console.WriteLine("-------------------------------------------------------------");
            //Console.WriteLine($"Camera Position:    X: {CameraPlacment_X}, Y: {CameraPlacment_Y}, Z: {CameraPlacment_Z}");
            //Console.WriteLine($"Eye Direction:      X: {EyeSight_X}, Y: {EyeSight_Y}, Z: {EyeSight_Z}");
            //Console.WriteLine($"Camera Rotation:    Yaw: {Horizontal_Angle}, Pitch: {Vertical_Angle}");
            //Console.WriteLine("-------------------------------------------------------------");

            // Apply the camera transformation
            GLU.gluLookAt(
                CameraPlacment_X, CameraPlacment_Y, CameraPlacment_Z, // Camera position
                EyeSight_X, EyeSight_Y, EyeSight_Z,                 // Where the camera looks
                upX, upY, upZ                                       // "Up" direction
            );
        }

        private float DegreesToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }
    }
}
