using System;

namespace NebulaOS.Graphics {
    public class Print {
        /// <summary>
        /// Print a string at a spesified location
        /// </summary>
        /// <param name="str">String to print</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="color">Color of the string</param>
        /// <param name="bgColor">Background color of the string</param>
        /// <param name="returnToOldPos">Return to old position after printing</param>
        public static void AtPos(string str, int x, int y, RGB? color = null, RGB? bgColor = null, bool returnToOldPos = true) {
            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

            Console.SetCursorPosition(x, y);
            if (color != null) {
                if (bgColor != null) {
                    Console.Write(Color.CombineFB(color, bgColor) + str + Color.Reset());
                } else {
                    Console.Write(color.ToStr() + str + Color.Reset());
                }
            } else if (bgColor != null) {
                Console.Write(bgColor.ToStr() + str + Color.Reset());
            } else {
                Console.Write(str);
            }

            if (returnToOldPos)
                Console.SetCursorPosition(oldX, oldY);
        }

        /// <summary>
        /// Print a string at a spesified location
        /// </summary>
        /// <param name="str">String to print</param>
        /// <param name="pos">Position of the string</param>
        /// <param name="color">Color of the string</param>
        /// <param name="bgColor">Background color of the string</param>
        /// <param name="returnToOldPos">Return to old position after printing</param>
        public static void AtPos(string str, Vector2i pos, RGB? color = null, RGB? bgColor = null, bool returnToOldPos = true) {
            AtPos(str, pos.X, pos.Y, color, bgColor, returnToOldPos);
        }
    }
}