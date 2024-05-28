using System.Numerics;
using Raylib_cs;

namespace Quantumify.Gui;

public class CheckBox : GuiElement
{
    public bool On;
    private bool clicked;
    
    
    
    public CheckBox() : base()
    {
        On = false;
        Size = new Vector2(20, 20);
    }
    public override void Overlay()
    {
        base.Overlay();


        Rectangle rect = GetRectangle();
        
        
        
        if (On)
        {
            Raylib.DrawLineEx(Position,Position+new Vector2(GetRectangle().Width,GetRectangle().Height),2,Color.Black);
            Raylib.DrawLineEx(Position-new Vector2(0,-GetRectangle().Height),Position+new Vector2(GetRectangle().Width,0),2,Color.Black);
        }
        
        Raylib.DrawRectangleLines((int)Position.X,(int)Position.Y,(int)rect.Width,(int)rect.Height,Color.Black);

        
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.X, Position.Y, GetRectangle().Width, GetRectangle().Height)))
        {
            if(Raylib.IsMouseButtonPressed(MouseButton.Left)&&!clicked)
            {
                clicked = true;
                On = !On;
            }
            else
            {
                clicked = false;
            }
        }
    }
    
    
}