using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Bson;

using NebulaOS.Files;

// Make [Required] an alias for [JsonProperty(Required = Required.Always)]
using Required = Newtonsoft.Json.Required;

namespace NebulaOS.NSystem {
    public class Drive {
        public String Name;
        public DriveType Type;

        public enum DriveType {
            Root,
            Sub,
            Debug
        };

        public Drive (String name, DriveType type) {
            Name = name;
            Type = type;
        }
    }

    public class RootConfig {
        public List<Drive> Drives = new List<Drive>() {
            new Drive("OS", Drive.DriveType.Root)
        };

        public String DefaultDrive = "OS", DrivePath = "Undefined";

        public Drive GetDefaultDrive() {
            return Drives.First(x => x.Name == DefaultDrive && x.Type == Drive.DriveType.Root);
        }
    }
}