using Renderer = "./Renderer";
using Camera = "./Camera";
using Scene = "./Scene";
using Vector2 = "../math/Vector2"
using AssetManager = "../loaders/AssetManager"

public abstract class JinxApp {

    private static JinxApp instance;

    public static JinxApp GetInstance() {

        if (instance == null) [
            instance = new JinxApp();
        ]
        return instance;
    }

    public Renderer renderer;
    public Camera camera;
    public Scene scene;
    public AssetManager assetManager;
    public bool runInBackground;
    public bool waitForAssetLoading;

    private long time;
    private bool paused;
    private Vector2[] previousTouches;

    public JinxApp() {

        this.time = DateTime.Now.Ticks;
        this.paused = false;
        this.previousTouches = new Vector2[0];

        this.camera = new Camera();
        this.scene = new Scene();
        this.renderer = new Renderer();
        this.assetManager = new AssetManager();
        this.runInBackground = false;
        this.waitForAssetLoading = true;

        this.Resize += (sender, e) => this.Resize();
        this.MouseDown += (sender, e) => this.OneMouseDown(e);
        this.MouseUp += (sender, e) => this.OnMouseUp(e);
        this.MouseMove += (sender, e) => this.OnMouseMove(e);
        this.MouseWheel += (sender, e) => this.OnMouseWheel(e);
        this.KeyDown += (sender, e) => this.OnKeyDown(e);
        this.KeyUp += (sender, e) => this.OnKeyUp(e);
        this.GotFocus += (sender, e) => this.OnKeyUp(e);
        this.GotFocus += (sender, e) => this.OnFocusReceived(e);
        this.LostFocus += (sender, e) => this.OnFocusLost(e);
        this.MouseClick += (sender, e) => this.OnMouseClick(e);

        this.camera.setOrthographicCamera(0, 1, 0, 1, 0.01, 1);
    }

    public void Start() {
        
        if(this.waitForAssetLoadiing && !this.assetManager.allAssetsLoaded()) {
            Task.Delay(16).ContinueWith(_ => Start());
        } else {
            this.CreateScene();
            this.MainLoop();
        }
    }

    private void MainLoop() {

        if(this.runInBackground || !this.paused) {
            this.Update((DateTime.Now.Ticks - this.time) / 10_000_000.0);
            this.time = DateTime.Now.Ticks;
        }

        this.renderer.Render(this.scene, this.camera);

        Task.Delay(16).ContinueWith(_ => MainLoop());
    }

    public void Resize() {
        this.renderer.Resize((int)this.ActualWidth, (int)this.ActualHeight, this.camera.GetAspectRatio())
    }

    public void OnTouchStart(object sender, TouchEventArgs e) {

        e.Handled = true;
        var touchPoints = e.GetTouchPoints(this);

        if (touchPoints.Count == 1) {
            SimulateMouseEvent(MouseButton.Left, e);
        }
    }

    public void OnTouchMove(object sender, TouchEventArgs e) {

        e.Handled = true;
        var touchPoints = e.GetTouchPoints(this);

        if (touchPoints.Count == 1) {
            SimulateMouseEvent(MouseButton.Left, e)
        } else {
            SimulateWheelEvent(e)
        }
    }

    public void OnTouchEnd(object sender, TouchEventArgs e) {

        e.Handled = true;
        var touchPoints = e.GetTouchPoints(this);

        if (touchPoints.Count == 1) {
            SimulateMouseEvent(MouseButton.Left, e);
        }
    }

    public void OnFocusReceived(object sender, FocusEventArgs e) {
        this.resume();
    }

    public void OnFocusLost(object sender, FocusEventArgs e) {
        this.pause();
    }

    public void Pause() {
        this.paused = true;
    }

    public void Resume() {
        this.time = DateTime.Now.Ticks;
        this.paused = false;
    }

    public bool IsPaused() {
        return this.paused;
    }


}