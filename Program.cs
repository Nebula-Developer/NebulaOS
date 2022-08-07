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

namespace NebulaOS {
    public class Root {
        public static RootConfig Config = new RootConfig();

        public static int Main(String[] args) {
            Console.WriteLine("NebulaOS v0.0.6");
            Config = Deps.CreateSystem();

            RGB bootStartCol = new RGB(250, 122, 100);
            RGB bootEndCol = new RGB(237, 199, 142);

            int bootHeight = Console.BufferHeight - 1;
            Console.Clear();

            List<RGB> gradient = bootStartCol.ToGradient(bootEndCol, bootHeight);
            for (int i = 0; i < gradient.Count(); i++) {
                Console.SetCursorPosition(0, i);
                Console.Write(gradient[i].ToBGStr() + new String(' ', Console.BufferWidth));
            }

            String bootText = "Welcome";
            Console.SetCursorPosition((Console.BufferWidth / 2) - (bootText.Length / 2), Console.BufferHeight / 2);
            Console.Write(Color.CombineFB(new RGB(0, 0, 0), gradient[gradient.Count() / 2]) + bootText);

            Console.ReadKey(true);
            
            Logging.LogInfo("Using drive: " + Config.GetDefaultDrive().Name);
            Logging.LogInfo("Creating user and system dependencies...");
            Deps.CreateDeps();

            Logging.LogInfo("Done");
            Logging.Log("NebulaOS.Booting", "Booting NebulaOS...", Logging.LogType.System);
            return 0;
        }
    }
}