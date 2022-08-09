using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace NebulaOS.Graphics.Effects.Wave {
    public class WaveEffect : Effect {
        public override void Update()
        {
            this.EffectData["frame"] = (int)(this.EffectData["frame"] ?? -1) + 1;
            base.Update();
        }

        public override void Render()
        {
            int frame = ((int)(this.EffectData["frame"] ?? 0) + 1);
            float progress = (float)this.EffectTimer.ElapsedMilliseconds / this.EffectTime;
            // Convert progress to span over the screen
            int screenWidthProgress = (int)Math.Round(progress * (Console.BufferWidth - 1));
            Console.SetCursorPosition(0, 0);

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new RGB(255, 255, 255).ToBGStr() + new String(' ', screenWidthProgress) + RGB.Reset());
            base.Render();
        }
    }
}