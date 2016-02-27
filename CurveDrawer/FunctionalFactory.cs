using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class InvalidFunctional : IFunctional
    {
        public InvalidFunctional()
        {

        }

        public double Distance(ICurve curve)
        {
            return -1.0;
        }

        public Point[] Points
        {
            get { return new Point[0]; }
            set { }
        }
    }
    class FunctionalFactory
    {
        IFunctional CreateFunctional()
        {
            return new InvalidFunctional();
        }
    }
}
