using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class InvalidParametersIdentificator : IParametersIdentificator
    {
        public InvalidParametersIdentificator() { }

        public void ProcessCurve(ICurve Curve, Point[] Points, IFunctional Functional, ILogger Logger) { }
    }

    class MsmParametersIdentificator : IParametersIdentificator
    {
        public MsmParametersIdentificator()
        {
            m_OnethreadThresholdForPhase1 = 500;
            m_OnethreadThresholdForPhase2 = 200;
        }

        public void ProcessCurve(ICurve Curve, Point[] Points, IFunctional Functional, ILogger Logger) { }

        int m_OnethreadThresholdForPhase1;
        int m_OnethreadThresholdForPhase2;
    }
}
