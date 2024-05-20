using Raylib_cs;

namespace Quantumify.Gui;

public class LabelSettings
{
    public int FontSize { get; set; }
    public int Spacing { get; set; }
    public Color Color { get; set; }
    
    public LabelSettings()
    {
        FontSize = 20;
        Spacing = 1;
        Color = Color.Black;
    }
    
}