using System;

using NebulaOS.Graphics;
using NebulaOS.Files.NJSON;
using NebulaOS.NSystem;

namespace NebulaOS.Graphics {
    public class DrawConfig {
        public string HorizontalLine = "─";
        public string VerticalLine = "│";
        
        public string CornerTopLeft = "┌";
        public string CornerTopRight = "┐";
        public string CornerBottomLeft = "└";
        public string CornerBottomRight = "┘";

        public string RoundCornerTopLeft = "╭";
        public string RoundCornerTopRight = "╮";
        public string RoundCornerBottomLeft = "╰";
        public string RoundCornerBottomRight = "╯";

        public string VerticalSlashRight = "╱";
        public string VerticalSlashLeft = "╲";
        public string SlashCross = "╳";

        public string TopTree = "┬";
        public string BottomTree = "┴";
        public string LeftTree = "├";
        public string RightTree = "┤";
        public string Cross = "┼";
        
        public string ArrowUp = "↑";
        public string ArrowDown = "↓";
        public string ArrowLeft = "←";
        public string ArrowRight = "→";

        public string ArrowUpLeft = "↖";
        public string ArrowUpRight = "↗";
        public string ArrowDownLeft = "↙";
        public string ArrowDownRight = "↘";

        public string ArrowUpDown = "↕";
        public string ArrowLeftRight = "↔";

        public string VerticalLineDouble = "║";
        public string HorizontalLineDouble = "═";

        public string CornerTopLeftDouble = "╔";
        public string CornerTopRightDouble = "╗";
        public string CornerBottomLeftDouble = "╚";
        public string CornerBottomRightDouble = "╝";

        public string TopTreeDouble = "╦";
        public string BottomTreeDouble = "╩";
        public string LeftTreeDouble = "╠";
        public string RightTreeDouble = "╣";
        public string CrossDouble = "╬";

        public string Pencil = "✎";
        public string Check = "✓";
        public string CrossMark = "✗";
        public string Warning = "⚠";
        public string Info = "ⓘ";
        public string Star = "★";
        public string StarEmpty = "☆";

        public string ThickCheck = "✔";
        public string ThickCross = "✖";

        public string CheckboxEmpty = "☐";
        public string CheckboxChecked = "☑";
        public string CheckboxCrossed = "☒";
        public string CheckboxCrossedChecked = "☓";

        public string ProgressBar = "█";
        public string ProgressBarEmpty = "░";
        public string ProgressBarBuffer = "▒";

        public string StemlessArrowUp = "▲";
        public string StemlessArrowDown = "▼";
        public string StemlessArrowLeft = "◀";
        public string StemlessArrowRight = "▶";

        public string StemlessArrowUpEmpty = "△";
        public string StemlessArrowDownEmpty = "▽";
        public string StemlessArrowLeftEmpty = "◁";
        public string StemlessArrowRightEmpty = "▷";

        public string ScrollBar = "|";

        public string Underline = "━";
        public string DoubleUnderline = "═";
        public string HalfUnderline = "▄";

        public string Key = "🔑";
        public string Lock = "🔒";
        public string LockOpen = "🔓";
        public string Bell = "🔔";
        public string BellDisabled = "🔕";
        public string Paperclip = "📎";
        public string Link = "🔗";
        public string Document = "📄";
        public string Book = "📖";
        public string Box = "📦";
        public string Archive = "📁";
        public string Disk = "💾";
        public string CDDisk = "💿";
        public string CDDiskAlt = "💽";
        public string Email = "📧";
        public string Calendar = "📅";
        public string World = "🌎";
        public string Pen = "🖊";
        public string Clipboard = "📋";
        public string Upload = "📤";
        public string Image = "🖼";
        public string Computer = "🖥";
        public string Mouse = "🖱";
        public string Keyboard = "🖲";
        public string Printer = "🖨";
        public string Mobile = "📱";
        public string KeyLock = "🔐";
        public string User = "👤";
        public string UserGroup = "👥";
    }

    public class WindowTheme {
        public RGB ForegroundColor = new RGB(255, 255, 255);
        public RGB TitlebarBackground = new RGB(90, 60, 130);
        public RGB TitleTextColor = new RGB(200, 150, 255);
        public RGB WindowBackground = new RGB(20, 10, 30);
        public DrawConfig DrawConfig = new DrawConfig();

        /// <summary>
        /// Load a theme from a file
        /// </summary>
        /// <param name="file">The file to load the theme from</param>
        /// <param name="checkParams">Whether to check the parameters and fields of the theme</param>
        /// <returns>The loaded theme</returns>
        public static WindowTheme? LoadFromFile(string file, bool checkParams = true) {
            WindowTheme theme = new WindowTheme();
            dynamic? json = JSON.ParseFile(file);
            if (json == null) { return null; }

            if (checkParams) {
                List<string> undefined = JSON.GetUndefinedValues(json, typeof(WindowTheme));
                List<Tuple<string, string>> unknown = JSON.GetUnknownValues(json, typeof(WindowTheme));

                if (undefined.Count > 0) {
                    Logging.LogError("Value '" + undefined[0] + "' is undefined in theme file '" + file + "'");
                }

                if (unknown.Count > 0) {
                    Logging.LogError("Value '" + unknown[0].Item1 + "' is unknown in theme file '" + file + "'");
                }
            }

            return JSON.ParseFile<WindowTheme>(file) ?? theme;
        }
    }

