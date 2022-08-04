using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using NebulaOS.Files;

namespace NebulaOS.NSystem {
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
        public static string Log(String sender, String message, LogType type) {
            String log = String.Format("[{0}] ({1}) {2}: {3}", sender, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message);
            File.AppendAllText(Paths.GetRootPath("System.log"), log + "\n");
            return log;
        }

        /// <summary>
        /// Get the function name of the caller.
        /// </summary>
        /// <param name="frame">The frame to get the function name from.</param>
        public static String GetFunctionName(int frame = 1) {
            MethodBase? method = new StackTrace().GetFrame(frame)?.GetMethod();
            if (method == null || method.DeclaringType == null) return "NebulaOS.Unknown";
            return method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary>
        /// JSON error handler
        /// </summary>
        /// <param name="sender">The function that sent the error.</param>
        /// <param name="error">The error that occured.</param>
        public static String LogError(string error) {
            return Log(GetFunctionName(2), error, LogType.Error);
        }

        /// <summary>
        /// JSON info handler
        /// </summary>
        /// <param name="sender">The function that sent the info.</param>
        /// <param name="info">The info that occured.</param>
        public static String LogInfo(string info) {
            return Log(GetFunctionName(2), info, Logging.LogType.Info);
        }

        /// <summary>
        /// JSON warning handler
        /// </summary>
        /// <param name="sender">The function that sent the warning.</param>
        /// <param name="warning">The warning that occured.</param>
        public static String LogWarning(string warning) {
            return Log(GetFunctionName(2), warning, Logging.LogType.Warning);
        }
    }
}