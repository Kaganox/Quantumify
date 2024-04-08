using KaganoEngine;
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using KaganoEngine.Scenes;
using Newtonsoft.Json;
using Raylib_cs;
using System.Numerics;

public class Game : IDisposable
{
    public static Game game;
    public ContentManager contentManager { get; private set; }
    private float _fixedTimeStep; //TODO: create Settings
    private double _timer;
    public Dimension dimension { get; private set; }
    /// <summary>
    /// Starts the game
    /// </summary>
    public Game(Dimension dimension = Dimension._2D)
    {
        this._fixedTimeStep = 1.0f / 60.0f;
        this.dimension = dimension;
        game = this;
    }


    /// <summary>
    /// The game loop
    /// </summary>
    /// <param name="action"></param>
    public void Run(Scene scene2d = null,Scene scene3d = null)
    {
        Logger.Success("Hello World!");

        
        Raylib.InitWindow(800, 480, "Hello World"); //TODO: create Window class with Title, Width, Height
        //Raylib.SetWindowIcon();
        SceneManager.activeScene = scene2d ?? new Scene(Dimension._2D);

        if (dimension == Dimension._3D)
        {
            SceneManager.activeScene = scene3d ?? new Scene(Dimension._3D);
        }
        contentManager = new ContentManager();
        OnRun();

        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(10, 10, 10); // Camera position
        camera.Target = new Vector3(0, 0, 0);      // Camera looking at point
        camera.Up = Vector3.UnitY;          // Camera up vector (rotation towards target)
        camera.FovY = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera mode type
        Camera.camera3D = camera;

        Init();

        while (!Raylib.WindowShouldClose())
        {
            bool is3D = dimension == Dimension._3D;
            if (is3D)
            {
                Raylib.UpdateCamera(ref Camera.camera3D, CameraMode.Orbital);
            }

            Update();
            AfterUpdate();
            _timer += Raylib.GetFrameTime();
            while (_timer >= _fixedTimeStep)
            {
                FixedUpdate();
                _timer -= _fixedTimeStep;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            Draw();
            //Raylib.DrawGrid(10, 1);

            Raylib.EndDrawing();

        }
        OnClose();
    }


    public virtual void OnRun()
    {

    }


    /// <summary>
    /// Is callen for game loop, here will be loaded files (Textures, Fonts, etc.), create Nodes and objects
    /// </summary>
    public virtual void Init()
    {
        
    }


    /// <summary>
    /// Runs every frame
    /// </summary>
    public virtual void Update()
    {
        SceneManager.Update();
    }


    /// <summary>
    /// Runs after Update (for redendering etc)
    /// </summary>
    public virtual void AfterUpdate()
    {
    }

    /// <summary>
    /// Runs every _fixedTimeStep default 60fps (1/60s)
    /// </summary>
    public virtual void FixedUpdate()
    {
        SceneManager.activeScene?.nodes.ForEach(node => node.FixedUpdate());
        if (SceneManager.activeScene != null)
        {
            SceneManager.activeScene?.nodes.ForEach(node => node.FixedUpdate());
        }
    }


    /// <summary>
    /// Draws everyframe
    /// </summary>
    public virtual void Draw()
    {
        SceneManager.Draw();
        Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
    }


    /// <summary>
    /// Runs by closing the game
    /// </summary>
    public virtual void OnClose()
    {
        Console.WriteLine("Goodbye, World!");
    }


    /// <summary>
    /// Will remove resources (textures, fonts, etc.) from your memory
    /// </summary>
    public void Dispose()
    {
        Raylib.CloseWindow();
    }



}