    public class Window {
        public int Width, Height;
        public string Title;
        public WindowTheme Theme;

        /// <summary>
        /// Attempt to load a theme from a file
        /// </summary>
        /// <param name="file">The file to load the theme from</param>
        /// <param name="checkParams">Whether to check the parameters and fields of the theme</param>
        public void LoadTheme(string file, bool checkParams = true) {
            Theme = WindowTheme.LoadFromFile(file, checkParams) ?? Theme;
        }

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

        /// <summary>
        /// Fit Vector2i to bounds of the screen.
        /// </summary>
        /// <param name="pos">Position to fit</param>
        /// <returns>Fitted position</returns>
        public Vector2i FitToBounds(Vector2i pos) {
            if (pos.X < 0) { pos.X = 0; }
            if (pos.X > Width) { pos.X = Width; }

            if (pos.Y < 1) { pos.Y = 1; }
            if (pos.Y > Height - 1) { pos.Y = Height - 1; }
            return pos;
        }

        /// <summary>
        /// Fit Vector2i to bounds of the screen based on a string.
        /// </summary>
        /// <param name="pos">Position to fit</param>
        /// <param name="text">Text to fit</param>
        /// <returns>Fitted position</returns>
        public Vector2i FitToBounds(Vector2i pos, string text) {
            if (pos.X < 0) { pos.X = 0; }
            if (pos.X > Width - text.Length) { pos.X = Width - text.Length; }

            if (pos.Y < 1) { pos.Y = 1; }
            if (pos.Y > Height - 1) { pos.Y = Height - 1; }
            return pos;
        }

        /// <summary>
        /// Draw text to the window
        /// </summary>
        /// <param name="text">Text to draw</param>
        /// <param name="pos">Position to draw the text</param>
        /// <param name="fgColor">Foreground color of the text</param>
        /// <param name="bgColor">Background color of the text</param>
        /// <param name="clear">Clear the window before drawing</param>
        public void DrawText(string text, Vector2i pos, RGB fgColor, RGB bgColor) {
            pos = this.FitToBounds(pos, text);
            Print.AtPos(text, pos, fgColor, bgColor, true);
        }

        /// </summary>
        /// <param name="start">Start position of the line</param>
        /// <param name="end">End position of the line</param>
        /// <param name="fgColor">Foreground color of the line</param>
        /// <param name="bgColor">Background color of the line</param>
        /// <param name="clear">Clear the window before drawing</param>
        public void DrawHorizontalLine(int start, int end, int y, RGB? fgColor = null, RGB? bgColor = null, String? character = null) {
            start = start < 0 ? 0 : start;
            end = end > Width ? Width : end;
            y = y < 1 ? 1 : y;
            y = y > Height - 1 ? Height - 1 : y;

            if (end < start) {
                int temp = end;
                end = start;
                start = temp;
            }

            fgColor = fgColor ?? Theme.ForegroundColor;
            bgColor = bgColor ?? Theme.WindowBackground;
            character = character ?? Theme.DrawConfig.HorizontalLine;

            for (int x = start; x < end; x++) {
                Print.AtPos(character, new Vector2i(x, y), fgColor, bgColor, true);
            }
        }

        /// <summary>
        /// Draw a vertical line to the window
        /// </summary>
        /// <param name="start">Start position of the line</param>
        /// <param name="end">End position of the line</param>
        /// <param name="fgColor">Foreground color of the line</param>
        /// <param name="bgColor">Background color of the line</param>
        /// <param name="clear">Clear the window before drawing</param>
        public void DrawVerticalLine(int start, int end, int x, RGB? fgColor = null, RGB? bgColor = null, String? character = null) {
            start = start < 0 ? 0 : start;
            end = end > Height - 1 ? Height - 1 : end;
            x = x < 0 ? 0 : x;
            x = x > Width - 1 ? Width - 1 : x;
            if (end < start) {
                int temp = end;
                end = start;
                start = temp;
            }

            fgColor = fgColor ?? Theme.ForegroundColor;
            bgColor = bgColor ?? Theme.WindowBackground;
            character = character ?? Theme.DrawConfig.VerticalLine;
            for (int y = start; y < end; y++) {
                Print.AtPos(character, new Vector2i(x, y), fgColor, bgColor, true);
            }
        }

