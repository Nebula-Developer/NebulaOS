using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System;

using NebulaOS.NSystem;

namespace NebulaOS.Files.NJSON {
    public class JSON {
        /// <summary>
        /// Parse a file as JSON.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The JSON object.</returns>
        public static dynamic? ParseFile(String path) {
            try {
                String json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject(json);
            } catch (Exception e) {
                Logging.LogError(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Parse a file as JSON.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The JSON object.</returns>
        public static T? ParseFile<T>(String path) where T : class {
            try {
                String json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            } catch (Exception e) {
                Logging.LogError(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Serialize a dynamic to JSON.
        /// </summary>
        /// <param name="json">The dynamic to serialize.</param>
        /// <param name="indent">Whether to indent the JSON.</param>
        /// <returns>The serialized JSON.</returns>
        public static String Serialize(dynamic json, bool indent = true) {
            return JsonConvert.SerializeObject(json, indent ? Formatting.Indented : Formatting.None);
        }
    }
}