using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Nodes;

public class Node2D : Node
{
    public Texture2D? texture = null;
    
    public Color color = Color.White;
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

        foreach (Node node in Game.currentScene.nodes)
        {

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
            Vector2 origin = new Vector2(dest.Width / 2, dest.Height / 2);
            Raylib.DrawTexturePro(texture2D, source, dest, origin, 0.0f, color);
        }
    }
}
