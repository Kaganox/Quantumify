using System.Numerics;
using Raylib_cs;

namespace Quantumify.Nodes.Nodes2D;

public class Node2D : Node {
    
    public Texture2D? Texture;
    public Color Color;
    
    public float Rotation;
    public Vector2? Origin;
    
    
    /// <summary>
    /// Represents a 2D node in the Quantumify.
    /// </summary>
    public Node2D(Texture2D? texture = null, Color? color = default) : base() {
        this.Texture = texture;
        this.Color = color ?? Color.White;
    }

    /// <summary>
    /// Draws the Node2D on the screen.
    /// If a texture is assigned, it will be drawn as an image; otherwise, a rectangle of the specified color will be drawn.
    /// </summary>
    public override void Draw() {
        base.Draw();

        if (this.Texture != null) {
            Raylib.DrawRectangleV(new Vector2(GlobalPosition.X,GlobalPosition.Y), new Vector2(Size.X*Scale.X,Size.Y*Scale.Y), Color);
            this.DrawImage(this.GlobalPosition, this.Size,this.Scale, this.Texture.Value,Color.White, this.Origin);
        }
        else {
            Raylib.DrawRectangleV(new Vector2(GlobalPosition.X,GlobalPosition.Y), new Vector2(Size.X*Scale.X,Size.Y*Scale.Y), Color);
        }
    }

    /// <summary>
    /// Draws the specified image on the screen at the specified position and size.
    /// If a texture is assigned, it will be drawn as an image; otherwise, a rectangle of the specified color will be drawn.
    /// </summary>
    /// <param name="position">The position of the image.</param>
    /// <param name="size">The size of the image.</param>
    /// <param name="texture">The texture to be drawn.</param>
    /// <param name="origin">Optional. The origin point of the image. If not specified, the center of the image will be used.</param>
    public void DrawImage(Vector3 position, Vector3 size, Vector3 scale, Texture2D texture, Color? color = default, Vector2? origin = default, float rotation = 0) {
        float finalSizeX = texture.Width * size.X * scale.X;
        float finalSizeY = texture.Height * size.Y * scale.Y;
        
        Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle dest = new Rectangle(position.X-finalSizeX/2, position.Y-finalSizeY/2, finalSizeX,finalSizeY);
        Vector2 finalOrigin = origin ?? new Vector2(dest.Width / 2, dest.Height / 2);
        
        Raylib.DrawTexturePro(texture, source, dest, finalOrigin, rotation, color ?? Color.White);
    }

    /// <summary>
    /// Calculates the rotation angle in degrees to rotate towards the specified Node2D target.
    /// </summary>
    /// <param name="target">The target Node2D to rotate towards.</param>
    /// <returns>The rotation angle in degrees.</returns>
    public float RotateToNode(Node2D target) {
        Vector2 direction = new Vector2(target.GlobalPosition.X - this.GlobalPosition.X, target.GlobalPosition.Y - this.GlobalPosition.Y);
        return (float) Math.Atan2(direction.Y, direction.X) * Raylib.RAD2DEG;
    }
}
