using System;

namespace Jinx.Src.Math {

    public class Vector2{

        public static readonly ZERO = new Vector2(0, 0);
        public static readonly ONE = new Vector2(1, 1);
        public static readonly UP = new Vector2(0, 1);
        public static readonly DOWN = new Vector2(0, -1);
        public static readonly LEFT = new Vector2(-1, 0);
        public static readonly RIGHT = new Vector2(1, 0);
        public static readonly X_AXIS = Vector2.RIGHT;
        public static readonly Y_AXIS = Vector2.UP;

        public static Vector2 Copy(Vector2 v) {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 Inverse(Vector2 v) {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 Add(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 Subtract(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 Multiply(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 Divide(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }

        public static Vector2 MultiplyScalar(Vector2 v, float n) {
            return new Vector2(v.x * n, v.y * n);
        }

        public static Vector2 DivideScalar(Vector2 v, float n) {
            return new Vector2(v.x / n, v.y / n);
        }

        public static float DistanceBetween(Vector2 v, float n) {
            return v1.DistanceTo(v2);
        }

        public static float AngleBetween(Vector2 v1, Vector2 v2) {
            return v1.AngleBetween(v2);
        }

        public static float AngleBetweenSigned(Vector2 v1, Vector2 v2) {
            return v1.AngleBetweenSigned(v2);
        }

        public static float Dot(Vector2 v1, Vector2 v2) {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static Vector2 Normalize(Vector2 v) {

            float sizeSquared = v.x * v.x + v.y * v.y;

            if (sizeSquared < 1e-8) {
                return new Vector2();
            }

            float scaleFactor = 1 / (float)Math.Sqrt(sizeSquared);
            return new Vector2(v.x * scaleFactor, v.y * scaleFactor);
        }

        public static Vector2 Rotate(Vector2 v, float angle) {
            return new Vector2((float)Math.Cos(angle) * v.x - (float)Math.Sin(angle) * v.y, (float)Math.sin(angle) * v.x + (float)Math.Cos(angle) * v.y);
        }

        public float x;
        public float y;

        public Vector2(float x = 0, float y = 0) {

            this.x = x;
            this.y = y;
        }

        public void Set(float x, float y) {

            this.x = x;
            this.y = y;
        }

        public void Copy(Vector2 v) {

            this.x = v.x;
            this.y = v.y;
        }

        public Vector2 Clone() {
            return new Vector2(this.x, this.y);
        }

        public bool Equals(Vector2 v) {
            return this.x == v.x && this.y == v.y;
        }

        public void Add(Vector2 v) {

            this.x += v.x;
            this.y += v.y;
        }

        public void Subtract(Vector2 v) {

            this.x -= v.x;
            this.y -= v.y;
        }

        public void Multiply(Vector2 v) {

            this.x *= v.x;
            this.y *= v.y;
        }

        public void Divide(Vector2 v) {

            this.x /= v.x;
            this.y /= v.y;
        }

        public void MultiplyScalar(float n) {

            this.x *= n;
            this.y *= n;
        }

        public void DivideScalar(float n) {

            this.x /= n;
            this.y /= n;
        }

        public float DistanceTo(Vector2 v) {

            return (float)Math.Sqrt(
                (this.x - v.x) * (this.x - v.x) +
                (this.y - v.y) * (this.y - v.y)
            );
        }

        public void SetPositionFromMatrix(Matrix3 m) {

            this.x = m.mat[6];
            this.y = m.mat[7];
        }

        public void SetScaleFromMatrix(Matrix3 m) {

            this.x = (float)Math.Sqrt(m.mat[0] * m.mat[0] + m.mat[1] * m.mat[1]);
            this.y = (float)Math.Sqrt(m.mat[3] * m.mat[3] + m.mat[4] * m.mat[4]);
        }

        public void ApplyMatrix(Matrix3 m) {

            var v = this.Clone();
            float w = 1 / (m.mat[2] * v.x + m.mat[5] * v.y + m.mat[8]);
            this.x = w * (m.mat[0] * v.x + m.mat[3] * v.y + m.mat[6]);
            this.y = w * (m.mat[1] * v.x + m.mat[4] * v.y + m.mat[7]);
        }

        public float Dot(Vector2 v) {
            return this.x * v.x + this.y * v.y;
        }

        public float Length() {
            return (float)Math.Sqrt(this.x * this.x + this.y * this.y);
        }

        public void Normalize() {

            float sizeSquared = this.x * this.x + this.y * this.y;

            if (sizeSquared < 1e-8) {
                return;
            }

            float scaleFactor = 1 / (float)Math.Sqrt(sizeSquared);
            this.x *= scaleFactor;
            this.y *= scaleFactor;
        }

        public void Invert() {

            this.x = -this.x;
            this.y = -this.y;
        }

        public float AngleBetween(Vector2 v) {

            var v1Norm = Vector2.Normalize(this);
            var v2Norm = Vector2.Normalize(v);

            return (float)Math.Acos(v1Norm.Dot(v2Norm));
        }

        public float AngleBetweenSigned(Vector2 v) {

            var v1Norm = Vector2.Normalize(this);
            var v2Norm = Vector2.Normalize(v);

            return (float)Math.Atan2(v2Norm.y, v2Norm.x) - (float)Math.Atan2(v1Norm.y, v1Norm.x);
        }

        public void Rotate(float angle) {

            float x = this.x;
            float y = this.y;
            this.x = (float)Math.Cos(angle) * x - (float)Math.Sin(angle) * y;
            this.y = (float)Math.Sin(angle) * x + (float)Math.Cos(angle) * y;
        }
    }
}