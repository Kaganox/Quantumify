using System.Numerics;
using Quantumify.Nodes;
using Raylib_cs;

namespace Quantumify.Gui;

public class GuiElement : Node
{
    
    public int ZIndex { get; set; }
    public bool Visible;
    public Vector2 Position;
    public Vector2 Size;

    public Func<bool>? TurnVisible;
    
    
    public GuiElement() : base()
    {
        Visible = true;
    }
    
    internal Rectangle GetRectangle()
    {
        return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    }

    public override void Overlay()
    {
        base.Overlay();
    }

    public override void Update()
    {
        base.Update();
        
        Visible = TurnVisible?.Invoke() ?? Visible;
    }
}