using Jitter2.LinearMath;
using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Scenes;
using nkast.Aether.Physics2D.Dynamics;

namespace KaganoEngine.Nodes;

public class Node2D : Node
{
    public Texture2D? texture = null;
    
    public Color color = Color.White;
    public float Rotation = 0;
    public Vector2? Origin = null;

    
    public Node2D(Texture2D? texture = null) : base()
    {
        this.texture = texture;
        color = texture == null ? Color.Black : Color.White;
    }
    public override void Update() 
    {
        base.Update();

        if (Raylib.IsKeyDown(KeyboardKey.F))
        {
            Console.WriteLine(ToJson());
        }
    }

    public override void Draw()
    {
        int[] positionArray = VectorToIntArray(globalPosition);
        int[] sizeArrray = VectorToIntArray(Size);

        if (texture == null)
        {
            Raylib.DrawRectangle(positionArray[0], positionArray[1], sizeArrray[0], sizeArrray[1], color);
        }
        else
        {
            Texture2D texture2D = texture.Value;
            Rectangle source = new Rectangle(0, 0, texture2D.Width, texture2D.Height);
            Rectangle dest = new Rectangle(this.Position.X, this.Position.Y, this.Size.X, this.Size.Y);
            Vector2 origin = (Origin == null ? new Vector2(dest.Width / 2, dest.Height / 2) : (Vector2)Origin);
            Raylib.DrawTexturePro(texture2D, source, dest, origin, Rotation, color);
        }
    }

    public float RotateToNode(Node2D target)
    {
        if(target == null) { return 0; }
        Vector2 direction = new Vector2(target.globalPosition.X - globalPosition.X, target.globalPosition.Y - globalPosition.Y);

        // Calculate the angle of the direction vector
        float angle = (float)Math.Atan2(direction.Y, direction.X);

        // Convert the angle from radians to degrees
        float angleDegrees = angle * (180 / (float)Math.PI);

        return angleDegrees;
    }
}
