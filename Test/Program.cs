
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using KaganoEngine.Components;
using Raylib_cs;
using System.Numerics;
using Test;

public partial class Program : Game
{
    public static void Main(string[] args)
    {
        new Program();
    }

    public Program() : base(Dimension._3D)
    {
        Run();
    }

    public override void Init()
    {

        Console.WriteLine("Test");
        base.Init();
        //Texture2D texture = contentManager.Load<Texture2D>("content/player.png");

        Player player = new()
        {
            Position = new Vector3(400, 240, 0),
            texture = contentManager.Load<Texture2D>("player.png"),
            color = Color.White,
            Size = new Vector3(128, 128,0),
        };

        Node3D node = new()
        {

            Position = new Vector3(0, 0, 0),
            Texture = contentManager.Load<Texture2D>("texture.png"),
            Model = contentManager.Load<Model>("platypus.gltf"),
            Size = new Vector3(1, 1, 1),
            Scale = new Vector3(1f, 1f, 1f),
            Color = Color.White,
        };
        node.SetMaterialTexture();
    }
}
