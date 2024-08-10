
using log4net.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class Log4NetConfig
    {
        public static void Configure()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            if (!configFile.Exists)
            {
                Console.WriteLine($"Archivo de configuración no encontrado: {configFile.FullName}");
            }
            else
            {
                Console.WriteLine($"Cargando configuración desde: {configFile.FullName}");
            }

            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, configFile);
        }
    }
}
