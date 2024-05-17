using System.Numerics;
using Quantumify.Nodes;
using Raylib_cs;

namespace Quantumify.Gui;

public class GuiElement : Node
{
    
    public int ZIndex { get; set; }
    public Vector2 Position;
    public Vector2 Size;
    
    internal Rectangle GetRectangle()
    {
        return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    }

    public override void Overlay()
    {
        base.Overlay();
    }
}