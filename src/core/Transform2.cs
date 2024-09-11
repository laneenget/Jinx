namespace Jinx.Src.Core {

    public enum IntersectionMode2 {

        BOUNDING_CIRCLE,
        AXIS_ALIGNED_BOUNDING_BOX
    }

    public class Transform2 {

        public List<Transform2> children;
        public Transform2 parent;

        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public float layer;

        public bool autoUpdateMatrix;
        public Matrix3 matrix;
        public Matrix4 worldMatrix;

        public bool visible;

        public BoundingBox2 boundingBox;
        public BoundingCircle boundingCircle;

        public Transform2() {

            this.children = new List<Transform2>;

            this.position = new Vector2();
            this.rotation = 0;
            this.scale = new Vector2(1, 1);

            this.autoUpdateMatrix = true;
            this.matrix = new Matrix3();
            this.worldMatrix = new Matrix3();

            this.layer = 0;

            this.visible = true;
            
            this.parent = null;

            this.boundingBox = new BoundingBox2();
            this.boundingCircle = new BoundingCircle();
        }

        public void Draw(Transform2 parent) {

            if (!this.visible) {
                return;
            }

            foreach(Transform2 elem in this.children) {
                elem.Draw(this);
            }
        }

        public void TraverseSceneGraph() {

            if (this.autoUpdateMatrix) {
                this.matrix.Compose(this.position, this.rotation, this.scale);
            }

            if (this.parent) {
                this.worldMatrix.Copy(this.parent.worldMatrix);
                this.worldMatrix.Multiply(this.matrix);
            } else {
                this.worldMatrix.Copy(this.matrix);
            }

            foreach(Transform2 elem in this.children) {
                elem.TraverseSceneGraph();
            }
        }

        public void UpdateWorldMatrix() {

            if (this.AutoUpdateMatrix) {

                this.matrix.Compose(this.position, this.rotation, this.scale);
            }

            if (this.parent) {
                this.parent.UpdateWorldMatrix();
                this.worldMatrix.Copy(this.parent.WorldMatrix);
                this.worldMatrix.Multiply(this.matrix);
            } else {
                this.worldMatrix.Copy(this.matrix);
            }
        }

        // Don't forget that you will have to refactor all of the code
        // you have written so far because TypeScript doesn't require
        // an explicit return type.
    }
}