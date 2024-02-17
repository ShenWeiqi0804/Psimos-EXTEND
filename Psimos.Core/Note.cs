using Psimos.Transform.SpecialClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psimos.Core
{
    public abstract class Note : Psimos.Core.SpecialClasses.Component
    {
        public float checkTime;
        public Transform.SpecialClasses.Transform defaultTransform;
        public string texturePath;
    }
}
