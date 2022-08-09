using System;
using NebulaOS.Graphics;

namespace NebulaOS.Graphics {
    public class WindowTheme {
        public RGB ForegroundColor = new RGB(255, 255, 255);
        public RGB TitlebarBackground = new RGB(90, 60, 130);
        public RGB TitleTextColor = new RGB(200, 150, 255);
        public RGB WindowBackground = new RGB(20, 10, 30);
    }

    public class Window {
        public int Width, Height;
        public string Title;
        public WindowTheme Theme;

        /// <summary>
        /// Window constructor
        /// </summary>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        /// <param name="title">Title of the window</param>
        /// <param name="theme">Theme of the window</param>
        public Window(int width, int height, string title, WindowTheme theme) {
            this.Width = width;
            this.Height = height;
            this.Title = title;
            this.Theme = theme;
        }

        /// <summary>
        /// Center text to the window.
        /// </summary>
        /// <param name="text">Text to center</param>
        /// <param name="y">Y position</param>
        public Vector2i CenterText(string text, int y) {
            return new Vector2i((this.Width / 2) - (text.Length / 2), y);
        }

        /// <summary>
        /// Initialize the window
        /// </summary>
        public void Init() {
            String bgRefresh = new String(' ', Width);

            for (int y = 0; y < Height; y++) {
                Print.AtPos(bgRefresh, new Vector2i(0, y), Theme.ForegroundColor, Theme.WindowBackground, true);
            }

            Print.AtPos(bgRefresh, new Vector2i(0, 0), Theme.ForegroundColor, Theme.TitlebarBackground, true);
            Print.AtPos(Title, this.CenterText(Title, 0), Theme.TitleTextColor, Theme.TitlebarBackground, true);

            Console.Write(RGB.Reset());
        }
    }
}