using System.Numerics;
using Jitter2.Collision.Shapes;
using Quantumify;
using Quantumify.Config;
using Quantumify.Gui;
using Quantumify.Nodes;
using Quantumify.Nodes.Nodes2D;
using Quantumify.Nodes.Nodes3D;
using Quantumify.Physics.Aether;
using Quantumify.Scenes;
using Quantumify.Windowing;
using Raylib_cs;

namespace Test;

public partial class TestGame : Game
{
    public SaveFile SaveFile;
    public static Floor? Enemy;
    public static Texture2D test;
    public static void Main(string[] args)
    {
        new TestGame();
    }

    public TestGame() : base(Dimension._2D)
    {
        Run(new Scene(dimension,new Physic2DSettings?()));
    }

    public override void OnRun()
    {
        base.OnRun();
    }

    public override void Init()
    {

        base.Init();
        
        
        Window.SetTitle("Test");
        Window.SetIcon(contentManager.Load<Image>("Unbenannt.png"));
        //Texture2D texture = contentManager.Load<Texture2D>("content/player.png");
        //Console.WriteLine(Environment.CurrentDirectory + "/content/save.dat");
        //SaveFile = new(Environment.CurrentDirectory + "/content/save.dat");
        //SaveFile.RegisterTyp<Player>();
        //test = contentManager.Load<Texture2D>("test.png");
        Enemy = new()
        {
            
        };

        Button button = new Button(new LabelSettings()
        {
            Color = Color.Green,
            FontSize = 20,
            Spacing = 1
        })
        {
            Text = "Test",
            Position = new Vector2(50,50),
            Size = new Vector2(100, 100),
            Normal = Color.Red,
            Hover = Color.Blue,
            Pressed = Color.Green
        };
        
        Label label = new Label()
        {
            Text = "Tethrgest",
            Position = new Vector2(250,50),
            Size = new Vector2(5, 100),
        };

        RichTextLabel richTextLabel = new RichTextLabel()
        {
            Position = new Vector2(150,250),
            Size =  new Vector2(50, 100),
        };
        richTextLabel
            .AppendText("Helwesrrrrrrrrlo\nWorld! This is a ")
            .SetColor(Color.Blue)
            .AppendText("TEST ")
            .SetColor(Color.Black)
            .AppendText("if it works!! wadasd asd asd as das das dasd a ");

        
        
        CircleDiagram circle = new CircleDiagram()
        {
            
        };
        circle.SetData("test", Color.Red, 0.5);
        circle.SetData("test1", Color.Green, 0.1);
        circle.SetData("test2", Color.Blue, 0.15);
        circle.SetData("test3", Color.Yellow, 0.15);
        circle.SetData("test4", Color.Brown, 0.1);
        
        var atlas = new Dictionary<int, Vector2>()
        {
            [Raylib.ColorToInt(Color.Black)] = new Vector2(0, 0),
            [Raylib.ColorToInt(Color.White)] = new Vector2(1, 0)
        };
        
        TileMap tileMap = new(new List<Image>(){contentManager.Load<Image>("atlas.png")},
            contentManager.Load<Texture2D>("tilemap.png"),atlas)
        {
            TileSize = 4,
            Size = new Vector3(30, 30,1),
        };

        Tile tile = tileMap.GetTile(0, new Vector2(0, 0));
        tile.SetData("test", 0);
        int data = tile.GetData<int>("test");

        tileMap.OnClicked += (tile, button) =>
        {
            if (button == MouseButton.Left)
            {
                tileMap.SetTile(tile.Layer, new Vector2(tile.Position.X, tile.Position.Y), new Vector2(1, 0));
            }

            if (button == MouseButton.Right)
            {
                tileMap.SetTile(tile.Layer, new Vector2(tile.Position.X, tile.Position.Y), new Vector2(0, 0));
            }
        };
        
        Player player = new()
        {
            //Texture = test,
        };
        
        button.OnClicked += () => { player.Coins++; };

        
        Cam3D cam3D = new()
        {
            Position = new Vector3(10, 10, 10),
        };
        for (int i = 0; i < 25; i++)
        {
            Coin coin = new(i);
        }
        
        player.AddChild(cam3D);
        cam3D.SetActiveCamera();

        //List<Shape> shapes = new List<Shape>();
        /* RigidBody3D node = new(shapes)
    {
        Color = Color.White,
    };
    */
    }

    public override void OnClose()
    {
    }
}