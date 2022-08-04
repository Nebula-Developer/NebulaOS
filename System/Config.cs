using System.Collections.Generic;
using System;

using NebulaOS.Files;

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
            return Drives.First(x => x.Name == DefaultDrive);
        }
    }
}