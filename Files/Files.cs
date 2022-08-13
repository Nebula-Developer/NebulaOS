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

        /// <summary>
        /// Create a file and write a string to it, only if it doesn't exist.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="content">The content to write.</param>
        public static void CreateNonExisting(String path, String data = "") {
            if (!File.Exists(path)) {
                File.WriteAllText(path, data);
            }
        }

        /// <summary>
        /// Create a directory, only if it doesn't exist.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public static void CreateNonExistingDir(String path) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Create a directory with spesified files and directories, only if it doesn't exist.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <param name="files">The files to create.</param>
        /// <param name="directories">The directories to create.</param>
        public static void CreateNonExistingDir(String path, List<String> files, List<String> directories) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
                foreach (String file in files)
                    File.Create(Path.Combine(path, file));
                foreach (String dir in directories)
                    Directory.CreateDirectory(Path.Combine(path, dir));
            }
        }
    }
}