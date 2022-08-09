using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Collections.Generic;

namespace NebulaOS.Maths {
    public class Curve {
        public int Start, End;

        /// <summary>
        /// Curve constructor
        /// </summary>
        /// <param name="start">Start of the curve</param>
        /// <param name="end">End of the curve</param>
        public Curve(int start, int end) {
            this.Start = start;
            this.End = end;
        }


        #region Easing Functions
        public float EaseInOut(float t) {
            return (t < 0.5f) ? (2 * t * t) : (-1 + (4 - 2 * t) * t);
        }

        public float EaseIn(float t) {
            return t * t;
        }

        public float EaseOut(float t) {
            return 1 - (1 - t) * (1 - t);
        }

        public float Bounce(float t) {
            if (t < 0.5f) {
                return EaseOut(t * 2) * 0.5f;
            } else {
                return EaseIn((t - 0.5f) * 2) * 0.5f + 0.5f;
            }
        }

        public float Linear(float t) {
            return t;
        }

        public float ExponentialInOut(float t) {
            return (float)((t < 0.5f) ? (Math.Pow(2, 20 * t - 10) * 0.5f) : (1 - Math.Pow(2, -20 * t + 10) * 0.5f));
        }

        public float ExponentialIn(float t) {
            return (float)(Math.Pow(2, 10 * (t - 1)));
        }

        public float ExponentialOut(float t) {
            return (float)(1 - Math.Pow(2, -10 * t));
        }

        public float SineInOut(float t) {
            return (float)(-0.5f * (Math.Cos(Math.PI * t) - 1));
        }

        public float SineIn(float t) {
            return (float)(1 - Math.Cos(t * Math.PI / 2));
        }

        public float SineOut(float t) {
            return (float)(Math.Sin(t * Math.PI / 2));
        }

        public float CubicInOut(float t) {
            return (float)((t < 0.5f) ? (4 * t * t * t) : ((t - 1) * (2 * t - 2) * (2 * t - 2) + 1));
        }

        public float CubicIn(float t) {
            return (float)(t * t * t);
        }

        public float CubicOut(float t) {
            return (float)(1 - (1 - t) * (1 - t) * (1 - t));
        }
        #endregion
        
        public enum Ease {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut,
            Bounce,
            ExponentialIn,
            ExponentialOut,
            ExponentialInOut,
            SineIn,
            SineOut,
            SineInOut,
            CubicIn,
            CubicOut,
            CubicInOut
        };

        public float GetValue(float t, Ease ease = Ease.Linear) {
            switch (ease) {
                case Ease.Linear:
                    return Linear(t);
                case Ease.EaseIn:
                    return EaseIn(t);
                case Ease.EaseOut:
                    return EaseOut(t);
                case Ease.EaseInOut:
                    return EaseInOut(t);
                case Ease.Bounce:
                    return Bounce(t);
                case Ease.ExponentialIn:
                    return ExponentialIn(t);
                case Ease.ExponentialOut:
                    return ExponentialOut(t);
                case Ease.ExponentialInOut:
                    return ExponentialInOut(t);
                case Ease.SineIn:
                    return SineIn(t);
                case Ease.SineOut:
                    return SineOut(t);
                case Ease.SineInOut:
                    return SineInOut(t);
                case Ease.CubicIn:
                    return CubicIn(t);
                case Ease.CubicOut:
                    return CubicOut(t);
                case Ease.CubicInOut:
                    return CubicInOut(t);
                default:
                    return Linear(t);
            }
        }

        /// <summary>
        /// Calculates start and end value based on a time and a curve.
        /// </summary>
        /// <param name="t">Time</param>
        /// <param name="curve">Curve</param>
        public float GetStartEndValue(float t, Ease ease = Ease.Linear) {
            return GetValue(t, ease) * (End - Start) + Start;
        }
        
        /// <summary>
        /// Eases a value from start to end over a specified time.
        /// </summary>
        /// <param name="t">The time to ease in (ms).</param>
        /// <param name="ease">The ease to use.</param>
        /// <param name="callEvent">Event called on update.</param>
        /// <param name="useThread">Use a thread for the event.</param>
        /// <param name="onEnd">Event called on end.</param>
        /// <param name="step">Animation step value (try not to use this).</param>
        public void EaseOverMS(float t, Ease ease = Ease.Linear, Action<float>? callEvent = null, bool useThread = false, Action? onEnd = null, int step = 16) {
            float t2 = 0;
            if (useThread) {
                Thread thread = new Thread(() => {
                    while (t2 < t) {
                        t2 += step;
                        callEvent?.Invoke(GetStartEndValue(t2 / t, ease));
                        Thread.Sleep(step);
                    }
                    onEnd?.Invoke();
                });
                thread.Start();
            } else {
                while (t2 < t) {
                    t2 += step;
                    callEvent?.Invoke(GetStartEndValue(t2 / t, ease));
                    NSystem.Generic.OS.Sleep(step);
                }
                onEnd?.Invoke();
            }
            return;
        }
    }
}