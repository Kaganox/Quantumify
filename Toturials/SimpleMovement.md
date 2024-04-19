# Create Simple Player Movement

First creating the Game Window
**Programm.cs**
```cs
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using System.Numerics;

public class YourGame : Game {
    
    public static void Main(string[] args)
    {
        new TestGame(); //Create game instance
    }
    
    public TestGame() : base(Dimension._2D)
    {
        Run(new Scene(dimension,new Physic2DSettings?()));
    }
    
    public override void Init(){
        //Create Player
        Player player = new()
        {
            position = new Vector3(400, 240, 0),
            texture = ContentManager.GetTexture("player.png"), // Add the xml up (loads from "content/player.png")
        };
        Cam3D cam3D = new() //Create Camera
        {
            Position = new Vector3(10, 10, 10),
        };
        player.AddChild(cam3D); //make follow player
        cam3D.SetActiveCamera(); //makes the camera active 
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


As next you did need a Custom Node
What is a Node? A node is a object with parent system, so you can subordinate.
**Player.cs**
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
}
```

Create Floor how the player can Stay
```cs
public class Floor : RigidBody2D {
    public Floor() : base(bodyType:BodyType.Static) { // make sure you make it static
        Size = new Vector3(128*5, 128, 0);
        Color = Raylib_cs.Color.Blue;
    }
}
```


You also can use other keys
```cs
//Here as example it will change to arrow keys
Input.Vector2Input(
    KeyboardKey.Up,
    KeyboardKey.Down,
    KeyboardKey.Left,
    KeyboardKey.Right
);
```