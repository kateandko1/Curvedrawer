using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer.ParameterizedCurves
{
    class PolynomFunctions : ICurve
    {
        double[] m_params ;
        int      m_nparams;
        string   m_name   ;
        DOMAIN   m_domain ;

        public double[] Params
        {
            set
            {
                if (value.Length == 2) { m_params = value; }
            }

            get { return m_params; }
        }

        public double FuncVal(double x)
        {
            double functionValue = 0;
            double temp_value;

            for (int i = 0; i < m_nparams; ++i)
            {
                temp_value = m_params[i];
                for (int j = 0; j < m_nparams - (i + 1); ++i)
                {
                    temp_value *= x;
                }
                functionValue += temp_value;
            }

            return functionValue;
        }
       
        public int nparams
        {
            get { return m_nparams ; }
            set { m_nparams = value; }
        }

       public string Serialize()
        {
            string OutStr;
            OutStr = m_name    + " ";
            OutStr+= m_nparams + " ";

            for (int i = 0; i < m_nparams; ++i)
            {
                OutStr += m_params[i] + " ";
            }

            return OutStr;
        }

        public string Name
        {
            get { return m_name ; }
            set { m_name = value; }
        }

        public DOMAIN Domain
        {
            get { return m_domain; }
        }
    }
}
