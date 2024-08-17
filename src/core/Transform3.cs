

public class Transform3 {

    public Transform3[] children;

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public bool visible;

    public bool autoUpdateMatrix;
    public Matrix4 matrix;
    public Matrix4 worldMatrix;

    public Vector3 worldPosition;
    public Quaternion worldRotation;
    public Vector3 worldScale;

    public Transform3 parent = null;

    public Transform3() {

        this.children = [];
        this.position = new Vector3();
        this.rotation = new Quaternion();
        this.scale = new Vector3(1, 1, 1);
        this.visible = true;

        this.autoUpdateMatrix = true;
        this.matrix = new Matrix4();
        this.worldMatrix = new Matrix4();

        this.worldPosition = new Vector3();
        this.worldRotation = new Quaternion();
        this.worldScale = new Vector3();

        this.parent = null;
    }

    public void draw(Transform3 parent, Camera camera, LightManager lightManager) {

        if(!this.visble) {
            return;
        }

    }
}