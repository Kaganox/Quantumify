using System.Numerics;
using LibNoise.Renderer;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Quantumify.Gui;

public class Button : GuiElement
{
    
    public delegate void Clicked();
    public event Clicked OnClicked;

    public String Text;

    public Color Normal;
    public Color Hover;
    public Color Pressed;

    private bool Press;

    public LabelSettings Settings;

    public Button(LabelSettings settings = default)
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
    }
    
    public override void Overlay()
    {
        base.Overlay();
        
        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, Settings.FontSize, Settings.Spacing) * new Vector2(1,1.35f) + new Vector2(8,0);

        Rectangle rect = new Rectangle(Position,textSize);
        Color color = Normal;
        if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),rect)){
            
            color = Hover;
            if(Raylib.IsMouseButtonDown(MouseButton.Left)){
                color = Pressed;
            }
            
            if(Raylib.IsMouseButtonReleased(MouseButton.Left)&&!Press){
                Press = true;
                OnClicked?.Invoke();
            }
            else
            {
                Press = false;
            }
            
        }

        Raylib.DrawRectangleRec(rect,color);

        Raylib.DrawRectangleLinesEx(rect,5,LerpColor(color,Color.Black,0.25f));
        Raylib.DrawTextEx(Raylib.GetFontDefault(),Text,rect.Position+new Vector2(5,5),Settings.FontSize,Settings.Spacing,Color.Black);
    }
    
    public Color LerpColor(Color color, Color color2, float floatc){
        int R = (int)Raymath.Lerp((float)color.R, (float)color2.R, floatc);
        int G = (int)Raymath.Lerp((float)color.G, (float)color2.G, floatc);
        int B = (int)Raymath.Lerp((float)color.B, (float)color2.B, floatc);
        int A = (int)Raymath.Lerp((float)color.A, (float)color2.A, floatc);
        return new Color(R, G,B,A);
    }
}