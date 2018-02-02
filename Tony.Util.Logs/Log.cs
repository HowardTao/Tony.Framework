using log4net;

namespace Tony.Util.Logs
{
    /// <summary>
    /// 日志
    /// </summary>
   public  class Log
    {
        private readonly ILog _log;

        public Log(ILog log)
        {
            _log = log;
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }
        public void Error(object message)
        {
            _log.Error(message);
        }
        public void Info(object message)
        {
            _log.Info(message);
        }
        public void Warn(object message)
        {
            _log.Warn(message);
        }
    }
}
