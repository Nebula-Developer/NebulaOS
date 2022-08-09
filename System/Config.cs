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
    public class Version {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Sub { get; set; }

        public Version(int major, int minor, int sub) {
            this.Major = major;
            this.Minor = minor;
            this.Sub = sub;
        }

        public Version(String version) {
            String[] split = version.Split('.');
            this.Major = int.Parse(split[0]);
            this.Minor = int.Parse(split[1]);
            this.Sub = int.Parse(split[2]);
        }

        public override string ToString() {
            return $"{this.Major}.{this.Minor}.{this.Sub}";
        }
    }

    public enum RelState {
        Release,
        Beta,
        Alpha,
        Debug
    }

    public class Info {
        public Version Version = new Version(0, 0, 0);
        public String Name = "Undefined";
        public String Author = "John Doe";
        public RelState State = RelState.Alpha;
    }

    public class DriveData {
        public string Data;
        public DriveData(string data) {
            Data = data;
        }
    }

    public class Drive {
        public String Name;
        public DriveType Type;
        public DriveData data = new DriveData("TestData");

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