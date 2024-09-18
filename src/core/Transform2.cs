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

            this.children = new List<Transform2>();

            this.position = new Vector2();
            this.rotation = 0;
            this.scale = new Vector2(1, 1);

            this.autoUpdateMatrix = true;
            this.matrix = new Matrix3();
            this.worldMatrix = new Matrix4();

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

            if (this.parent != null) {
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

            if (this.autoUpdateMatrix) {
                this.matrix.Compose(this.position, this.rotation, this.scale);
            }

            if (this.parent != null) {
                this.parent.UpdateWorldMatrix();
                this.worldMatrix.Copy(this.parent.worldMatrix);
                this.worldMatrix.Multiply(this.matrix);
            } else {
                this.worldMatrix.Copy(this.matrix);
            }
        }

        public void Add(Transform2 child) {

            this.children.Add(child);
            child.parent = this;
        }

        public bool Remove() {

            if (this.parent == null) {
                return false;
            } else {
                return this.parent.RemoveChild(this) != null;
            }
        }

        public Transform2 RemoveChild(Transform2 child) {

            int index = this.children.IndexOf(child);

            if (index == -1) {
                return null;
            } else {
                Transform2 removedElement = this.children[index];
                this.children.RemoveAt(index);
                removedElement.parent = null;
                return removedElement;
            }
        }

        public void Translate(Vector2 translation) {

            this.position += Vector2.Transform(translation, this.rotation);
        }

        public void TranslateX(float distance) {

            this.position += Vector2.Transform(new Vector2(distance, 0), this.rotation);
        }

        public void TranslateY(float distance) {

            this.position += Vector2.Transform(new Vector2(0, distance), this.rotation);
        }

        public void LookAt(Vector2 target, Vector2 lookVector = default(Vector2)) {

            if (lookVector == default(Vector2)) {
                lookVector = Vector2.UP;
            }

            this.UpdateWorldMatrix();

            var decomposition = this.worldMatrix.Decompose();
            var worldPosition = decomposition.Item1;
            var worldRotation = decomposition.Item2;

            var targetVector = Vector2.Subtract(target, worldPosition);

            if (targetVector.Length() > 0) {
                var worldLookVector = Vector2.Rotate(lookVector, worldRotation);
                this.rotation += worldLookVector.AngleBetweenSigned(targetVector);
            }
        }

        public bool Intersects(Transform2 other, IntersectionMode2 mode = IntersectionMode2.BOUNDING_CIRCLE) {

            if (mode == IntersectionMode2.BOUNDING_CIRCLE) {

                BoundingCircle thisCircle = new BoundingCircle();
                thisCircle.Copy(this.boundingCircle);
                thisCircle.Transform(this.position, this.scale);

                BoundingCircle otherCircle = new BoundingCircle();
                otherCircle.Copy(other.boundingCircle);
                otherCircle.Transform(other.position, other.scale);

                return thisCircle.Intersects(otherCircle);

            } else if (mode == IntersectionMode2.AXIS_ALIGNED_BOUNDING_BOX){
                
                BoundingBox2 thisBox = new BoundingBox2();
                thisBox.Copy(this.boundingBox);
                thisBox.Transform(this.position, this.rotation, this.scale);

                BoundingBox2 otherBox = new BoundingBox2();
                otherBox.Copy(other.boundingBox);
                otherBox.Transform(other.position, other.rotation, other.scale);

                return thisBox.Intersects(otherBox);

            } else {
                return false;
            }
        }
    }
}  