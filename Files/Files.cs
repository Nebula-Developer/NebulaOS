using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System;

using NebulaOS.NSystem;

namespace NebulaOS.Files {
    public static class OSFile {
        /// <summary>
        /// Write a string to a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="content">The content to write.</param>
        public static void Write(String path, String data) {
            File.WriteAllText(path, data);
        }

        /// <summary>
        /// Read a string from a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The content of the file.</returns>
        public static String Read(String path) {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Check if a file exists.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>True if the file exists, false otherwise.</returns>
        public static bool Exists(String path) {
            return File.Exists(path);
        }

        /// <summary>
        /// Delete a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void Delete(String path) {
            File.Delete(path);
        }
    }
}