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
            Console.WriteLine("NebulaOS v0.0.3");
            Config = Deps.CreateSystem();

            RGB rgbColor = new RGB(50, 100, 255);
            Console.Clear();
            float percent = 0;
            for (int i = 0; i <= Console.BufferHeight - 1; i++) {
                Console.SetCursorPosition(0, i);
                percent += 2.5f;

                
                Console.Write(rgbColor.FadeTo(new RGB(155, 0, 0), percent).ToBGStr() + new String(' ', Console.BufferWidth - 1) + RGB.Reset());

                if (i == Console.BufferHeight / 2) {
                    String welcomeStr = "Test";
                    if (Console.BufferWidth > welcomeStr.Length) {
                        Console.SetCursorPosition((Console.BufferWidth / 2) - (welcomeStr.Length / 2), Console.BufferHeight / 2);
                        Console.Write(rgbColor.FadeTo(new RGB(255, 0, 0), percent).ToBGStr() + welcomeStr + RGB.Reset());
                    }
                }
            }

            Console.ReadKey(true);
            Console.Clear();

            RGB nColor = new RGB(255, 0, 0);
            for (int i = 0; i <= 100; i += 10) { Console.Write(nColor.FadeTo(new RGB(255, 255, 255), i).ToBGStr() + i.ToString() + RGB.Reset() + ", "); }
            nColor.Set(0, 255, 0);
            Console.Write("\n");
            for (int i = 0; i <= 100; i += 10) { Console.Write(nColor.FadeTo(new RGB(255, 255, 255), i).ToBGStr() + i.ToString() + RGB.Reset() + ", "); }
            nColor.Set(0, 0, 255);
            Console.Write("\n");
            for (int i = 0; i <= 100; i += 10) { Console.Write(nColor.FadeTo(new RGB(255, 255, 255), i).ToBGStr() + i.ToString() + RGB.Reset() + ", "); }

            for (int r = 0; r < 255; r += 35) {
                for (int g = 0; g < 255; g += 35) {
                    for (int b = 0; b < 255; b += 35) {
                        for (int percentage = 0; percentage < 100; percentage += 50) {
                            Console.Write(new RGB(r, g, b).FadeTo(new RGB(255, 255, 255), percentage).ToBGStr() + " " + RGB.Reset());
                        }
                    }
                }
            }
            

            Logging.LogInfo("Using drive: " + Config.GetDefaultDrive().Name);
            Logging.LogInfo("Creating user and system dependencies...");
            Deps.CreateDeps();

            Logging.LogInfo("Done");
            Logging.Log("NebulaOS.Booting", "Booting NebulaOS...", Logging.LogType.System);
            return 0;
        }
    }
}