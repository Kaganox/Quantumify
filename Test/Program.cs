
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using KaganoEngine.Components;
using Raylib_cs;
using System.Numerics;
using Test;
using KaganoEngine;
using KaganoEngine.Config;

public partial class Program : Game
{
    public SaveFile saveFile;
    public static Program program;
    public static Enemy enemy = null;
    public static void Main(string[] args)
    {
        new Program();
    }

    public Program() : base(Dimension._3D)
    {
        program = this;
        Run();
    }

    public override void Init()
    {

        Console.WriteLine("Test");
        base.Init();
        //Texture2D texture = contentManager.Load<Texture2D>("content/player.png");
        Console.WriteLine(Environment.CurrentDirectory + "/content/save.dat");
        saveFile = new(Environment.CurrentDirectory + "/content/save.dat");
        SaveFile.RegisterTyp<Player>();

        if (saveFile.Exists())
        {
            saveFile.Read();
        }
        else
        {

            enemy = new()
            {
                Position = new Vector3(0, 0, 0),
            };
            Player player = new()
            {
                Position = new Vector3(400, 240, 0),
            };

            CameraNode camera = new();
            player.AddChild(camera);
            camera.SetActiveCamera();

        }


        Node3D node = new()
        {
            Position = new Vector3(0, 1, 0),
            Texture = contentManager.Load<Texture2D>("new.png"),
            Model = contentManager.Load<Model>("model.glb"),
            Size = new Vector3(1, 1, 1),
            Scale = new Vector3(1f, 1f, 1f),
            Rotation = new Vector3(0, 0f, 0),
            RotationAxis = 1,
            Color = Color.White,
        };
        node.SetMaterialTexture();
    }

    public override void OnClose()
    {
        saveFile.Write();
    }
}
