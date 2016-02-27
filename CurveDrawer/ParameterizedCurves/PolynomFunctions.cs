using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer.ParameterizedCurves
{
    class PolynomFunctions : ICurve
    {
        double[] _params;
        int _nparams;
        string _name;
        DOMAIN _domain;
        public double[] Params
        {
            set
            {
                if (value.Length == 2)
                {
                    _params = value;
                }
            }
            get
            {
                return _params;
            }
        }
        public double FuncVal(double x)
        {
            double functionValue = 0;
            double temp_value;
            for (int i = 0; i < _nparams; i++)
            {
                temp_value = _params[i];
                for (int j = 0; j < _nparams - (i + 1);i++ )
                {
                    temp_value *= x;
                }
                functionValue += temp_value;
            }
            return functionValue;
        }
       
        public int nparams
        {
            get
            {
                return _nparams;
            }
            set
            {
                _nparams = value;
            }
        }
       public string Serialize()
        {
            string OutStr;

            OutStr = _name+" ";

            OutStr += _nparams + " ";
            for (int i = 0; i < _nparams;i++ )
            {
                OutStr += _params[i] + " ";
            }

            return OutStr;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public DOMAIN Domain
        {
            get
            {
                return _domain;
            }
        }
    }
}
