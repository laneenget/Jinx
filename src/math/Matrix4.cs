using System.Collections.Generic;

namespace Jinx.Src.Math {

    public class Matrix4 {

        public static readonly Matrix4 IDENTITY = new Matrix4();
        public List<float> mat = new List<float>(new float[16]);

        public static Matrix4 Multiply(Matrix4 m1, Matrix4 m2) {

            var m = new Matrix4();
            m.mat[0] = 0;
            m.mat[5] = 0;
            m.mat[10] = 0;
            m.mat[15] = 0;

            for (int r = 0; r < 4; r++) {
                for (int c = 0; c < 4; c++) {
                    for (int i = 0; i < 4; i++) {
                        m.mat[r * 4 + c] += m1.mat[r * 4 + i] * m2.mat[i * 4 + c];
                    }
                }
            }

            return m;
        }

        public static Matrix4 Copy(Matrix4 m) {

            var mat = new Matrix4();
            mat.Copy(m);
            return mat;
        }

        public static Matrix4 FromRowMajor(float n1, float n2, float n3,
            float n4, float n5, float n6, float n7, float n8,
            float n9, float n10, float n11, float n12, float n13,
            float n14, float n15, float n16) {
                
                var matrix = new Matrix4();
                matrix.SetRowMajor(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16);
                return matrix;
            }

        public static Matrix4 FromColumnMajor(float n1, float n2, float n3, float n4,
            float n5, float n6, float n7, float n8,
            float n9, float n10, float n11, float n12,
            float n13, float n14, float n15, float n16) {

                var matrix = new Matrix4();
                matrix.SetColumnMajor(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16);
                return matrix;
            }

        public static Matrix4 MakeTranslation(Vector3 v) {

            return Matrix4.FromRowMajor(
                1, 0, 0, v.x,
                0, 1, 0, v.y,
                0, 0, 1, v.z,
                0, 0, 0, 1
            );
        }

        public static Matrix4 MakeRotationX(float angle) {

            var matrix = new Matrix4();
            matrix.MakeRotationX(angle);
            return matrix;
        }

        public static Matrix4 MakeRotationY(float angle) {

            var matrix = new Matrix4();
            matrix.MakeRotationY(angle);
            return matrix;
        }

        public static Matrix4 MakeRotationZ(float angle) {

            var matrix = new Matrix4();
            matrix.MakeRotationZ(angle);
            return matrix;
        }

        public static Matrix4 MakeRotation(Vector3 axis, float angle) {

            var matrix = new Matrix4();
            matrix.MakeRotation(axis, angle);
            return matrix;
        }

        public static Matrix4 MakeScale(Vector3 scale) {
            
            var matrix = new Matrix4();
            matrix.MakeScale(scale);
            return matrix;
        }

        public static Matrix4 MakeTransform(Vector3 position) {
            return MakeTransform(position, Quaternion.IDENTITY, Vector3.ONE);
        }

        public static Matrix4 MakeTransform(Vector3 position, Quaternion rotation) {
            return MakeTransform(position, rotation, Vector3.ONE);
        }

        public static Matrix4 MakeTransform(Vector3 position, Quaternion rotation, Vector3 scale) {

            var matrix = new Matrix4();
            matrix.MakeTransform(position, rotation, scale);
            return matrix;
        }

        public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up) {

            var matrix = new Matrix4();
            matrix.LookAt(eye, target, up);
            return matrix;
        }

        public static Matrix4 MakeOrthographic(float left, float right, float bottom, float top, float near, float far) {

            var matrix = new Matrix4();
            matrix.MakeOrthographic(left, right, bottom, top, near, far);
            return matrix;
        }

        public static Matrix4 MakePerspective(float fov, float aspectRatio, float near, float far) {

            var matrix = new Matrix4();
            matrix.MakePerspective(fov, aspectRatio, near, far);
            return matrix;
        }

        public static Matrix4 MakeFrustrum(float left, float right, float bottom, float top, float near, float far) {

            var matrix = new Matrix4();
            matrix.MakeFrustrum(left, right, bottom, top, near, far);
            return matrix;
        }

        Matrix4() {

            this.mat.AddRange(new float[] {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            });
        }

