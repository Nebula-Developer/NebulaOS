using System;
using System.Collections.Generic;
using System.Diagnostics;

using NebulaOS.Programs;
using NebulaOS.Graphics;
using NebulaOS.Files;
using NebulaOS.Files.NJSON;

namespace NebulaOS.Programs.SystemPrograms {
  public class Home : Program {
    public override void Start() {
      Console.Clear();
      Console.WriteLine("NebulaOS v0.1.0");
      Console.WriteLine("Press any key to continue...");

      Vector2i winScale = new Vector2i(75, 15);
      Window win = new Window(winScale.X, winScale.Y, "NebulaOS", new WindowTheme());

      win.LoadTheme(Paths.GetRootPath("sys/themes/Default.json"));
      win.Init();

      int boxSizes = 5;
      int boxCount = (int)Math.Floor((double)(winScale.X / (boxSizes * 2) - 1));
      int boxSpacing = 0;

      for (int i = 0; i < boxCount; i++) {
        win.DrawBox(new Vector2i(i * (boxSizes * 2) + (i * (boxSpacing + 1)), 0), boxSizes);
      }

      Console.WriteLine("NebulaOS v0.0.9");
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();

      Window win = new Window(Console.WindowWidth - 5, Console.WindowHeight - 5, "NebulaOS", new WindowTheme());

      win.LoadTheme(Paths.GetRootPath("sys/themes/Default.json"));
      win.Init();
      win.DrawHorizontalLine(-10, 1000, 7, new RGB(255, 255, 255), new RGB(0, 0, 0));
      Console.ReadKey();
    }
  }
}