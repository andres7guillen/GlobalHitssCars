using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging.Interfaces;
using log4net;
using log4net.Core;
using log4net.Repository;

namespace Common.Logging.Implementations
{
    public class Log4NetLogger : Interfaces.ILogger
    {
        private readonly ILog _logger;

        public string Name => throw new NotImplementedException();

        public ILoggerRepository Repository => throw new NotImplementedException();

        public Log4NetLogger(Type type)
        {
            _logger = LogManager.GetLogger(type);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger.Error($"{message} : {ex.StackTrace}", ex);
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        // Otros métodos de logging según sea necesario
    }
}
