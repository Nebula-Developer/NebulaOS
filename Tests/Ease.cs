using NebulaOS.Tests;
using System;
using System.Collections.Generic;

using NebulaOS.Maths;
using NebulaOS.Graphics;

namespace NebulaOS.Tests.TestClasses {
  public class EaseTest : Test {
    public override void Init()
    {
      Curve c = new Curve(0, Console.BufferWidth - 1);
      String testEaseTest = "R ->";

      c.EaseOverMS(2000, Curve.Ease.EaseInOut, (x) => {
        Console.SetCursorPosition((int)x > Console.BufferWidth - 1 ? Console.BufferWidth - 1 : (int)x, Console.BufferHeight - 1);
        Console.WriteLine('%');

        Console.SetCursorPosition((Console.BufferWidth / 2) - (testEaseTest.Length / 2), Console.BufferHeight - 2);
        Console.Write(new RGB(120, 255, 200).ToStr() + testEaseTest + RGB.Reset());
      });

      testEaseTest = "L <-";
      c = new Curve(Console.BufferWidth - 1, 0);

      c.EaseOverMS(2000, Curve.Ease.EaseInOut, (x) => {
        Console.SetCursorPosition((int)x < 0 ? 0 : (int)x, Console.BufferHeight - 1);
        Console.WriteLine('%');

        Console.SetCursorPosition((Console.BufferWidth / 2) - (testEaseTest.Length / 2), Console.BufferHeight - 2);
        Console.Write(new RGB(50, 255, 100).ToStr() + testEaseTest + RGB.Reset());
      });
    }

    public override void End()
    {
      base.End();
    }

    public override bool CallOnBoot() { return false; }
  }
}