        /// <summary>
        /// Draw a box to the window
        /// </summary>
        /// <param name="start">Start position of the box</param>
        /// <param name="end">End position of the box</param>
        /// <param name="fgColor">Foreground color of the box</param>
        /// <param name="bgColor">Background color of the box</param>
        /// <param name="clear">Clear the window before drawing</param>
        public void DrawBox(Vector2i start, int scale, RGB? fgColor = null, RGB? bgColor = null, String? horizontal = null, String? vertical = null, String? topLeft = null, String? topRight = null, String? bottomLeft = null, String? bottomRight = null) {
            (Vector2i, Vector2i) positions = this.CalculateSquare(start, scale);
            Vector2i startPos = positions.Item1;
            Vector2i endPos = positions.Item2;

            fgColor = fgColor ?? Theme.ForegroundColor;
            bgColor = bgColor ?? Theme.WindowBackground;

            horizontal = horizontal ?? Theme.DrawConfig.HorizontalLine;
            vertical = vertical ?? Theme.DrawConfig.VerticalLine;
            topLeft = topLeft ?? Theme.DrawConfig.CornerTopLeft;
            topRight = topRight ?? Theme.DrawConfig.CornerTopRight;
            bottomLeft = bottomLeft ?? Theme.DrawConfig.CornerBottomLeft;
            bottomRight = bottomRight ?? Theme.DrawConfig.CornerBottomRight;

            // Top line
            this.DrawHorizontalLine(startPos.X, endPos.X, startPos.Y, fgColor, bgColor, horizontal);
            // Bottom line
            this.DrawHorizontalLine(startPos.X, endPos.X, endPos.Y, fgColor, bgColor, horizontal);
            // Left line
            this.DrawVerticalLine(startPos.Y, endPos.Y, startPos.X, fgColor, bgColor, vertical);
            // Right line
            this.DrawVerticalLine(startPos.Y, endPos.Y, endPos.X, fgColor, bgColor, vertical);

            // Draw corners
            this.DrawText(topLeft, new Vector2i(startPos.X, startPos.Y), fgColor, bgColor);
            this.DrawText(topRight, new Vector2i(endPos.X, startPos.Y), fgColor, bgColor);
            this.DrawText(bottomLeft, new Vector2i(startPos.X, endPos.Y), fgColor, bgColor);
            this.DrawText(bottomRight, new Vector2i(endPos.X, endPos.Y), fgColor, bgColor);
        }

        /// <summary>
        /// Draw a rectangle to the window
        /// </summary>
        /// <param name="start">Start position of the rectangle</param>
        /// <param name="end">End position of the rectangle</param>
        /// <param name="fgColor">Foreground color of the rectangle</param>
        /// <param name="bgColor">Background color of the rectangle</param>
        /// <param name="clear">Clear the window before drawing</param>
        public void DrawRectangle(Vector2i start, Vector2i end, RGB? fgColor = null, RGB? bgColor = null, String? horizontal = null, String? vertical = null, String? topLeft = null, String? topRight = null, String? bottomLeft = null, String? bottomRight = null) {
            if (start.X > end.X) {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }

            start = this.FitToBounds(start);
            end = this.FitToBounds(end);
            
            fgColor = fgColor ?? Theme.ForegroundColor;
            bgColor = bgColor ?? Theme.WindowBackground;

            horizontal = horizontal ?? Theme.DrawConfig.HorizontalLine;
            vertical = vertical ?? Theme.DrawConfig.VerticalLine;
            topLeft = topLeft ?? Theme.DrawConfig.CornerTopLeft;
            topRight = topRight ?? Theme.DrawConfig.CornerTopRight;
            bottomLeft = bottomLeft ?? Theme.DrawConfig.CornerBottomLeft;
            bottomRight = bottomRight ?? Theme.DrawConfig.CornerBottomRight;

            // Top line
            this.DrawHorizontalLine(start.X, end.X, start.Y, fgColor, bgColor, horizontal);
            // Bottom line
            this.DrawHorizontalLine(start.X, end.X, end.Y, fgColor, bgColor, horizontal);
            // Left line
            this.DrawVerticalLine(start.Y, end.Y, start.X, fgColor, bgColor, vertical);
            // Right line
            this.DrawVerticalLine(start.Y, end.Y, end.X, fgColor, bgColor, vertical);
            
            // Draw corners
            this.DrawText(topLeft, new Vector2i(start.X, start.Y), fgColor, bgColor);
            this.DrawText(topRight, new Vector2i(end.X, start.Y), fgColor, bgColor);
            this.DrawText(bottomLeft, new Vector2i(start.X, end.Y), fgColor, bgColor);
            this.DrawText(bottomRight, new Vector2i(end.X, end.Y), fgColor, bgColor);
        }

        /// <summary>
        /// Calculate the shape of a square based on the equation:<br/>
        /// <code>
        /// w = s*2
        /// </code>
        /// </summary>
        /// <param name="start">Start position of the square</param>
        /// <param name="scale">Scale of the square</param>
        /// <returns>The shape of the square as a tuple of start and end positions</returns>
        public (Vector2i, Vector2i) CalculateSquare(Vector2i start, int scale) {
            Vector2i startPos = this.FitToBounds(start);
            Vector2i endPos = new Vector2i(startPos.X + (scale * 2), startPos.Y + scale);
            endPos = this.FitToBounds(endPos);
            return (startPos, endPos);
        }
    }
}