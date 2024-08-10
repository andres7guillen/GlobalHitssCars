using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging.Interfaces
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception ex);
        // Otros métodos de logging según sea necesario
    }
}
