# Create Simple Player Movement

First creating the Game Window
**Programm.cs**
```cs
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using System.Numerics;

public class YourGame : Game {
    
    public override void Init(){
        Player player = new()
        {
            position = new Vector3(400, 240, 0),
            texture = ContentManager.GetTexture("player.png"),
        };
    }
}

```

As next you did need a Custom Node
What is a Node? A node is a object with parent system, so you can subordinate.
**Player.cs**
```cs
public class Player : Node2D
{
    float speed = 0.01f
    public override void Process()
    {
        base.Process();
        
        position += Input.Vector2Input()*speed;
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

Add this to your project
```xml
<ItemGroup>
    <Content Include="content/**/*">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```