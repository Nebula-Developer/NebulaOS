using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

using NebulaOS.Files;
using NebulaOS.Files.NJSON;
using NebulaOS.NSystem;
using NebulaOS.Graphics;
using NebulaOS.Graphics.Effects;
using NebulaOS.Maths;
using NebulaOS.NSystem.Generic;
using NebulaOS.Programs.SystemPrograms;

namespace NebulaOS {
    public class Root {
        public static RootConfig Config = new RootConfig();
        public static Info SystemInfo = new Info();

        public static int Main(String[] args) {
            Console.CursorVisible = false;
            Console.WriteLine("NebulaOS v0.0.9");

            Logging.LogInfo("Using drive: " + Config.GetDefaultDrive().Name);

            Logging.LogInfo("Loading system dependencies..");
            Deps.CreateSystemDeps();
            Logging.LogInfo("System variables loaded.");

            Logging.LogInfo("Creating user dependencies...");
            Deps.CreateUserDeps();

            Logging.LogInfo("Done");
            Logging.Log("NebulaOS.Booting", "Booting NebulaOS...", Logging.LogType.System);

            Console.Clear();
            Logging.LogInfo("Console cleared, initializing boot window...");

            Home home = new Home();
            home.Run();
            Console.Clear();
            return 0;
        }
    }
}