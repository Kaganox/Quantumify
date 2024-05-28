using System.Numerics;
using Raylib_cs;

namespace Quantumify.Gui;

public class Slider : GuiElement
{
    private float pos;
    bool moved = false;
    
    int value;
    int maxValue;
    int minValue;

    public Slider() : base()
    {
        minValue = 50;
        maxValue = 100;
        Size = new Vector2(100, 0);
    }
    
    public override void Overlay()
    {
        base.Overlay();
        
        
        float size = GetRectangle().Width;
        
        //Raylib.DrawLineEx(Position,Position+new Vector2(size,0),15,Color.Black);
        Raylib.DrawRectangleRounded(new Rectangle(Position.X,Position.Y-7,size,15),2,1,Color.Black);

        
        
        if (Raylib.CheckCollisionPointLine(Raylib.GetMousePosition(), Position, Position + new Vector2(size, 0), 5)&&Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            moved = true;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            moved = false;
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left)&&moved)
        {
            pos = Raylib.GetMousePosition().X - Position.X;
            if (pos < 0)
            {
                pos = 0;
            }
            
            if (pos > size)
            {
                pos = size;
            }
        }
        
        value = (int)((pos/size)*(maxValue-minValue))+minValue;

        Logger.Warn($"Value: {value}");
        
        Raylib.DrawCircle((int)(pos + Position.X),(int)(Position.Y),10,Color.Red);
    }
}