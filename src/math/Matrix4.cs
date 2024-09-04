using System.Collections.Generic;

namespace Jinx.Src.Math {

    public class Matrix4 {

        public static readonly IDENTITY = new Matrix4();
        public List<float> mat;

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

        
    }
}