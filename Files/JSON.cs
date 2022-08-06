using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System;
using System.Reflection;

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

        /// <summary>
        /// Check if a JSON dynamic contains unknown values torwards a class.
        /// </summary>
        /// <param name="json">The JSON dynamic to check.</param>
        /// <param name="type">The class to check for.</param>
        /// <returns>List of Tuple[Name, Value] (String, string)</returns>
        public static List<Tuple<String, String>> GetUnknownValues(dynamic json, Type type) {
            List<Tuple<String, String>> unknownValues = new List<Tuple<String, String>>();
            foreach (var item in json) {
                if (item.Value.GetType() == typeof(JArray)) {
                    var genericZero = type.GetField(item.Name).FieldType.GetGenericArguments()[0];
                    for (int i = 0; i < item.Value.Count; i++) {
                        if (item.Value[i].GetType() == typeof(JObject)) {
                            try {
                                unknownValues.AddRange(GetUnknownValues(item.Value[i], genericZero));
                            } catch {
                                unknownValues.Add(new Tuple<String, String>(item.Name, item.Value[i].ToString()));
                            }
                        }
                    }
                } else {
                    if (type.GetProperty(item.Name) == null && type.GetField(item.Name) == null) {
                        unknownValues.Add(new Tuple<String, String>(item.Name, item.Value.ToString()));
                    }
                }
            }
            return unknownValues;
        }

        /// <summary>
        /// Find undefined values in a JSON dynamic to a class.
        /// </summary>
        /// <param name="json">The JSON dynamic to check.</param>
        /// <param name="type">The class to check for.</param>
        /// <returns>List of values that are undefined in the JSON dynamic.</returns>
        public static List<String> FindUndefinedValues(dynamic json, Type type) {
            List<String> undefinedValues = new List<String>();
            foreach (var field in type.GetFields()) {
                if (field.FieldType.IsGenericType) {
                    if (json[field.Name] == null) {
                        undefinedValues.Add(field.Name);
                    } else {
                        for (int i = 0; i < json[field.Name].Count; i++) {
                            if (json[field.Name][i].GetType() == typeof(JObject)) {
                                List<String> undefinedValsArr = FindUndefinedValues(json[field.Name][i], field.FieldType.GetGenericArguments()[0]);
                                for (int j = 0; j < undefinedValsArr.Count; j++) {
                                    undefinedValues.Add(field.Name + "." + undefinedValsArr[j]);
                                }
                            }
                        }
                    }
                } else {
                    if (json[field.Name] == null) {
                        undefinedValues.Add(field.Name);
                    }
                }
            }

            foreach (var property in type.GetProperties()) {
                if (property.PropertyType.IsArray) {
                    if (json[property.Name] == null) {
                        undefinedValues.Add(property.Name);
                    } else {
                        for (int i = 0; i < json[property.Name].Count; i++) {
                            if (json[property.Name][i] == null) {
                                undefinedValues.Add(property.Name + "[" + i + "]");
                            }
                        }
                    }
                } else {
                    if (json[property.Name] == null) {
                        undefinedValues.Add(property.Name);
                    }
                }
            }
            return undefinedValues;
        }
    }
}

namespace System.Text {
    // Make (string).ToUpLow()
    public static class StringExtensions {
        public static String ToUpLow(this String str) {
            return str.ToLowerInvariant();
        }
    }
}