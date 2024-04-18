using System.Numerics;
using Jitter2.Collision.Shapes;
using Quantumify;
using Quantumify.Config;
using Quantumify.Nodes.Nodes3D;
using Quantumify.Physics.Aether;
using Quantumify.Scenes;
using Raylib_cs;

namespace Test;

public partial class TestGame : Game
{
    public SaveFile SaveFile;
    public static Enemy? Enemy;
    public static Texture2D test;
    public static void Main(string[] args)
    {
        new TestGame();
    }

    public TestGame() : base(Dimension._2D)
    {
        Run(new Scene(dimension,new Physic2DSettings?()));
    }

    public override void Init()
    {

        base.Init();
        //Texture2D texture = contentManager.Load<Texture2D>("content/player.png");
        Console.WriteLine(Environment.CurrentDirectory + "/content/save.dat");
        SaveFile = new(Environment.CurrentDirectory + "/content/save.dat");
        SaveFile.RegisterTyp<Player>();
        test = contentManager.Load<Texture2D>("test.png");
        Enemy = new()
        {
            
        };
        
        Player player = new()
        {
            //Texture = test,
        };
        Cam3D cam3D = new()
        {
            Position = new Vector3(10, 10, 10),
        };
        player.AddChild(cam3D);
        cam3D.SetActiveCamera();

        List<Shape> shapes = new List<Shape>();
        shapes.Add(new BoxShape(1,1,1));
        
        /* RigidBody3D node = new(shapes)
    {
        Texture = contentManager.Load<Texture2D>("new.png"),
        Model = contentManager.Load<Model>("model.glb"),
        Size = new Vector3(1, 1, 1),
        Scale = new Vector3(1f, 1f, 1f),
        Rotation = Quaternion.Identity,
        Color = Color.White,
    };
    node.SetMaterialTexture();*/
    }

    public override void OnClose()
    {
        SaveFile.Write();
    }
}