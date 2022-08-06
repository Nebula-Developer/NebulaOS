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
            RootConfig? readConfig = JSON.ParseFile<RootConfig>(Paths.GetRootPath("config.json"));
            dynamic? readCheck = JSON.ParseFile(Paths.GetRootPath("config.json"));
            
            if (readConfig == null) {
                Console.WriteLine("Fixing null config...");
                readConfig = new RootConfig();
                JSON.Serialize(readConfig, true);
            }

            foreach (Tuple<String, String> item in JSON.GetUnknownValues(readCheck, typeof(RootConfig))) { Logging.LogWarning("Unknown config value: '" + item.Item1 + "' in 'root/config.json'"); }
            foreach (String s in JSON.FindUndefinedValues(readCheck, typeof(RootConfig))) { Logging.LogError("Undefined or incorrect type value: '" + s + "' in 'root/config.json'"); }
            Config = readConfig;

            Console.WriteLine("Config loaded.");
            Console.WriteLine("Using drive: {0}", Config.GetDefaultDrive().Name);
            return 0;
        }
    }
}