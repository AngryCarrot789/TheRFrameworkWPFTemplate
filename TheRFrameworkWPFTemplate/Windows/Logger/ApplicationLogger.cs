using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Windows.Logger
{
    /// <summary>
    /// Used as a simple application-wide logger. If you need multiple loggers,
    /// you could make this non-static and carry instances around. Though tbh for
    /// that i'd probably just carry around the LoggerViewModel for simplicity.
    /// </summary>
    public static class ApplicationLogger
    {
        public delegate void LogEventArgs(DateTime date, string header, string description);
        public static event LogEventArgs LogInformation;

        public static void Log(string head, string description)
        {
            LogInformation?.Invoke(DateTime.Now, head, description);
        }

        public static void Log(string description)
        {
            LogInformation?.Invoke(DateTime.Now, "General Error", description);
        }
    }
}
