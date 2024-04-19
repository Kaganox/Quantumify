```cs
public class TestGame : Game
{
    public SaveFile saveFile;
    
    public static void Main(string[] args)
    {
        new TestGame();
    }

    public TestGame() : base(Dimension._3D)
    {
        Run(new Scene(dimension,new Physic2DSettings?()));
    }

    public override void Init()
    {
        base.Init();
        saveFile = new(Environment.CurrentDirectory + "/content/save.dat"); // create save file (save.dat) in the content folder
        SaveFile.RegisterTyp<Player>();

        if (saveFile.Exists()) // Check if save file exist
        {
            saveFile.Read(); // Player will create from the save files
        }
        else
        {
            Player player = new()
            {
                Position = new Vector3(400, 240, 0),
            };
        }
    }

    // Run by closing Window
    public override void OnClose()
    {
        saveFile.Write(); // Saves registererd saveables
    }
}

```
Add this to your project to make able to load files like textures (png)
```xml
<ItemGroup>
    <Content Include="content/**/*">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```

```cs
public class Player : Node2D,SaveAble // Implement Saveable
{
    public Player() : base()
    {
        MyGame mygame = MyGame.mygame;
        texture = mygame.contentManager.Load<Texture2D>("player.png"); // Add the xml up (loads from "content/player.png")
        mygame.saveFile.AddSaveAble(this); // Register the object for saving
        color = Color.White;
        Size = new Vector3(128, 128, 0);
    }
    public override void Update()
    {
        base.Update();
        
        Position += Input.Vector2Input()*0.1f;
    }

}
```


```cs
public class Player : RigidBody2D
{
    private float _speed = 0.01f
        
    public Player() : base()
    {
        Color = Color.Gray;
        Size = new Vector3(128, 128, 0);
        ZIndex = 1;
    }
    
    public override void Update()
    {
        base.Update();

        Vector3 v = Input.Vector2Input() * _speed; // WASD vector * SPEED
        Body.Position += new Vector2A(v.X, v.Y); // Add it to the Physic Body
    }
    
    public override void Ready()
    {
        base.Ready();
        Body.Position = new Vector2A(0,-Size.Y); // set in on the top of the floor
    }
    
        public NBT Write(NBT nbt) // Saves Position
    {
        nbt.SetFloat("x", Position.X);
        nbt.SetFloat("y", Position.Y);
        nbt.SetFloat("z", Position.Z);
        return nbt;
    }


    // This method runs by creating the object from the save file
    public void Read(NBT nbt) // Load Position
    {

        float? x = nbt.GetFloat("x"),
               y = nbt.GetFloat("y"),
               z = nbt.GetFloat("z");
        Position = new Vector3(x ?? 0, y ?? 0, z ?? 0);
    }
}
```