        public void SetColumnMajor(float n1, float n2, float n3, float n4,
            float n5, float n6, float n7, float n8,
            float n9, float n10, float n11, float n12,
            float n13, float n14, float n15, float n16) {

                this.mat[0] = n1;
                this.mat[1] = n2;
                this.mat[2] = n3;
                this.mat[3] = n4;
                this.mat[4] = n5;
                this.mat[5] = n6;
                this.mat[6] = n7;
                this.mat[7] = n8;
                this.mat[8] = n9;
                this.mat[9] = n10;
                this.mat[10] = n11;
                this.mat[11] = n12;
                this.mat[12] = n13;
                this.mat[13] = n14;
                this.mat[14] = n15;
                this.mat[15] = n16;
        }

        public void SetRowMajor(float n1, float n2, float n3, float n4,
            float n5, float n6, float n7, float n8,
            float n9, float n10, float n11, float n12,
            float n13, float n14, float n15, float n16) {

                this.mat[0] = n1;
                this.mat[1] = n5;
                this.mat[2] = n9;
                this.mat[3] = n13;
                this.mat[4] = n2;
                this.mat[5] = n6;
                this.mat[6] = n10;
                this.mat[7] = n14;
                this.mat[8] = n3;
                this.mat[9] = n7;
                this.mat[10] = n11;
                this.mat[11] = n15;
                this.mat[12] = n4;
                this.mat[13] = n8;
                this.mat[14] = n12;
                this.mat[15] = n16;
        }

        public void Copy(Matrix4 m) {

            for (int i = 0; i < 16; i++) {
                this.mat[i] = m.mat[i];
            }
        }

        public Matrix4 Clone() {

            var matrix = new Matrix4();

            for (int i = 0; i < 16; i++) {
                matrix.mat[i] = this.mat[i];
            }

            return matrix;
        }

        public float Element(float row, float col) {

            return this.mat[col*4 + row];
        }

        public void Set(float value, float row, float col) {

            this.mat[(int)(col*4 + row)] = value;
        }

        public void Multiply(Matrix4 m) {

            var temp = Matrix4.Multiply(m, this);
            this.Copy(temp);
        }

        public void MakeTranslation(Vector3 v) {

            this.SetRowMajor(
                1, 0, 0, v.x,
                0, 1, 0, v.y,
                0, 0, 1, v.z,
                0, 0, 0, 1
            );
        }

        public void MakeRotationX(float angle) {

            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);

