﻿using System.Numerics;
using Quantumify;
using Quantumify.Config;
using Quantumify.Gui;
using Quantumify.Manager;
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
        Run(new Scene(dimension,new Physic2DSettings()
        {
            Gravity = new Vector2(0, 0)
        }));
    }

    public override void OnRun()
    {
        base.OnRun();
    }

    public override void Init()
    {

        base.Init();
        //new InputField(100, 100, 500, 100);
        
        Window.SetTitle("Test");
        Window.SetIcon(contentManager.Load<Image>("Unbenannt.png"));
        //Texture2D texture = contentManager.Load<Texture2D>("content/player.png");
        //Console.WriteLine(Environment.CurrentDirectory + "/content/save.dat");
        //SaveFile = new(Environment.CurrentDirectory + "/content/save.dat");
        //SaveFile.RegisterTyp<Player>();
        //test = contentManager.Load<Texture2D>("test.png");
        /*Enemy = new()
        {
            
        };*/

        /*new Calendar()
        {
            Position = new Vector2(150,50),
        };*/
        
        /*RainbowButton button = new RainbowButton(new LabelSettings()
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
        };*/
        /*
        Label label = new Label()
        {
            Text = "Tethrgest",
            Position = new Vector2(250,50),
            Size = new Vector2(5, 100),
        };
       */
       

/*
        JsonBuilder jsonBuilder = new JsonBuilder()
            .Load("test/test.json")
            .Add("test", 1)
            .Add("test1", 2)
            .Save("test/test.json");

        Logger.Warn(jsonBuilder.PrettyJson());*/
        /*CircleDiagram circle = new CircleDiagram()
        {
            ZIndex = 1
        };
        circle.SetData("Cookies", Color.Green, 0.5);
        circle.SetData("Milk", Color.DarkGreen, 0.1);
        circle.SetData("Salami", Color.Blue, 0.1);
        circle.SetData("Chocolate", Color.DarkBlue, 0.15);
        circle.SetData("Cheese", Color.SkyBlue, 0.15);
        
        RichTextLabel richTextLabel = new RichTextLabel()
        {
            Position = new Vector2(150,250),
            Size =  new Vector2(75, 50),
            HasBox = true,
            ZIndex = 2
        };
        richTextLabel
            .AppendText("Helwesrrrrrrrrlo\nWorld! This is a ")
            .SetColor(Color.Blue)
            .AppendText("TEST ")
            .SetColor(Color.Black)
            .AppendText("if it works!! wadasd asd asd as das das dasd a ");

        
        circle.OnHover += (name, color, value) =>
        {
            richTextLabel.Clear();
            richTextLabel.AppendText($"{name} {value*100}%");
            richTextLabel.Position = Raylib.GetMousePosition()+new Vector2(10,10);
        };
        richTextLabel.ZIndex = 1;
        richTextLabel.TurnVisible = () => circle.IsHover;
*/

        Random random = new Random();

        int size = 50;
        Dictionary<string, int> datas = new Dictionary<string, int>();
        for (int i = 0; i < 5; i++)
        {

            double rnd = size * 0.8f;
            double r = size / 5;
            datas.Add($"{i}", random.Next((int)rnd)+(int)r);
        }
        
        /*new SpiderChart(size,5,datas,Color.Green)
        {
            Position = new Vector2(250, 250),
        };*/
        
        /*var atlas = new Dictionary<int, Vector2>()
        {
            [Raylib.ColorToInt(Color.Black)] = new Vector2(0, 0),
            [Raylib.ColorToInt(Color.White)] = new Vector2(1, 0)
        };
        
        
        new ImageRect()
        {
            Position = new Vector2(0, 0),
            Size = new Vector2(1, 1),
            Texture = contentManager.Load<Texture2D>("test.png"),
            HoverAble = true
        }.OnClick += () => { Logger.Warn("Clicked"); };
        
        
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
        }; */
        
        Player player = new()
        {
            //Texture = test,
        };
        
        //button.OnClicked += () => { player.Coins++; };

        /*new CheckBox()
        {
            Position = new Vector2(50, 100),
        };

        new Slider()
        {
            Position = new Vector2(50, 50),
        };*/
        
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
        (Dictionary < Image, (int x, int y) > provinces, List<int> colors) = ProvinceGenerator.Create(contentManager.Load<Image>("prv.png"));
        int index = 0;
        foreach (var value in provinces)
        {
            new Provinze(Raylib.LoadTextureFromImage(value.Key),new Vector3(value.Value.x, value.Value.y,0),colors.ElementAt(index))
            {
                Size = new Vector3(1, 1,1),
                Scale = new Vector3(1, 1,1),
            };
            index++;
        }

        new ProvinceGenerator();
    }

    public override void OnClose()
    {
    }
}