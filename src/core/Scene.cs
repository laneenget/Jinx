namespace Jinx.Src.Core {

    public class Scene {

        public Transform3 root3d;
        public Transform2 root2d;
        private LightManager lightManager;

        public Scene() {

            this.root3d = new Transform3();
            this.root2d = new Transform2();
            this.lightManager = new LightManager();
        }

        public void Draw(Camera camera) {

            camera.ComputeWorldTransform();

            this.ComputeWorldTransforms();

            this.lightManager.Clear();
            this.root3d.SetLights(this.lightManager);
            this.lightManager.UpdateLights();

            foreach (Transform3 elem in this.root3d.children) {
                elem.Draw(this.root3d, camera, this.lightManager);
            }

            foreach (Transform2 elem in this.root2d.children) {
                elem.Draw(this.root2d);
            }
        }

        public void Add(object child) {

            if (child is Transform3 transform3) {
                this.root3d.Add(transform3);
            } else if (child is Transform2 transform2) {
                this.root2d.Add(transform2);
            }
        }

        public void ComputeWorldTransforms() {

            foreach (Transform3 elem in this.root3d.children) {
                elem.ComputeWorldTransform();
            }

            foreach (Transform2 elem in this.root2d.children) {
                elem.ComputeWorldTransform();
            }
        }
    }
}