            this.SetRowMajor(
                1, 0, 0, 0,
                0, (float)cosTheta, -(float)sinTheta, 0,
                0, (float)sinTheta, (float)cosTheta, 0,
                0, 0, 0, 1
            );
        }

        public void MakeRotationY(float angle) {

            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);

            this.SetRowMajor(
                (float)cosTheta, 0, (float)sinTheta, 0,
                0, 1, 0, 0,
                -(float)sinTheta, 0, (float)cosTheta, 0,
                0, 0, 0, 1
            );
        }

        public void MakeRotationZ(float angle) {

            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);

            this.SetRowMajor(
                (float)cosTheta, -(float)sinTheta, 0, 0,
                (float)sinTheta, (float)cosTheta, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public void MakeScale(Vector3 scale) {

            this.SetRowMajor(
                scale.x, 0, 0, 0,
                0, scale.y, 0, 0,
                0, 0, scale.z, 0,
                0, 0, 0, 1
            );
        }

        public Vector3 GetTranslation() {

            return new Vector3(this.mat[12], this.mat[13], this.mat[14]);
        }

        public Quaternion GetRotation() {

            return Quaternion.MakeMatrix(this);
        }

        public Vector3 GetScale() {

            return new Vector3(
                (float)Math.sqrt(this.mat[0]*this.mat[0] + this.mat[1]*this.mat[1] + this.mat[2]*this.mat[2]),
                (float)Math.sqrt(this.mat[4]*this.mat[4] + this.mat[5]*this.mat[5] + this.mat[6]*this.mat[6]),
                (float)Math.sqrt(this.mat[8]*this.mat[8] + this.mat[9]*this.mat[9] + this.mat[10]*this.mat[10])
            );
        }

        public void MakeOrthographic(float left, float right, float bottom, float top, float near, float far) {

            this.SetRowMajor(
                2/(right-left), 0, 0, -(right+left)/(right-left),
                0, 2/(top-bottom), 0, -(top+bottom)/(top-bottom),
                0, 0, -2/(far-near), -(far+near)/(far-near),
                0, 0, 0, 1
            );
        }

        public void MakePerspective(float fov, float aspectRatio, float near, float far) {

            float yMax = near * (float)Math.Tan(fov * (float)Math.PI / 360);
            float xMax = yMax * aspectRatio;
            this.MakeFrustrum(-xMax, xMax, -yMax, yMax, near, far);
        }

        public void MakeFrustrum(float left, float right, float bottom, float top, float near, float far) {

            this.SetRowMajor(
                2*near/(right-left), 0, (right+left)/(right-left), 0,
                0, 2*near/(top-bottom), (top+bottom)/(top-bottom), 0,
                0, 0, -(far+near)/(far-near), -2*far*near/(far-near),
                0, 0, -1, 0
            );
        }

        public void MakeTransform(Vector3 position) {
            MakeTransform(position, Quaternion.IDENTITY, Vector3.ONE);
        }

        public void MakeTransform(Vector3 position, Quaternion rotation) {
            MakeTransform(position, rotation, Vector3.ONE);
        }

        public void MakeTransform(position=Vector3.ZERO, rotation=Quaternion.IDENTITY, scale=Vector3.ONE) {

            this.MakeTranslation(position);
            this.Multiply(rotation.GetMatrix());
            this.Multiply(Matrix4.MakeScale(scale));
        }

        public void LookAt(Vector3 eye, Vector3 target, up=Vector3.UP) {

            var z = Vector3.Subtract(eye, target);
            z.Normalize();

            var x = Vector3.Cross(up, z);
            x.Normalize();

            var y = Vector3.Cross(z, x);

            var rotation = Matrix4.FromRowMajor(
                x.x, y.x, z.x, 0,
                x.y, y.y, z.y, 0,
                x.z, y.z, z.z, 0,
                0, 0, 0, 1
            );

            var translation = Matrix4.MakeTranslation(eye);
            this.Copy(Matrix4.Multiply(rotation, translation));
        }

        public void MultiplyScalar(float x) {

            for (int i = 0; i < 16; i++) {
                this.mat[i] *= x;
            }
        }

        public float Determinant() {

            float determinant =
                this.mat[3] * this.mat[6] * this.mat[9] * this.mat[12]-
                this.mat[2] * this.mat[7] * this.mat[9] * this.mat[12]-
                this.mat[3] * this.mat[5] * this.mat[10] * this.mat[12]+
                this.mat[1] * this.mat[7] * this.mat[10] * this.mat[12]+
                this.mat[2] * this.mat[5] * this.mat[11] * this.mat[12]-
                this.mat[1] * this.mat[6] * this.mat[11] * this.mat[12]-
                this.mat[3] * this.mat[6] * this.mat[8] * this.mat[13]+
                this.mat[2] * this.mat[7] * this.mat[8] * this.mat[13]+
                this.mat[3] * this.mat[4] * this.mat[10] * this.mat[13]-
                this.mat[0] * this.mat[7] * this.mat[10] * this.mat[13]-
                this.mat[2] * this.mat[4] * this.mat[11] * this.mat[13]+
                this.mat[0] * this.mat[6] * this.mat[11] * this.mat[13]+
                this.mat[3] * this.mat[5] * this.mat[8] * this.mat[14]-
                this.mat[1] * this.mat[7] * this.mat[8] * this.mat[14]-
                this.mat[3] * this.mat[4] * this.mat[9] * this.mat[14]+
                this.mat[0] * this.mat[7] * this.mat[9] * this.mat[14]+
                this.mat[1] * this.mat[4] * this.mat[11] * this.mat[14]-
                this.mat[0] * this.mat[5] * this.mat[11] * this.mat[14]-
                this.mat[2] * this.mat[5] * this.mat[8] * this.mat[15]+
                this.mat[1] * this.mat[6] * this.mat[8] * this.mat[15]+
                this.mat[2] * this.mat[4] * this.mat[9] * this.mat[15]-
                this.mat[0] * this.mat[6] * this.mat[9] * this.mat[15]-
                this.mat[1] * this.mat[4] * this.mat[10] * this.mat[15]+
                this.mat[0] * this.mat[5] * this.mat[10] * this.mat[15];

            return determinant;
        }

        public Matrix4 Inverse() {

            float determinant = this.Determinant();
            if ((float)Math.Abs(determinant) < 1e-8) {
                return new Matrix4();
            }

            var inverse = new Matrix4();

            inverse.mat[0] = (this.mat[6]*this.mat[11]*this.mat[13] -
                this.mat[7]*this.mat[10]*this.mat[13] +
                this.mat[7]*this.mat[9]*this.mat[14] -
                this.mat[5]*this.mat[11]*this.mat[14] -
                this.mat[6]*this.mat[9]*this.mat[15] +
                this.mat[5]*this.mat[10]*this.mat[15])/determinant;

            inverse.mat[1] = (this.mat[3]*this.mat[10]*this.mat[13] -
                this.mat[2]*this.mat[11]*this.mat[13] -
                this.mat[3]*this.mat[9]*this.mat[14] +
                this.mat[1]*this.mat[11]*this.mat[14] +
                this.mat[2]*this.mat[9]*this.mat[15] -
                this.mat[1]*this.mat[10]*this.mat[15])/determinant;
            
            inverse.mat[2] = (this.mat[2]*this.mat[7]*this.mat[13] -
                this.mat[3]*this.mat[6]*this.mat[13] +
                this.mat[3]*this.mat[5]*this.mat[14] -
                this.mat[1]*this.mat[7]*this.mat[14] -
                this.mat[2]*this.mat[5]*this.mat[15] +
                this.mat[1]*this.mat[6]*this.mat[15])/determinant;
            
            inverse.mat[3] = (this.mat[3]*this.mat[6]*this.mat[9] -
                this.mat[2]*this.mat[7]*this.mat[9] -
                this.mat[3]*this.mat[5]*this.mat[10] +
                this.mat[1]*this.mat[7]*this.mat[10] +
                this.mat[2]*this.mat[5]*this.mat[11] -
                this.mat[1]*this.mat[6]*this.mat[11])/determinant;

            inverse.mat[4] = (this.mat[7]*this.mat[10]*this.mat[12] -
                this.mat[6]*this.mat[11]*this.mat[12] -
                this.mat[7]*this.mat[8]*this.mat[14] +
                this.mat[4]*this.mat[11]*this.mat[14] +
                this.mat[6]*this.mat[8]*this.mat[15] -
                this.mat[4]*this.mat[10]*this.mat[15])/determinant;
                
            inverse.mat[5] = (this.mat[2]*this.mat[11]*this.mat[12] -
                this.mat[3]*this.mat[10]*this.mat[12] +
                this.mat[3]*this.mat[8]*this.mat[14] -
                this.mat[0]*this.mat[11]*this.mat[14] -
                this.mat[2]*this.mat[8]*this.mat[15] +
                this.mat[0]*this.mat[10]*this.mat[15])/determinant;
                
            inverse.mat[6] = (this.mat[3]*this.mat[6]*this.mat[12] -
                this.mat[2]*this.mat[7]*this.mat[12] -
                this.mat[3]*this.mat[4]*this.mat[14] +
                this.mat[0]*this.mat[7]*this.mat[14] +
                this.mat[2]*this.mat[4]*this.mat[15] -
                this.mat[0]*this.mat[6]*this.mat[15])/determinant;
                
            inverse.mat[7] = (this.mat[2]*this.mat[7]*this.mat[8] -
                this.mat[3]*this.mat[6]*this.mat[8] +
                this.mat[3]*this.mat[4]*this.mat[10] -
                this.mat[0]*this.mat[7]*this.mat[10] -
                this.mat[2]*this.mat[4]*this.mat[11] +
                this.mat[0]*this.mat[6]*this.mat[11])/determinant;
                
            inverse.mat[8] = (this.mat[5]*this.mat[11]*this.mat[12] -
                this.mat[7]*this.mat[9]*this.mat[12] +
                this.mat[7]*this.mat[8]*this.mat[13] -
                this.mat[4]*this.mat[11]*this.mat[13] -
                this.mat[5]*this.mat[8]*this.mat[15] +
                this.mat[4]*this.mat[9]*this.mat[15])/determinant;
                
            inverse.mat[9] = (this.mat[3]*this.mat[9]*this.mat[12] -
                this.mat[1]*this.mat[11]*this.mat[12] -
                this.mat[3]*this.mat[8]*this.mat[13] +
                this.mat[0]*this.mat[11]*this.mat[13] +
                this.mat[1]*this.mat[8]*this.mat[15] -
                this.mat[0]*this.mat[9]*this.mat[15])/determinant;
                
            inverse.mat[10] = (this.mat[1]*this.mat[7]*this.mat[12] -
                this.mat[3]*this.mat[5]*this.mat[12] +
                this.mat[3]*this.mat[4]*this.mat[13] -
                this.mat[0]*this.mat[7]*this.mat[13] -
                this.mat[1]*this.mat[4]*this.mat[15] +
                this.mat[0]*this.mat[5]*this.mat[15])/determinant;
                
            inverse.mat[11] = (this.mat[3]*this.mat[5]*this.mat[8] -
                this.mat[1]*this.mat[7]*this.mat[8] -
                this.mat[3]*this.mat[4]*this.mat[9] +
                this.mat[0]*this.mat[7]*this.mat[9] +
                this.mat[1]*this.mat[4]*this.mat[11] -
                this.mat[0]*this.mat[5]*this.mat[11])/determinant;
                
            inverse.mat[12] = (this.mat[6]*this.mat[9]*this.mat[12] -
                this.mat[5]*this.mat[10]*this.mat[12] -
                this.mat[6]*this.mat[8]*this.mat[13] +
                this.mat[4]*this.mat[10]*this.mat[13] +
                this.mat[5]*this.mat[8]*this.mat[14] -
                this.mat[4]*this.mat[9]*this.mat[14])/determinant;
                
            inverse.mat[13] = (this.mat[1]*this.mat[10]*this.mat[12] -
                this.mat[2]*this.mat[9]*this.mat[12] +
                this.mat[2]*this.mat[8]*this.mat[13] -
                this.mat[0]*this.mat[10]*this.mat[13] -
                this.mat[1]*this.mat[8]*this.mat[14] +
                this.mat[0]*this.mat[9]*this.mat[14])/determinant;
            
            inverse.mat[14] = (this.mat[2]*this.mat[5]*this.mat[12] -
                this.mat[1]*this.mat[6]*this.mat[12] -
                this.mat[2]*this.mat[4]*this.mat[13] +
                this.mat[0]*this.mat[6]*this.mat[13] +
                this.mat[1]*this.mat[4]*this.mat[14] -
                this.mat[0]*this.mat[5]*this.mat[14])/determinant;
            
            inverse.mat[15] = (this.mat[1]*this.mat[6]*this.mat[8] -
                this.mat[2]*this.mat[5]*this.mat[8] +
                this.mat[2]*this.mat[4]*this.mat[9] -
                this.mat[0]*this.mat[6]*this.mat[9] -
                this.mat[1]*this.mat[4]*this.mat[10] +
                this.mat[0]*this.mat[5]*this.mat[10])/determinant;

            return inverse;
        }

        public void Invert(){

            var inverseMatrix = this.Inverse();
            this.Copy(inverseMatrix);
        }

        public Matrix4 Transpose() {

            return Matrix4.FromRowMajor(
                this.mat[0], this.mat[1], this.mat[2], this.mat[3],
                this.mat[4], this.mat[5], this.mat[6], this.mat[7],
                this.mat[8], this.mat[9], this.mat[10], this.mat[11],
                this.mat[12], this.mat[13], this.mat[14], this.mat[15]
            );
        }

        public void Decompose(Vector3 position, Quaternion rotation, Vector3 scale) {

            position.SetPositionFromMatrix(this);
            scale.SetScaleFromMatrix(this);

            var rotationMatrix = new Matrix4();
            rotationMatrix.mat[0] = this.mat[0] / scale.x;
            rotationMatrix.mat[1] = this.mat[1] / scale.x;
            rotationMatrix.mat[2] = this.mat[2] / scale.x;
            rotationMatrix.mat[3] = 0;

            rotationMatrix.mat[4] = this.mat[4] / scale.y;
            rotationMatrix.mat[5] = this.mat[5] / scale.y;
            rotationMatrix.mat[6] = this.mat[6] / scale.y;
            rotationMatrix.mat[7] = 0;

            rotationMatrix.mat[8] = this.mat[8] / scale.z;
            rotationMatrix.mat[9] = this.mat[9] / scale.z;
            rotationMatrix.mat[10] = this.mat[10] / scale.z;
            rotationMatrix.mat[11] = 0;

            rotationMatrix.mat[12] = 0;
            rotationMatrix.mat[13] = 0;
            rotationMatrix.mat[14] = 0;
            rotationMatrix.mat[15] = 1;

            rotation.SetMatrix(rotationMatrix);
        }
    }
}