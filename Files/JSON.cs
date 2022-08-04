using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System;

using NebulaOS.System;

namespace NebulaOS.Files.JSON {
    public class JSON {
        #region Logging
        /// <summary>
        /// JSON error handler
        /// </summary>
        /// <param name="sender">The function that sent the error.</param>
        /// <param name="error">The error that occured.</param>
        public static void LogError(String sender, string error) {
            Logging.Log(sender, error, Logging.LogType.Error);
        }

        /// <summary>
        /// JSON info handler
        /// </summary>
        /// <param name="sender">The function that sent the info.</param>
        /// <param name="info">The info that occured.</param>
        public static void LogInfo(String sender, string info) {
            Logging.Log(sender, info, Logging.LogType.Info);
        }

        /// <summary>
        /// JSON warning handler
        /// </summary>
        /// <param name="sender">The function that sent the warning.</param>
        /// <param name="warning">The warning that occured.</param>
        public static void LogWarning(String sender, string warning) {
            Logging.Log(sender, warning, Logging.LogType.Warning);
        }
        #endregion

        /// <summary>
        /// Parse a file as JSON.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static dynamic? ParseFile(String path) {
            try {
                String json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject(json);
            } catch (Exception e) {
                LogError("NebulaOS.Files.JSON.ParseFile", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Parse a file as JSON.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static T? ParseFile<T>(String path) where T : class {
            try {
                String json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            } catch (Exception e) {
                LogError("NebulaOS.Files.JSON.ParseFile", e.Message);
                return null;
            }
        }
    }
}