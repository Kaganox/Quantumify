using Quantumify;
using Quantumify.Gui;
using Quantumify.Nodes;
using Quantumify.Windowing;
using Raylib_cs;

namespace Test;

public class RainbowButton : Button
{
    private Tween _tween;


    private float x;

    public RainbowButton(LabelSettings ? settings = default) : base(settings)
    {
        _tween = new Tween();
        
        List<Color> colors = new List<Color>
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.DarkPurple,
            Color.Violet
        };

        int time = 2;
        
        int i = 0;
        foreach (Color color in colors)
        {

            Color lastColor;
            if (i == 0)
            {
                lastColor = colors.ElementAt(colors.Count-1);
            }
            else
            {
                lastColor = colors.ElementAt(i-1);
            }
            
            _tween.SetProperty((past) => LerpButton(past, color,lastColor), time, i * time);
            i++;
        }
        _tween.Running = true;
        _tween.Loop = true;

        _tween.SetProperty(ref x, 5, 0, 1, 10);
    }


    public void LerpButton(float past,Color color, Color lastColor)
    {
        double f = past;//Math.Floor(past * 100) / 100;

        if (past <= 0||past >= 1)
        {
            return;
        }
        
        this.Normal = Engine.LerpColor(lastColor, color, (float)f);
    }
    
    public override void Update()
    {
        base.Update();

        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            _tween.Running = true;
        }
        Logger.Debug(x+"");
    }
}