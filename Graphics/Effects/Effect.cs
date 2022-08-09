using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NebulaOS.NSystem.Generic;

namespace NebulaOS.Graphics {
    public class Effect {
        public void Init() {
            EffectTimer = new Stopwatch();
            EffectTimer.Start();
            bool Complete = false;
            
            while (this.Running && !Complete) {
                this.Update();
                this.Render();
                OS.Sleep(1000 / UpdateFreq);

                if (this.EffectTime != -1)
                    Complete = EffectTimer.ElapsedMilliseconds < this.EffectTime ? false : true;
            }
        }

        public void Destroy() {
            this.Running = false;
        }

        public int UpdateFreq = 5;
        public bool Running = true;
        public float EffectTime = -1;
        public Stopwatch EffectTimer = new Stopwatch();
        
        public virtual void Render() { }
        public virtual void Update() { }

        public JObject EffectData = new JObject();
    }
}