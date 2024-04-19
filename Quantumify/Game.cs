using Quantumify.Manager;
using Quantumify.Scenes;
using Raylib_cs;

namespace Quantumify;

public class Game : IDisposable
{
    public static Game Instance { get; private set; }
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
        Instance = this;
    }

    /// <summary>
    /// The game loop
    /// </summary>
    /// <param name="action"></param>
    public void Run(Scene? scene = null)
    {
        Logger.SetupRaylibLogger();
        Logger.Info("Hello World!");

        SceneCamera.Init();
        Raylib.InitWindow(800, 480, "Hello World"); //TODO: create Window class with Title, Width, Height
        
        contentManager = new ContentManager();
        
        OnRun();
        SceneManager.ActiveScene = scene;
        Init();

        while (!Raylib.WindowShouldClose())
        {

            Update();
            AfterUpdate();
            _timer += Raylib.GetFrameTime();
            while (_timer >= _fixedTimeStep)
            {
                FixedUpdate();
                _timer -= _fixedTimeStep;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.RayWhite);
            Draw();

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
        SceneManager.AfterUpdate();
    }

    /// <summary>
    /// Runs every _fixedTimeStep default 60fps (1/60s)
    /// </summary>
    public virtual void FixedUpdate()
    {
        SceneManager.FixedUpdate();
    }

    /// <summary>
    /// Draws everyframe
    /// </summary>
    public virtual void Draw()
    {
        SceneManager.Draw();
        //aylib.DrawText("Hello, world!", 12, 12, 20, Raylib_cs.Color.Black);
    }
    
    /// <summary>
    /// Runs by closing the game
    /// </summary>
    public virtual void OnClose()
    {
        Logger.Info("Goodbye, World!");
    }

    /// <summary>
    /// Will remove resources (textures, fonts, etc.) from your memory
    /// </summary>
    public void Dispose()
    {
        Raylib.CloseWindow();
    }
}