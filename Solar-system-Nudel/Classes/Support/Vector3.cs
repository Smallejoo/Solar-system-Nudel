using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_Nudel.Classes.Support
{
    public class Vector3<WhatEva>
    {



        public WhatEva X { get; set; }
        public WhatEva Y { get; set; }
        public WhatEva Z { get; set; }

        public void Put(Vector3<WhatEva> copyit)
        {
            this.X = copyit.X;
            this.Y = copyit.Y;
            this.Z = copyit.Z;
        }
        public Vector3(WhatEva x, WhatEva y, WhatEva z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3()
         {

         }


        public override string ToString()
            {
                return $"({X}, {Y}, {Z})";
            }
        

    }
}
