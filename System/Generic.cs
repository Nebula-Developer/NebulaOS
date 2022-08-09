using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace NebulaOS.NSystem.Generic {
    public class OS {
        public static void Sleep(float ms) { new ManualResetEvent(false).WaitOne((int)ms); }
        public static void StSleep(float ms) { Stopwatch sw = new Stopwatch(); sw.Start(); while (sw.ElapsedMilliseconds < ms) { Thread.Sleep(1); }}
        public static void ThSleep(float ms) { Thread.Sleep((int)ms); }
    }
}