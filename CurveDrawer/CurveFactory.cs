using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CurveDrawer
{
    class InvalidCurve : ICurve
    {
        double[] m_params ;
        int      m_nparams;
        DOMAIN   m_dom    ;

        public InvalidCurve() { }

        public double[] Params
        {
            get { return  m_params; }
            set { m_params = value; }
        }

        public int nparams
        {
            get { return m_nparams; }
            set {                   }
        }

        public string Name
        {
            get { return ""; }
            set {            }
        }

        public string Type   { get { return ""   ; } }
        public DOMAIN Domain { get { return m_dom; } }

        public string Serialize()       { return ""  ; }
        public double FuncVal(double x) { return -1.0; }
    }

    class TestCurve : ICurve
    {
        double[] m_params;
        DOMAIN   m_dom   ;
        string   m_name  ;

        public double[] Params
        {
            get { return m_params; }
            set {                  }
        }

        public int nparams
        {
            get { return 1; }
            set {           }
        }

        public string Name
        {
            get { return "TestCurve" ; }
            set { m_name = value     ; }
        }

        public string Type   { get { return "TEST" ; } }
        public DOMAIN Domain { get { return m_dom  ; } }

        public string Serialize()       { return "TEST_SERIALIZED"; }
        public double FuncVal(double x) { return x*x + x          ; }
    }

    class CurveFactory
    {
        public ICurve CreateCurve(string Type)
        {
            if (Type == "TEST") return new TestCurve   ();
                                return new InvalidCurve();
        }

        public ICurve DeserializeFromString(string Str)
        {
            if (Str == "TEST_SERIALIZED") return new TestCurve   ();
                                          return new InvalidCurve();
        }
    }
}
