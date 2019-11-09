using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using System.Web;

namespace Computer_Store
{
    public class Logger
    {
        private static readonly ILog l = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog log
        {
            get { return l; }
        }

        public static void initLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}