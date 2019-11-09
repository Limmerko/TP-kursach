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
        private static readonly ILog log = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}