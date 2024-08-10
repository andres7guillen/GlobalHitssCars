using Common.Logging.Implementations;
using Common.Logging.Interfaces;

namespace Common.Logging.Factory
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger(Type type)
        {
            return new Log4NetLogger(type);
        }
    }
}
