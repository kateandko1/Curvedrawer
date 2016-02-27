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
        private double[] _params;
        private int _nparams;
        DOMAIN _dom;

        public InvalidCurve()
        {

        }

        public double[] Params
        {
            get { return _params; }
            set { _params = value; }
        }

        public int nparams
        {
            get { return _nparams; }
            set { }
        }

        public string Name
        {
            get { return ""; }
            set { }
        }

        public string Type
        {
            get { return ""; }
        }
        public DOMAIN Domain
        {
            get { return _dom; }
        }

        public string Serialize() { return ""; }
        public double FuncVal(double x) { return -1.0; }
    }

    class TestCurve : ICurve
    {
        double[] _params;
        DOMAIN _dom;
        string _name;
        public double[] Params
        {
            get { return _params; }
            set { }
        }

        public int nparams
        {
            get { return 1; }
            set { }
        }

        public string Name
        {
            get { return "TestCurve"; }
            set { _name = value; }
        }

        public string Type
        {
            get { return "TEST"; }
        }

        public DOMAIN Domain
        {
            get { return _dom; }
        }

        public string Serialize() { return "TEST_SERIALIZED"; }
        public double FuncVal(double x) { return x*x + x; }
    }

    class CurveFactory
    {
        public ICurve CreateCurve(string Type)
        {
            if (Type == "TEST")
                return new TestCurve();
            else
                return new InvalidCurve();
        }

        public ICurve DeserializeFromString(string Str)
        {
            if (Str == "TEST_SERIALIZED")
                return new TestCurve();
            return new InvalidCurve();
        }
    }
}