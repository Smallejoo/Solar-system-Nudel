using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes
{

    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        // Constructor with initial values
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Default constructor
        public Vector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        // Method to copy values from another Vector3
        public void Put(Vector3 copyit)
        {
            this.X = copyit.X;
            this.Y = copyit.Y;
            this.Z = copyit.Z;
        }

        // Method to set values directly
        public void Put(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Subtract another Vector3 from this Vector3
        public void Sub(Vector3 secondVector)
        {
            this.X -= secondVector.X;
            this.Y -= secondVector.Y;
            this.Z -= secondVector.Z;
        }
        public Vector3 SubReturn(Vector3 secondVector)
        {
            Vector3 Result= new Vector3();
            Result.X = this.X-secondVector.X;
            Result.Y = this.Y-secondVector.Y;
            Result.Z = this.Z-secondVector.Z;
            return Result;

        }

        // Normalize the vector
        public Vector3 Normalize()
        {
            // Calculate the magnitude
            float magnitude = (float)Math.Sqrt(X * X + Y * Y + Z * Z);

            // Avoid division by zero
            if (magnitude == 0)
            {
                return new Vector3(0, 0, 0);
            }

            // Divide each component by the magnitude
            float nx = X / magnitude;
            float ny = Y / magnitude;
            float nz = Z / magnitude;

            // Return a new normalized vector
            return new Vector3(nx, ny, nz);
        }

        // Override ToString for easier debugging
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
