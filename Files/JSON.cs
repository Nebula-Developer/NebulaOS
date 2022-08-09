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

            return unknownValues;
        }

        public class TypeItem {
            public String Name;
            public Type Type;
            public bool IsArray() { return Type.IsArray; }
            public bool IsClass() { return Type.IsClass; }
            public bool IsGenericType() { return Type.IsGenericType; }
            public bool IsSystemClass() { return Type.Namespace?.StartsWith("System") ?? false; }
            public Type? GetElemType() { return Type.GetElementType(); }
            public Type? GetGenericArgument() { return Type.GetGenericArguments()[0]; }
            
            public TypeItem(String name, Type type) {
                Name = name;
                Type = type;
            }
        }

        /// <summary>
        /// Find undefined values in a JSON dynamic to a class.
        /// </summary>
        /// <param name="json">The JSON dynamic to check.</param>
        /// <param name="type">The class to check for.</param>
        /// <returns>List of values that are undefined in the JSON dynamic.</returns>
        public static List<String> GetUndefinedValues(dynamic json, Type type) {
            List<String> undefinedValues = new List<String>();

            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            List<TypeItem> items = new List<TypeItem>();
            items.AddRange(fields.Select(f => new TypeItem(f.Name, f.FieldType)));
            items.AddRange(properties.Select(p => new TypeItem(p.Name, p.PropertyType)));

            foreach (TypeItem item in items) {
                if (json[item.Name] == null)
                    undefinedValues.Add(item.Name);

                else if (item.IsGenericType()) {
                    Type? elementType = item.GetGenericArgument();
                    if (elementType != null)
                        foreach (dynamic element in json[item.Name])
                            undefinedValues.AddRange(GetUndefinedValues(element, elementType));
                }

                else if (item.IsArray()) {
                    Type? elementType = item.GetElemType();
                    if (elementType != null)
                        foreach (dynamic element in json[item.Name])
                            undefinedValues.AddRange(GetUndefinedValues(element, elementType));
                }

                else if (item.IsClass() && !item.IsSystemClass()) {
                    undefinedValues.AddRange(GetUndefinedValues(json[item.Name], item.Type));
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