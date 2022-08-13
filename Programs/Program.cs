using System;
using System.Collections.Generic;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NebulaOS.Programs {
    public class Game {
        /// <summary>
        /// This function is called on the beginning of the game.
        /// </summary>
        public virtual void Start() { }

        private bool IsRunning = true;

        /// <summary>
        /// This function is called every frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// This function is called on the end of the game.
        /// </summary>
        public virtual void End() { }

        /// <summary>
        /// FPS value (frames per second).
        /// </summary>
        public int FPS { get; set; } = 15;

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Run() {
            this.Start();

            Thread thread = new Thread(this.CalculateDelta);
            thread.Start();

            while (true) {
                if (!IsRunning) break;
                this.Update();
                Thread.Sleep(1000 / this.FPS);
            }

            this.End();
        }

        /// <summary>
        /// DeltaTime calculation thread
        /// </summary>
        public void CalculateDelta() {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
        
            while (this.IsRunning) {
                TimeSpan ts = stopWatch.Elapsed;
                double FirstFrame = ts.TotalMilliseconds;
                
                this.deltaTime = FirstFrame - this.secondFrame;
                
                this.counter += this.deltaTime;

                this.secondFrame = ts.TotalMilliseconds;

                Thread.Sleep(1000 / this.FPS);
            }
        }

        public double deltaTime = 0;
        public double counter = 0;
        public double secondFrame = 0;

        /// <summary>
        /// Stop the game.
        /// </summary>
        public void Stop() {
            this.IsRunning = false;
        }

        /// <summary>
        /// Dynamic game data variable.
        /// </summary>
        public dynamic Data { get; set; } = new JObject();
    }
}