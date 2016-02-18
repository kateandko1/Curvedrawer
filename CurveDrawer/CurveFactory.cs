using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class CurveFactory
    {
        public ICurve CreateCurve(string Name);
        public ICurve DeserializeFromString(string Str);
    }
}
