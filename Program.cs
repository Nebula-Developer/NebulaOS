using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

using NebulaOS.Files;
using NebulaOS.Files.NJSON;
using NebulaOS.NSystem;

namespace NebulaOS {
    public class Root {
        public static RootConfig Config = new RootConfig();

        public static int Main(String[] args) {
            Console.WriteLine("NebulaOS v0.0.3");
            Config = Deps.CreateSystem();
            
            Logging.LogInfo("Using drive: " + Config.GetDefaultDrive().Name);
            Logging.LogInfo("Creating user and system dependencies...");
            Deps.CreateDeps();

            Logging.LogInfo("Done");
            Logging.Log("NebulaOS.Booting", "Booting NebulaOS...", Logging.LogType.System);
            return 0;
        }
    }
}