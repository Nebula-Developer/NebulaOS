using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System;

using NebulaOS.Files;

namespace NebulaOS.System {
    public class Logging {
        public enum LogType {
            Info, // Information
            Warning, // Warning - Should fix
            Error, // Error - Just below fatal
            Fatal, // Fatal - System is about to crash (Serious)
            Debug // Debug - From system development
        }

        /// <summary>
        /// Log a console information message to the system log file.
        /// </summary>
        /// <param name="sender">The message to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="area">The area of the system the message is from.</param>
        /// <param name="type">The info/error level of the message.</param>
        public static void Log(String sender, String message, LogType type) {
            File.WriteAllText(Paths.GetRootPath("System.log"), String.Format("[{0}] ({1}) {2}: {3}\n", sender, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message));
        }
    }
}