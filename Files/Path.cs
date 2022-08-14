using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Collections.Generic;

namespace NebulaOS.Files {
    public class Paths {
        /// <summary>
        /// Combine a path with the program's root directory.
        /// </summary>
        /// <param name="path">The path to combine with the root directory.</param>
        public static string GetRootPath(String path) {
            return Path.Combine(AppContext.BaseDirectory, path);
        }

        /// <summary>
        /// Combine a path with the program's main drive directory.
        /// </summary>
        /// <param name="path">The path to combine with the main drive directory.</param>
        public static string GetDrivePath(String path) {
            return Path.Combine(Root.Config.GetDefaultDrivePath(), path);
        }
    }
}