namespace UnityTool.Libgame
{
    public class Log
    {
        static log4net.ILog logger;
        public static log4net.ILog GetInstance()
        {
            if (logger == null)
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
                logger = log4net.LogManager.GetLogger(typeof(Log));
            }
            return logger;
        }
        public static void WriteLog(string msg)
        {
            GetInstance().Info(msg);
        }
    }
}
