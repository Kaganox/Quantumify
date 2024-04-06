using KaganoEngine;
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using KaganoEngine.Scenes;
using Newtonsoft.Json;
using Raylib_cs;
using System.Numerics;

public class Game : IDisposable
{
    public static Scene? currentScene;
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
    }

    public void JsonToNode(string json)
    {
        Dictionary<string, Type> types = new Dictionary<string, Type>();
        Dictionary<string, string>? nodeData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        Type typeData = types["type"];
        Node node = (Node)JsonConvert.DeserializeObject(json, typeData)!;
        Game.currentScene?.nodes.Add(node);
    }


    /// <summary>
    /// The game loop
    /// </summary>
    /// <param name="action"></param>
    public void Run(Scene? scene = null)
    {
        Logger.Success("Hello World!");

        
        Raylib.InitWindow(800, 480, "Hello World"); //TODO: create Window class with Title, Width, Height
        //Raylib.SetWindowIcon();
        currentScene = scene ?? new Scene();
        contentManager = new ContentManager();
        OnRun();
        Init();

        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(10, 10, 10); // Camera position
        camera.Target = new Vector3(0, 0, 0);      // Camera looking at point
        camera.Up = Vector3.UnitY;          // Camera up vector (rotation towards target)
        camera.FovY = 45.0f;                                // Camera field-of-view Y
        camera.Projection= CameraProjection.Perspective;             // Camera mode type

        while (!Raylib.WindowShouldClose())
        {
            bool is3D = dimension == Dimension._3D;
            Raylib.UpdateCamera(ref camera, CameraMode.Orbital);
            Update();
            AfterUpdate();
            _timer += Raylib.GetFrameTime();
            while (_timer >= _fixedTimeStep)
            {
                FixUpdate();
                _timer -= _fixedTimeStep;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            Raylib.BeginMode3D(camera);

            Raylib.DrawGrid(10, 1);
            Draw();
            Raylib.EndMode3D();
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
        Game.currentScene?.nodes.ForEach(node => node.Update());
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
    public virtual void FixUpdate()
    {
        Game.currentScene?.nodes.ForEach(node => node.FixUpdate());
    }


    /// <summary>
    /// Draws everyframe
    /// </summary>
    public virtual void Draw()
    {
        Raylib.DrawCube(new Vector3(0, 0, 0), 1, 1, 1, Color.Red);
        Game.currentScene?.nodes.ForEach(node => node.Draw());
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


    public enum Dimension
    {
        _2D,_3D
    }
}
