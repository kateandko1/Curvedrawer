﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    enum DOMAIN_TYPE
    {
        DOMAIN_POS_RAY = 1,
        DOMAIN_NEG_RAY,
        INFINITY,
        SEGMENT

    }

    struct DOMAIN
    {
        string Text;
        double x1;
        double x2;
        DOMAIN_TYPE Type;
    }

    interface ICurve
    {
        string Serialize();
        double FuncVal(double x);

        double[] Params { get; set; }
        string[] ParamsNames { get; set; }
        int nparams { get; set; }

        string Name { get; set; }
        string Type { get; }
        DOMAIN Domain { get; }
    }
    
    interface ICurveRepresentation
    {
        double XMin { get; }
        double YMin { get; }
        double XMax { get; }
        double YMax { get; }
    }

    interface ILogger
    {
        int Progress { get; set; }
        string Message {get; set;}
    }

    interface IParametersIdentificator
    {
        void ProcessCurve(ICurve Curve, Point[] Points, IFunctional Functional, ILogger Logger);
    }

    struct Point
    {
        double x;
        double y;
    }

    interface IFunctional
    {
        Point[] Points { get; set; }
        double Distance(ICurve Curve);
    }

}
