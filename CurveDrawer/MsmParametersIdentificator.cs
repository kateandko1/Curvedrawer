using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class InvalidParametersIdentificator : IParametersIdentificator
    {
        public InvalidParametersIdentificator()
        {

        }

        public void ProcessCurve(ICurve Curve, Point[] Points, IFunctional Functional, ILogger Logger)
        {

        }
    }

    class MsmParametersIdentificator : IParametersIdentificator
    {
        int OnethreadThresholdForPhase1;
        int OnethreadThresholdForPhase2;

        public MsmParametersIdentificator()
        {
            OnethreadThresholdForPhase1 = 500;
            OnethreadThresholdForPhase2 = 200;
        }

        public void ProcessCurve(ICurve Curve, Point[] Points, IFunctional Functional, ILogger Logger)
        {

        }
    }
}
