using log4net;

namespace Subvault_Domain {

    public static class Logger {

        public static readonly ILog Log = LogManager.GetLogger(typeof(Logger));
        public static string Format = "{0}: ";
    }
}