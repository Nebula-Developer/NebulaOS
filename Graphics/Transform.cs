using System;

namespace NebulaOS.Graphics {
    public class Vector2 {
        public float X, Y;

        /// <summary>
        /// Vector2 constructor
        /// </summary>
        public Vector2(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class Vector2i {
        public int X, Y;

        /// <summary>
        /// Vector2 integer constructor
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public Vector2i(int x, int y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class Rotation {
        public float X, Y, Z;

        /// <summary>
        /// 3-Axis Rotation constructor
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public Rotation(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    public class Transform {
        public Vector2 Position;
        public Vector2 Scale;
        public Rotation Rotation;
        public String Name = "Transform";

        /// <summary>
        /// Transform constructor
        /// </summary>
        /// <param name="position">Position of the transform</param>
        /// <param name="scale">Scale of the transform</param>
        /// <param name="rotation">Rotation of the transform</param>
        public Transform(Vector2 position, Vector2 scale, Rotation rotation) {
            this.Position = position;
            this.Scale = scale;
            this.Rotation = rotation;
        }
    }
}