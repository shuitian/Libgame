using UnityEngine;

namespace Libgame
{
    public class Log
    {
        static log4net.ILog logger;
        public static log4net.ILog GetInstance()
        {
            if (logger == null)
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(UnityEngine.Application.streamingAssetsPath + "/log4net.config"));
                logger = log4net.LogManager.GetLogger(typeof(Log));
            }
            return logger;
        }
        public static void WriteLog(string type, string msg)
        {
            if (type == "Debug")
            {
                GetInstance().Debug(msg);
            }
            else if (type == "Warn")
            {
                GetInstance().Warn(msg);
            }
            else if (type == "Error")
            {
                GetInstance().Error(msg);
            }
            else if (type == "Fatal")
            {
                GetInstance().Fatal(msg);
            }
            else
            {
                GetInstance().Info(msg);
            }
        }

        public static void WriteLog(string msg)
        {
            WriteLog("Info", msg);
        }

    }
}
