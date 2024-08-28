namespace Jinx.Src.Core {

    public class Camera : Transform3 {

        protected float aspectRatio;
        protected float fov;
        protected float near;
        protected float far;
        protected float left;
        protected float right;

        public Matrix4 projectionMatrix;
        public Matrix4 viewMatrix;
        public bool projectionMatrixDirty;

        public Camera() : base() {
            
            this.fov = 0;
            this.aspectRatio = 0;
            this.near = 0;
            this.far = 0;
            this.left = 0;
            this.right = 0;
            this.projectionMatrixDirty = true;

            this.projectionMatrix = new Matrix4();
            this.viewMatrix = new Matrix4();
        }

        public void SetPerspectiveCamera(float fov, float aspectRatio, float near, float far) {

            this.fov = fov;
            this.aspectRatio = aspectRatio;
            this.near = near;
            this.far = far;
            this.projectionMatrixDirty = true;

            this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, near, far);
        }

        public override void ComputeWorldTransform() {
            base.ComputeWorldTransform();
            this.viewMatrix = Matrix4.Invert(this.worldMatrix);
        }

        public float GetAspectRatio() {
            return this.aspectRatio;
        }

        public float GetNear() {
            return this.near;
        }

        public float GetFar() {
            return this.far;
        }

        public float GetLeft() {
            return this.left;
        }

        public float GetRight() {
            return this.right;
        }
    }
}