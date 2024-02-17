using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psimos.Transform.SpecialClasses
{
    public struct Vector2D
    {
        public float x, y;
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2D Normalize()
        {
            if (x == 0 && y == 0) return this;
            float r = (float)Math.Sqrt(x * x + y * y);
            return new Vector2D(x / r, y / r);
        }
        public static Vector2D Zero { get { return new Vector2D(0, 0); } }

    }
}
