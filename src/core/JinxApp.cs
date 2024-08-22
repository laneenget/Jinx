/* Framework for a graphics library. This class manages rendering,
input handling, and scene management. */

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Numerics;

public abstract class JinxApp : Window {

    private static JinxApp instance;

    // TODO: Fix instantiation
    // Ensure there is only one instance of this class at any time.
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

        this.SizeChanged += (sender, e) => Resize();
        this.MouseDown += OnMouseDown;
        this.MouseUp += OnMouseUp;
        this.MouseMove += OnMouseMove;
        this.MouseWheel += OnMouseWheel;
        this.KeyDown += OnKeyDown;
        this.KeyUp += OnKeyUp;
        this.GotFocus += OnFocusReceived;
        this.LostFocus += OnFocusLost;

        this.camera.SetOrthographicCamera(0, 1, 0, 1, 0.01, 1);
    }

    public void Start() {
        
        // Ensures all assets are loaded before starting the main loop
        if(this.waitForAssetLoadiing && !this.assetManager.allAssetsLoaded()) {
            Task.Delay(16).ContinueWith(_ => Start());
        } else {
            CreateScene();
            MainLoop();
        }
    }

    // Runs the core loop of the application, continuously updating and
    // rendering the scene
    private void MainLoop() {

        // Manage frame timing and ensure smooth updates and rendering
        if(this.runInBackground || !this.paused) {
            Update((DateTime.Now.Ticks - this.time) / 10_000_000.0);
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
            SimulateMouseEvent("mousedown", e);
        }
    }

    public void OnTouchMove(object sender, TouchEventArgs e) {

        e.Handled = true;
        var touchPoints = e.GetTouchPoints(this);

        if (touchPoints.Count == 1) {
            SimulateMouseEvent("mousemove", e)
        } else {
            SimulateWheelEvent(e)
        }
    }

    public void OnTouchEnd(object sender, TouchEventArgs e) {

        e.Handled = true;
        var touchPoints = e.GetTouchPoints(this);

        if (touchPoints.Count == 1) {
            SimulateMouseEvent("mouseup", e);
        }
    }

    public void OnFocusReceived(object sender, FocusEventArgs e) {
        Resume();
    }

    public void OnFocusLost(object sender, FocusEventArgs e) {
        Pause();
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

    public Vector2 GetNormalizedDeviceCoordinates(float mouseX, float mouseY) {
        return this.renderer.GetNormalizedDeviceCoordinates(mouseX, mouseY);
    }

    private void SimulateMouseEvent(string type, TouchEventArgs touchEvent) {

        if(this.previousTouches.length == 1) {

            Point currentPosition = touchEvent.GetTouchPoints(this)[0].Position;
            
            MouseButtonEventArgs mouseEventArgs = new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left){
                RoutedEvent = type switch {
                    "mousedown" => Mouse.MouseDownEvent,
                    "mousemove" => Mouse.MouseMoveEvent,
                    "mouseup" => Mouse.MouseUpEvent,
                    _ => throw new ArgumentException("Invalid event type", nameof(type)),
                },
                Source = touchEvent.Source,
                Handled = false
            };

            this.RaiseEvent(mouseEventArgs);

            previousTouches[0] = new Vector2((float)currentPosition.X, (float)currentPosition.Y);
        }
    }

    private void SimulateWheelEvent(TouchEventArgs touchEvent){
        
        if (previousTouches.Length > 1) {

            double previousDistance = DistanceBetweenPoints(previousTouches[0], previousTouches[1])
            double currentDistance = DistanceBetweenPoints(
                new Vector2((float)touchEvent.Touches[0].Position.X, (float)touchEvent.Touches[0].Position.Y),
                new Vector2((float)touchEvent.Touches[1].Position.X, (float)touchEvent.Touches[1].Position.Y)
            );
            double scaleFactor = 0;

            if (currentDistance > previousDistance) {
                scaleFactor = -currentDistance / previousDistance;
            } else if (currentDistance < previousDistance) {
                scaleFactor = previousDistance / currentDistance;
            }

            Point averagePosition = new Point(
                (touchEvent.Touches[0].Position.X + touchEvent.Touches[1].Position.X) / 2,
                (touchEvent.Touches[0].Position.Y + touchEvent.Touches[1].Position.Y) / 2
            );

            OnSimulateWheel(averagePosition, 50 * scaleFactor);
        }

        previousTouches = touchEvent.Touches.Select(t => new Vector2((float)t.Position.X, (float)t.Position.Y)).ToArray();
    }

    private void OnSimulateWheel(Point position, double deltaY) {

        double zoomFactor = 1.0 + deltaY / 1000.0;

        ScaleTransform scaleTransform = new ScaleTransform();

        scaleTransform.CenterX = position.X;
        scaleTransform.CenterY = position.Y;

        scaleTransform.ScaleX *= zoomFactor;
        scaleTransform.ScaleY *= zoomFactor;

        content.RenderTransform = scaleTransform
    }

    private double DistanceBetweenPoints(Vector2 point1, Vector2 point2) {
        return (float)Math.sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }

    public abstract void CreateScene(){}
    public abstract void Update(long deltaTime){}

    public void OnMouseDown(object sender, MouseEventArgs e){}
    public void OnMouseUp(object sender, MouseEventArgs e){}
    public void OnMouseMove(object sender, MouseEventArgs e){}
    public void OnMouseWheel(object sender, MouseEventArgs e){}
    public void OnKeyDown(object sender, KeyboardEventArgs e){}
    public void OnKeyUp(object sender, KeyboardEventArgs e){}
}