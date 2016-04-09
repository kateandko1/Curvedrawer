using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveDrawer
{
    class InvalidLogger : ILogger
    {
        public InvalidLogger() { }

        public int Progress
        {
            get { return 0; }
            set {           }
        }

        public string Message
        {
            get { return ""; }
            set {            }
        }
    }

    class LoggerFactory
    {
        ILogger CreateLogger() { return new InvalidLogger(); }
    }
}
