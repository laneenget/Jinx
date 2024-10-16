namespace Jinx.Src.Math {

    public class Matrix3{

        public static readonly Matrix3 IDENTITY = new Matrix3();
        public List<float> mat = new List<float>(new float[9]);

        public static Matrix3 Multiply(Matrix3 m1, Matrix3 m2) {

            var m = new Matrix3();

            m.mat[0] = m1.mat[0] * m2.mat[0] + m1.mat[1] * m2.mat[3] + m1.mat[2] * m2.mat[6];
            m.mat[1] = m1.mat[0] * m2.mat[1] + m1.mat[1] * m2.mat[4] + m1.mat[2] * m2.mat[7];
            m.mat[2] = m1.mat[0] * m2.mat[2] + m1.mat[1] * m2.mat[5] + m1.mat[2] * m2.mat[8];
            m.mat[3] = m1.mat[3] * m2.mat[0] + m1.mat[4] * m2.mat[3] + m1.mat[5] * m2.mat[6];
            m.mat[4] = m1.mat[3] * m2.mat[1] + m1.mat[4] * m2.mat[4] + m1.mat[5] * m2.mat[7];
            m.mat[5] = m1.mat[3] * m2.mat[2] + m1.mat[4] * m2.mat[5] + m1.mat[5] * m2.mat[8];
            m.mat[6] = m1.mat[6] * m2.mat[0] + m1.mat[7] * m2.mat[3] + m1.mat[8] * m2.mat[6];
            m.mat[7] = m1.mat[6] * m2.mat[1] + m1.mat[7] * m2.mat[4] + m1.mat[8] * m2.mat[7];
            m.mat[8] = m1.mat[6] * m2.mat[2] + m1.mat[7] * m2.mat[5] + m1.mat[8] * m2.mat[8];

            return m;
        }

        public static Matrix3 Copy(Matrix3 m) {

            var mat = new Matrix3();
            mat.Copy(m);
            return mat;
        }

        public static Matrix3 FromRowMajor(float n1, float n2, float n3
            float n4, float n5, float n6,
            float n7, float n8, float n9) {

            var mat = new Matrix3();
            mat.SetRowMajor(n1, n2, n3, n4, n5, n6, n7, n8, n9);
            return mat;
        }

        public static Matrix3 FromColumnMajor(float n1, float n2, float n3,
            float n4, float n5, float n6,
            float n7, float n8, float n9) {

            var mat = new Matrix3();
            mat.SetColumnMajor(n1, n2, n3, n4, n5, n6, n7, n8, n9);
            return mat;
        }

        public static Matrix3 MakeTranslation(Vector2 v) {

            return Matrix3.FromRowMajor(
                1, 0, v.x,
                0, 1, v.y,
                0, 0, 1
            );
        }

        public static Matrix3 MakeRotation(float angle) {

            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);

            return Matrix3.FromRowMajor(
                (float)cosTheta, -(float)sinTheta, 0,
                sinTheta, cosTheta, 0,
                0, 0, 1
            );
        }

        public static Matrix3 MakeScale(Vector2 scale) {

            return Matrix3.FromRowMajor(
                scale.x, 0, 0,
                0, scale.y, 0,
                0, 0, 1
            );
        }

        public static Matrix3 Compose(Vector2 position = default(Vector2), float rotation = 0, Vector2 scale = default(Vector2)) {

            if (position == default(Vector2)) position = Vector2.Zero;
            if (scale == default(Vector2)) scale = Vector2.One;

            Matrix3 matrix = new Matrix3();
            matrix.Compose(position, rotation, scale);
            return matrix;
        }

        Matrix3() {
            
            this.mat.AddRange(new float = [] {
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
            });
        }

        public void SetColumnMajor(float n1, float n2, float n3,
            float n4, float n5, float n6,
            float n7, float n8, float n9) {
                this.mat[0] = n1;
                this.mat[1] = n2;
                this.mat[2] = n3;
                this.mat[3] = n4;
                this.mat[4] = n5;
                this.mat[5] = n6;
                this.mat[6] = n7;
                this.mat[7] = n8;
                this.mat[8] = n9;
            }

        public void SetRowMajor(float n1, float n2, float n3,
            float n4, float n5, float n6,
            float n7, float n8, float n9) {
                this.mat[0] = n1;
                this.mat[1] = n4;
                this.mat[2] = n7;
                this.mat[3] = n2;
                this.mat[4] = n5;
                this.mat[5] = n8;
                this.mat[6] = n3;
                this.mat[7] = n6;
                this.mat[8] = n9;
            }

        public void Copy(Matrix3 m) {

            for(int i = 0; i < 9; i++) {
                this.mat[i] = m.mat[i];
            }
        }

        public float Element(float row, float col) {
            return this.mat[col*3 + row];
        }

        public void Set(float value, float row, float col) {
            this.mat[col*3 + row] = value;
        }

        public void Multiply(Matrix3 m) {
            var temp = Matrix3.Multiply(m, this);
            this.Copy(temp);
        }

        public void SetTranslation(Vector2 v) {

            this.SetRowMajor(
                1, 0, v.x,
                0, 1, v.y,
                0, 0, 1
            );
        }

        public void SetRotation(float angle) {
            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);

            this.SetRowMajor(
                cosTheta, -sinTheta, 0,
                sinTheta, cosTheta, 0,
                0, 0, 1
            );
        }

        public void SetScale(Vector2 scale) {

            this.SetRowMajor(
                scale.x, 0, 0,
                0, scale.y, 0,
                0, 0, 1
            );
        }

        public void MultiplyScalar(float x) {

            for(int i = 0; i < 9; i++) {
                this.mat[i] *= x;
            }
        }

        public Matrix3 Inverse() {

            var inverse = new Matrix3();

            var det = this.mat[0] * this.mat[4] * this.mat[8] + 
            this.mat[1] * this.mat[5] * this.mat[6] + 
            this.mat[2] * this.mat[3] * this.mat[7] - 
            this.mat[0] * this.mat[5] * this.mat[7] -
            this.mat[1] * this.mat[3] * this.mat[8] - 
            this.mat[2] * this.mat[4] * this.mat[6];

            this.mat[0] = (this.mat[4] * this.mat[8] - this.mat[5] * this.mat[7])/det;
            this.mat[1] = (this.mat[2] * this.mat[7] - this.mat[1] * this.mat[8])/det;
            this.mat[2] = (this.mat[1] * this.mat[5] - this.mat[2] * this.mat[4])/det;
            this.mat[3] = (this.mat[5] * this.mat[6] - this.mat[3] * this.mat[8])/det;
            this.mat[4] = (this.mat[0] * this.mat[8] - this.mat[2] * this.mat[6])/det;
            this.mat[5] = (this.mat[2] * this.mat[3] - this.mat[0] * this.mat[5])/det;
            this.mat[6] = (this.mat[3] * this.mat[7] - this.mat[4] * this.mat[6])/det;
            this.mat[7] = (this.mat[1] * this.mat[6] - this.mat[0] * this.mat[7])/det;
            this.mat[8] = (this.mat[0] * this.mat[4] - this.mat[1] * this.mat[3])/det;

            return inverse;
        }

        public void Invert() {

            var inverseMatrix = this.Inverse();
            this.Copy(inverseMatrix);
        }

        public Matrix3 Transpose() {

            return Matrix3.FromRowMajor(
                this.mat[0], this.mat[1], this.mat[2],
                this.mat[3], this.mat[4], this.mat[5],
                this.mat[6], this.mat[7], this.mat[8]
            );
        }

        public void Compose()
    }
}