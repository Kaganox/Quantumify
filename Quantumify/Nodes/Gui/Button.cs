using System.Numerics;
using LibNoise.Renderer;
using Quantumify.Windowing;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Quantumify.Gui;

public class Button : GuiElement
{
    


    public String Text;

    public Color Normal;
    public Color Hover;
    public Color Pressed;
    public LabelSettings Settings;

    public delegate void Clicked();
    public event Clicked OnClicked;
    
    private bool _press;

    public float TextSize;


    public Button(LabelSettings? settings = default)
    {
        Settings = settings;
        if (settings == null)
        {
            Settings = new LabelSettings()
            {
                FontSize = 20,
                Spacing = 1
            };
        }
        
        Normal = Color.Gray;
        Hover = Color.LightGray;
        Pressed = Color.DarkGray;
    }
    
    public override void Overlay()
    {
        base.Overlay();
        
        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, Settings.FontSize, Settings.Spacing) * new Vector2(1,1.35f) + new Vector2(8,0);

        this.TextSize = textSize.X;
        Rectangle rect = new Rectangle(Position,textSize);
        Color color = Normal;
        if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),rect)){
            
            color = Hover;
            if(Raylib.IsMouseButtonDown(MouseButton.Left)){
                color = Pressed;
            }
            
            if(Raylib.IsMouseButtonReleased(MouseButton.Left)&&!_press){
                _press = true;
                OnClicked?.Invoke();
            }
            else
            {
                _press = false;
            }
            
        }

        Raylib.DrawRectangleRec(rect,color);

        Raylib.DrawRectangleLinesEx(rect,5,Engine.LerpColor(color,Color.Black,0.25f));
        Raylib.DrawTextEx(Raylib.GetFontDefault(),Text,rect.Position+new Vector2(5,5),Settings.FontSize,Settings.Spacing,Settings.Color);
    }
}