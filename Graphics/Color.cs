using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NebulaOS.Graphics {
    public class Color {
        /// <summary>
        /// Convert two RGB colors into a single string escape sequence
        /// This uses the RGB escape sequence: \e[38;2;r;g;bm
        /// </summary>
        /// <param name="Foreground">The foreground color</param>
        /// <param name="Background">The background color</param>
        public static String CombineFB(RGB Foreground, RGB Background) {
            return String.Format("\x1b[38;2;{0};{1};{2}m", Foreground.r, Foreground.g, Foreground.b) +
                   String.Format("\x1b[48;2;{0};{1};{2}m", Background.r, Background.g, Background.b);
        }

        /// <summary>
        /// Reset color escape sequence.
        /// </summary>
        public static String Reset() {
            return "\x1b[0m";
        }
    }

    public class RGB {
        public int r = 0, g = 0, b = 0;

        /// <summary>
        /// RGB Color constructor.
        /// </summary>
        /// <param name="r">Red value.</param>
        /// <param name="g">Green value.</param>
        /// <param name="b">Blue value.</param>
        public RGB(int r, int g, int b) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.OnFade = delegate (RGB col) { };
        }

        /// <summary>
        /// Convert color to safe values (0-255).
        /// </summary>
        public RGB ToSafe() {
            return new RGB(
                Math.Max(Math.Min(r, 255), 0),
                Math.Max(Math.Min(g, 255), 0),
                Math.Max(Math.Min(b, 255), 0)
            );
        }

        /// <summary>
        /// Automatically set the color to a safe value.
        /// </summary>
        public void MakeSafe() {
            this.r = Math.Max(Math.Min(r, 255), 0);
            this.g = Math.Max(Math.Min(g, 255), 0);
            this.b = Math.Max(Math.Min(b, 255), 0);
        }

        /// <summary>
        /// Set the color values.
        /// </summary>
        public void Set(int r, int g, int b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /// <summary>
        /// Convert RGB to string escape sequence.
        /// </summary>
        public string ToStr() {
            return String.Format("\x1b[38;2;{0};{1};{2}m", r, g, b);
        }

        /// <summary>
        /// Convert RGB to background string escape sequence.
        /// </summary>
        public string ToBGStr() {
            return String.Format("\x1b[48;2;{0};{1};{2}m", r, g, b);
        }

        /// <summary>
        /// Brighten the color by specified amount.
        /// </summary>
        /// <param name="amount">The amount to brighten the color by.</param>
        /// <param name="makeSafe">Whether to make the color safe after setting values.</param>
        public void Brighten(int amount, bool makeSafe = true) {
            this.r += amount;
            this.g += amount;
            this.b += amount;
            if (makeSafe) this.MakeSafe();
        }

        /// <summary>
        /// Darken the color by specified amount.
        /// </summary>
        /// <param name="amount">The amount to darken the color by.</param>
        /// <param name="makeSafe">Whether to make the color safe after setting values.</param>
        public void Darken(int amount, bool makeSafe = true) {
            this.r -= amount;
            this.g -= amount;
            this.b -= amount;
            if (makeSafe) this.MakeSafe();
        }

        /// <summary>
        /// Fade the color to another color by specified amount.
        /// </summary>
        /// <param name="color">The color to fade to.</param>
        /// <param name="percent">The amount to fade by.</param>
        /// <param name="makeSafe">Whether to make the color safe after setting values.</param>
        public RGB FadeTo(RGB color, float percent, bool makeSafe = true) {
            percent /= 100;
            RGB newCol = new RGB(0, 0, 0);
            newCol.r = (int)((float)this.r + ((float)color.r - (float)this.r) * percent);
            newCol.g = (int)((float)this.g + ((float)color.g - (float)this.g) * percent);
            newCol.b = (int)((float)this.b + ((float)color.b - (float)this.b) * percent);
            if (makeSafe) newCol.MakeSafe();
            return newCol;
        }

        /// <summary>
        /// Fade the color from another color by specified amount.
        /// </summary>
        /// <param name="color">The color to fade from.</param>
        /// <param name="percent">The amount to fade by.</param>
        /// <param name="makeSafe">Whether to make the color safe after setting values.</param>
        public RGB FadeFrom(RGB color, float percent, bool makeSafe = true) {
            percent /= 100;
            RGB newCol = new RGB(0, 0, 0);
            newCol.r = (int)((float)color.r + ((float)this.r - (float)color.r) * percent);
            newCol.g = (int)((float)color.g + ((float)this.g - (float)color.g) * percent);
            newCol.b = (int)((float)color.b + ((float)this.b - (float)color.b) * percent);
            if (makeSafe) newCol.MakeSafe();
            return newCol;
        }

        /// <summary>
        /// Fade the color over time, whilst calling the OnFade event.
        /// </summary>
        /// <param name="color">The color to fade to.</param>
        /// <param name="time">The time to fade over.</param>
        public void FadeInMS(RGB color, float time, int incrementAmount = 100) {
            float timeMS = time;
            // 1 * 100 = 1/10th of a second

            for (int i = 0; i < timeMS; i += 50) {
                this.OnFade(FadeTo(color, i / timeMS * 100));
                Sleep(50);
            }
        }

        /// <summary>
        /// Sleep for spesified MS
        /// </summary>
        /// <param name="ms">The amount of MS to sleep for.</param>
        public void Sleep(int ms) { new System.Threading.ManualResetEvent(false).WaitOne(ms); }

        /// <summary>
        /// Event called when the color is faded.
        /// </summary>
        public event Action<RGB> OnFade;

        /// <summary>
        /// Set the color to a random color.
        /// </summary>
        /// <param name="min">The minimum value for each color channel.</param>
        /// <param name="max">The maximum value for each color channel.</param>
        public void Randomize(int min = 0, int max = 255) {
            this.r = new Random().Next(min, max);
            this.g = new Random().Next(min, max);
            this.b = new Random().Next(min, max);
        }

        /// <summary>
        /// Convert RGB to hexadecimal string.
        /// </summary>
        public string ToHex() {
            return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
        }

        /// <summary>
        /// Create RGB constructor from hexadecimal string.
        /// </summary>
        /// <param name="hex">The hexadecimal string.</param>
        public static RGB FromHex(string hex) {
            if (hex.StartsWith("#")) hex = hex.Substring(1);
            if (hex.Length != 6) throw new Exception("Invalid hexadecimal string.");
            return new RGB(
                Convert.ToInt32(hex.Substring(0, 2), 16),
                Convert.ToInt32(hex.Substring(2, 2), 16),
                Convert.ToInt32(hex.Substring(4, 2), 16)
            );
        }

        /// <summary>
        /// Color reset escape sequence.
        /// </summary>
        public static string Reset() {
            return "\x1b[0m";
        }

        /// <summary>
        /// Color background escape sequence.
        /// </summary>
        public static string ResetBG() {
            return "\x1b[49m";
        }

        /// <summary>
        /// Convert color to gradient.
        /// </summary>
        /// <param name="toColor">The color to fade to.</param>
        /// <param name="size">The size of the gradient.</param>
        /// <returns>RGB color array based on the gradient settings.</returns>
        public List<RGB> ToGradient(RGB toColor, int size) {
            List<RGB> colors = new List<RGB>();
            float percent = 0;

            for (int i = 0; i < size; i++) {
                percent += 100f / (float)size;
                colors.Add(FadeTo(toColor, percent));
            }

            colors.Add(toColor);
            return colors;
        }

        #region Operators
        public static RGB operator +(RGB a, RGB b) { return new RGB(a.r + b.r, a.g + b.g, a.b + b.b); }
        public static RGB operator -(RGB a, RGB b) { return new RGB(a.r - b.r, a.g - b.g, a.b - b.b); }
        public static RGB operator *(RGB a, RGB b) { return new RGB(a.r * b.r, a.g * b.g, a.b * b.b); }
        public static RGB operator /(RGB a, RGB b) { return new RGB(a.r / b.r, a.g / b.g, a.b / b.b); }
        public static RGB operator ++(RGB a) { a.r++; a.g++; a.b++; return a; }
        public static RGB operator --(RGB a) { a.r--; a.g--; a.b--; return a; }
        #endregion
    }
}