using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class ParametersIdentificatorFactory
    {
        public IParametersIdentificator CreateParametersIdentificator(string Type)
        {
            if (Type == "MSM")
            {
                return new MsmParametersIdentificator();
            }
        }
    }
}
