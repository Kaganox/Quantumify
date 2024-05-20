using System.Numerics;
using LibNoise.Primitive;
using Raylib_cs;

namespace Quantumify.Gui;

public class Label : GuiElement
{
    
    public string Text;
    public LabelSettings Settings { get; set; }

    public Label(LabelSettings settings = default)
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

        Rectangle rect = new Rectangle(Position, Size);
        Raylib.DrawTextEx(Raylib.GetFontDefault(),Text,rect.Position,Settings.FontSize,Settings.Spacing,Settings.Color);
    }
}