using System;

namespace Jinx.Src.Math {

    public class Quaternion {

        public static readonly Quaternion IDENTITY = new Quaternion();

        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x = 0, float y = 0, float z = 0, float w = 1) {
            
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion Multiply(Quaternion q1, Quaternion q2) {
            
            var dest = new Quaternion();

            dest.w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            dest.x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            dest.y = q1.w * q2.y + q1.y * q2.w + q1.z * q2.x - q1.x * q2.z;
            dest.z = q1.w * q2.z + q1.z * q2.w + q1.x * q2.y - q1.y * q2.x;

            return dest;
        }

        public static Quaternion Normalize(Quaternion q) {
            
            var dest = q.Clone();
            dest.Normalize();
            return dest;
        }

        public static Quaternion Inverse(Quaternion q) {

            var dest = q.Clone();
            dest.Invert();
            return dest;
        }

        public static Quaternion MakeRotationX(float angle) {

            var dest = new Quaternion();
            dest.SetRotationX(angle);
            return dest;
        }

        public static Quaternion MakeRotationY(float angle) {

            var dest = new Quaternion();
            dest.SetRotationY(angle);
            return dest;
        }

        public static Quaternion MakeRotationZ(float angle) {
            
            var dest = new Quaternion();
            dest.SetRotationZ(angle);
            return dest;
        }

        public static Quaternion MakeAxisAngle(Vector3 axis, float angle) {

            var dest = new Quaternion();
            dest.SetAxisAngle(axis, angle);
            return dest;
        }

        public static Quaternion MakeEulerAngles(float yaw, float pitch, float roll) {

            var dest = new Quaternion();
            dest.SetEulerAngles(yaw, pitch, roll);
            return dest;
        }

        public static Quaternion MakeMatrix(Matrix4 matrix) {

            var dest = new Quaternion();
            dest.SetMatrix(matrix);
            return dest;
        }


        public void Set(float x, float y, float z, float w) {

            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void SetRotationX(float angle) {

            this.w = (float)Math.Cos(angle / 2);
            this.x = (float)Math.Sin(angle / 2);
            this.y = 0;
            this.z = 0;
        }

        public void SetRotationY(float angle) {

            this.w = (float)Math.Cos(angle / 2);
            this.x = 0;
            this.y = (float)Math.Sin(angle / 2);
            this.z = 0;
        }

        public void SetRotationZ(float angle) {

            this.w = (float)Math.Cos(angle / 2);
            this.x = 0;
            this.y = 0;
            this.z = (float)Math.Sin(angle / 2);
        }

        public void SetAxisAngle(Vector3 axis, float angle) {

            float sinAngle = (float)Math.Sin(angle / 2);

            this.w = (float)Math.Cos(angle / 2);
            this.x = sinAngle * axis.x;
            this.y = sinAngle * axis.y;
            this.z = sinAngle * axis.z;
        }

        public void SetEulerAngles(float yaw, float pitch, float roll) {

            float cosPitch = (float)Math.Cos(pitch / 2);
            float sinPitch = (float)Math.Sin(pitch / 2);

            float cosYaw = (float)Math.Cos(yaw / 2);
            float sinYaw = (float)Math.Sin(yaw / 2);

            float cosRoll = (float)Math.Cos(-roll / 2);
            float sinRoll = (float)Math.Sin(-roll / 2);

            this.x = sinPitch * cosYaw * cosRoll + cosPitch * sinYaw * sinRoll;
            this.y = cosPitch * sinYaw * cosRoll - sinPitch * cosYaw * sinRoll;
            this.z = cosPitch * cosYaw * sinRoll + sinPitch * sinYaw * cosRoll;
            this.w = cosPitch * cosYaw * cosRoll - sinPitch * sinYaw * sinRoll;
        }

        public void SetMatrix(Matrix4 matrix) {

            this.w = (float)Math.Sqrt(1 + matrix.mat[0] + matrix.mat[5] + matrix.mat[10]) / 2;
            this.x = (matrix.mat[6] - matrix.mat[9]) / (4 * this.w);
            this.y = (matrix.mat[8] - matrix.mat[2]) / (4 * this.w);
            this.z = (matrix.mat[1] - matrix.mat[4]) / (4 * this.w);
        }

        public void Copy(Quaternion q) {

            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
            this.w = q.w;
        }

        public Quaternion Clone() {
            return new Quaternion(this.x, this.y, this.z, this.w);
        }

        public void Multiply(Quaternion q) {
            this.Copy(Quaternion.Multiply(q, this));
        }

        public void Normalize() {

            const normalizeFactor = 1 / (float)Math.Sqrt(this.x * this.x + this.y * this.y + 
                this.z * this.z + this.w * this.w);

            this.x *= normalizeFactor;
            this.y *= normalizeFactor;
            this.z *= normalizeFactor;
            this.w *= normalizeFactor;
        }

        public Vector3 Rotate(Vector3 v) {

            var u = new Vector3(this.x, this.y, this.z);

            var result = Vector3.MultiplyScalar(u, 2 * u.Dot(v));

            result.Add(Vector3.MultiplyScalar(v, this.w * this.w - u.Dot(u)));

            var crossUV = u.Cross(v);
            crossUV.MultiplyScalar(2 * this.w);
            result.Add(crossUV);

            return result;
        }

        public void Invert() {
            
            float norm = 1 / (this.x * this.x + this.y * this.y + this.z * this.z);
            this.x *= -norm;
            this.y *= -norm;
            this.z *= -norm;
            this.w *= -norm;
        }

        public Quaternion Inverse() {

            return Quaternion.Inverse(this);
        }

        public Matrix4 GetMatrix() {

            float sqw = this.w * this.w;
            float sqx = this.x * this.x;
            float sqy = this.y * this.y;
            float sqz = this.z * this.z;

            float invs = 1 / (sqx + sqy + sqz + sqw);

            float tmp1 = this.x * this.y;
            float tmp2 = this.z * this.w;
            float tmp3 = this.x * this.z;
            float tmp4 = this.y * this.w;
            float tmp5 = this.y * this.z;
            float tmp6 = this.x * this.w;

            return Matrix4.FromRowMajor(
                ( sqx - sqy - sqz + sqw) * invs, 2 * (tmp1 - tmp2) * invs, 2 * (tmp3 + tmp4) * invs, 0,
                2 * (tmp1 + tmp2) * invs, (-sqx + sqy - sqz + sqw) * invs, 2 * (tmp5 - tmp6) * invs, 0,
                2 * (tmp3 - tmp4) * invs, 2 * (tmp5 + tmp6) * invs, (-sqx - sqy + sqz + sqw), 0,
                0, 0, 0, 1   
            );
        }
    }
}