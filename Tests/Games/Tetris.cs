using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using NebulaOS.Programs;
using NebulaOS.Files;
using NebulaOS.Graphics;

namespace NebulaOS.Tests.TestClasses {
    public class TetrisTest : Test {
        public override void Init()
        {
            TetrisGame Tetris = new TetrisGame();
            Tetris.Run();
            base.Init();
        }

        public override bool CallOnBoot() { return false; }
    }

    public class TetrisGame : Game {
        public ConsoleKey GetKeyDown() {
            if (Console.KeyAvailable) {
                return Console.ReadKey(true).Key;
            } else {
                return ConsoleKey.NoName;
            }
        }

        public float Score = 0;
        public Window? GameWindow;

        public override void Start() {
            Console.WriteLine("Tetris game started.");
            this.FPS = 30;

            GameWindow = new Window(Console.BufferWidth, 10, "Tetris", new WindowTheme() {
                WindowBackground = new RGB(0, 0, 0),
                TitlebarBackground = new RGB(30, 70, 70),
                TitleTextColor = new RGB(0, 255, 255)
            });

            Console.Clear();
            GameWindow.Init();
        }

        public double Timer = 0;
        public float MoveTime = 1000;

        public override void Update() {
            if (GameWindow == null) return;

            if (GetKeyDown() == ConsoleKey.Escape) {
                this.Stop();
            }
            
            GameWindow.DrawText("Score: " + Score.ToString(), new Vector2i(1000, 1000), new RGB(100, 255, 200), new RGB(0, 0, 0));
            
            Timer += this.deltaTime;
            if (Timer > MoveTime) {
                Timer = 0;
                Score += 100;
            }

            GameWindow.DrawText("Time: " + this.deltaTime, new Vector2i(0, 0), new RGB(100, 255, 200), new RGB(0, 0, 0));
        }

        public override void End() {
            Console.WriteLine("Tetris game ended.");
        }
    }
}