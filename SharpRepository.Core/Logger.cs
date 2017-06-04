using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDev.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class Logger : Attribute
    {
        private readonly ILog _logger;
        private LogLevel _logLevel = LogLevel.Debug;

        public Logger()
        {
            _logger = LogManager.GetLogger("Sharp");
        }

        public LogLevel LogLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }

        private void Log(string message)
        {
            switch (_logLevel)
            {
                case LogLevel.Error:
                    _logger.Error(message);
                    break;
                case LogLevel.Info:
                    _logger.Info(message);
                    break;
                case LogLevel.Trace:
                    _logger.Trace(message);
                    break;
                default:
                    _logger.Debug(message);
                    break;
            }
        }

        public virtual void OnError<T>(object context, Exception ex) where T : class
        {
            Log(String.Format("exception entity :  {0}     message : {1}     stack : {2}", typeof(T).Name, ex.Message, ex.StackTrace));

        }
    }
}
