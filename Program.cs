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

namespace NebulaOS {
    public class Root {
        public static RootConfig Config = new RootConfig();

        public static int Main(String[] args) {
            Console.Clear();
            Console.WriteLine("NebulaOS v0.0.7");

            List<Tuple<int, int, char>>? GraphicTest = Graphic.ReadGraphicFile("C:\\Users\\nebul\\Desktop\\NebulaOS\\testgraphic.graphic");
            if (GraphicTest == null) {
                Console.WriteLine("Graphic test failed");
                return 1;
            }
            
            Logging.LogInfo("Using drive: " + Config.GetDefaultDrive().Name);
            Logging.LogInfo("Creating user and system dependencies...");
            Deps.CreateDeps();

            Logging.LogInfo("Done");
            Logging.Log("NebulaOS.Booting", "Booting NebulaOS...", Logging.LogType.System);

            Console.Clear();
            Window win = new Window(Console.WindowWidth - 5, Console.WindowHeight - 5, "NebulaOS", new WindowTheme());
            win.Init();
            Console.ReadKey();
            Console.Clear();
            return 0;
        }
    }
}