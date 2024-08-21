/* Represents a vector in 3D space. Includes methods for
Vector3 objects and included data types */

namespace Jinx.src.math{
    public class Vector3{

        public static readonly ZERO = new Vector3(0, 0, 0);
        public static readonly ONE = new Vector3(1, 1, 1);
        public static readonly UP = new Vector3(0, 1, 0);
        public static readonly DOWN = new Vector3(0, -1, 0);
        public static readonly LEFT = new Vector3(-1, 0, 0);
        public static readonly RIGHT = new Vector3(1, 0, 0);
        public static readonly FORWARD = new Vector3(0, 0, -1);
        public static readonly BACK = new Vector3(0, 0, 1);
        public static readonly X_AXIS = Vector3.RIGHT;
        public static readonly Y_AXIS = Vector3.UP;
        public static readonly Z_AXIS = Vector3.FORWARD;

        public static Vector3 Copy(Vector3 v) {
            return new Vector3(v.x, v.y, v.z);
        }

        public static Vector3 Inverse(Vector3 v) {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 Subtract(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 Multiply(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vector3 Divide(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        public static float Dot(Vector3 v1, Vector3 v2) {
            return v1.x*v2.x + v1.y*v2.y + v1.z*v2.z;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2) {
            return new Vector3(
                v1.y * v2.z - v1.z * v2.y,
                v1.z * v2.x - v1.x * v2.z,
                v1.x * v2.y - v1.y * v2.x
            );
        }

        public static Vector3 MultiplyScalar(Vector3 v, float n) {
            return new Vector3(v.x * n, v.y * n, v.z * n);
        }

        public static Vector3 DivideScalar(Vector3 v, float n) {
            return new Vector3(v.x / n, v.y / n, v.z / n);
        }

        public static Vector3 Normalize(Vector3 v) {
            
            const sizeSquared = v.x * v.x + v.y * v.y + v.z * v.z;

            if (sizeSquared < 1e-8) {
                return new Vector3();
            }

            float scaleFactor = 1 / Math.Sqrt(sizeSquared);
            return new Vector3(v.x * scaleFactor, v.y * scaleFactor, v.z * scaleFactor);
        }

        public static float AngleBetween(Vector3 v1, Vector3 v2) {
            return v1.AngleBetween(v2);
        }

        public static float DistanceBetween(Vector3 v1, Vector3 v2) {
            return v1.DistanceTo(v2);
        }

        public float x;
        public float y;
        public float z;

        public Vector3(float x = 0, float y = 0, float z = 0) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Copy(Vector3 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public Vector3 Clone() {
            return new Vector3(this.x, this.y, this.z);
        }

        public bool Equals(Vector3 v) {
            return this.x == v.x && this.y == v.y && this.z == v.z;
        }

        public void Add(Vector3 v) {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
        }

        public void Subtract(Vector3 v) {
            this.x -= v.x;
            this.y -= v.y;
            this.z -= v.z;
        }

        public void Multiply(Vector3 v) {
            this.x *= v.x;
            this.y *= v.y;
            this.z *= v.z;
        }

        public void Divide(Vector3 v) {
            this.x /= v.x;
            this.y /= v.y;
            this.z /= v.z;
        }

        public float Dot(Vector3 v) {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        public Vector3 Cross(Vector3 v) {
            return new Vector3(
                this.y * v.z - this.z * v.y,
                this.z * v.x - this.x * v.z,
                this.x * v.y - this.y * v.x
            );
        }

        public void MultiplyScalar(float n) {
            this.x *= n;
            this.y *= n;
            this.z *= n;
        }

        public void DivideScalar(float n) {
            this.x /= n;
            this.y /= n;
            this.z /= n;
        }

        public float Length() {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public void Normalize() {
            const sizeSquared = this.x * this.x + this.y * this.y + this.z * this.z;

            if (sizeSquared < 1e-8) {
                return;
            }

            const scaleFactor = 1 / Math.Sqrt(sizeSquared);
            this.x *= scaleFactor;
            this.y *= scaleFactor;
            this.z *= scaleFactor;
        }

        public void Invert() {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }

        public void ApplyMatrix(Matrix4 m) {
            var v = this.Clone();
            float w = 1 / (m.mat[3] * v.x + m.mat[7] * v.y + m.mat[11] * v.z + m.mat[15]);
            this.x = w * (m.mat[0] * v.x + m.mat[4] * v.y + m.mat[8] * v.z + m.mat[12]);
            this.y = w * (m.mat[1]*v.x + m.mat[5]*v.y + m.mat[9]*v.z + m.mat[13]);
            this.z = w * (m.mat[2]*v.x + m.mat[6]*v.y + m.mat[10]*v.z + m.mat[14]);
        }

        public void ApplyMatrixAsNormal(Matrix4 m) {
            const v = this.Clone();
            const w = 1 / (m.mat[3]*v.x + m.mat[7]*v.y + m.mat[11]*v.z);
            this.x = w * (m.mat[0]*v.x + m.mat[4]*v.y + m.mat[8]*v.z);
            this.y = w * (m.mat[1]*v.x + m.mat[5]*v.y + m.mat[9]*v.z);
            this.z = w * (m.mat[2]*v.x + m.mat[6]*v.y + m.mat[10]*v.z);
        }

        public void Rotate(Quaternion q) {
            this.Copy(q.Rotate(this));
        }

        public float AngleBetween(Vector3 v) {
            const v1Norm = Vector3.Normalize(this);
            const v2Norm = Vector3.Normalize(v);

            return Math.Acos(v1Norm.dot(v2Norm));
        }

        public float DistanceTo(Vector3 v) {
            return Math.Sqrt(
                (this.x - v.x) * (this.x - v.x) +
                (this.y - v.y) * (this.y - v.y) +
                (this.z - v.z) * (this.z - v.z)
            );
        }

        public void SetPositionFromMatrix(Matrix4 m) {
            this.x = m.mat[12];
            this.y = m.mat[13];
            this.z = m.mat[14];
        }

        public void SetScaleFromMatrix(Matrix4 m) {
            this.x = Math.Sqrt(m.mat[0] * m.mat[0] + m.mat[1] * m.mat[1] + m.mat[2] * m.mat[2]);
            this.y = Math.Sqrt(m.mat[4] * m.mat[4] + m.mat[5] * m.mat[5] + m.mat[6] * m.mat[6]);
            this.z = Math.Sqrt(m.mat[8] * m.mat[8] + m.mat[9] * m.mat[9] + m.mat[10] * m.mat[10]);
        }
    }
}