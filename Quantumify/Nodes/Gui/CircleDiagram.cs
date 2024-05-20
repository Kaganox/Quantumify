using System.Numerics;
using System.Threading.Tasks.Dataflow;
using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Gui;

public class CircleDiagram : GuiElement
{
    private Dictionary<string, (Color, double)> datas;
    private List<Segment> values;
    
    public delegate void Hover(String name, Color color, double value);
    public event Hover OnHover;
    public CircleDiagram(Dictionary<string, (Color, double)> datas = default)
    {
        this.datas = datas;
        if (datas == null)
        {
            this.datas = new Dictionary<string, (Color, double)>();
        }

        Math();
    }

    public CircleDiagram SetData(string name, Color color, double value)
    {
        datas[name] = (color,value);
        Math();
        return this;
    }

    public CircleDiagram RemoveData(string name)
    {
        datas.Remove(name);
        Math();
        return this;
    }
    
    private void Math()
    {
        values = new List<Segment>();

        double total = 0;
        foreach (KeyValuePair<string, (Color color,double value)> entry in datas)
        {
            total += entry.Value.value;
        }
        
        foreach (KeyValuePair<string, (Color color,double value)> entry in datas)
        {
            values.Add(new Segment(entry.Key,entry.Value.color, entry.Value.value / total));
        }
    }
    
    public override void Overlay()
    {
        Position = new Vector2(500, 100);
        base.Overlay();
        
        
        DrawCircle();
        DrawOutline(100);
    }

    public void DrawCircle(bool outline = false)
    {
        double lastAngle = 0;
        
        
        foreach (Segment value in values)
        {
            if (outline)
            {
                DrawOutline(100, (int)lastAngle,(int)(lastAngle+value.Value * 360));
            }
            else
            {
                Color color = value.Color;
                if (Engine.CheckCollisionCircleSector(Position, 100, (int)lastAngle, (int)(lastAngle + value.Value * 360),
                        Raylib.GetMousePosition()))
                {
                    color = Engine.LerpColor(color, Color.Black, 0.25f);
                    OnHover?.Invoke("name", color, value.Value);
                }
                Raylib.DrawCircleSector(Position,100,(int)lastAngle,(int)(lastAngle+value.Value * 360),18,color);
            }

            lastAngle += value.Value * 360;
        }
    }

    public void DrawOutline(float Radius,float sAngle = 0, float eAngle = 360)
    {
        float thickness = 50;
        for (float i = 0; i < thickness; i += 1.0f)
        {
            Raylib.DrawCircleSectorLines(Position,Radius + (i/10),sAngle,eAngle,50, Color.Black);
        }
    }
    
    public class Segment(String name, Color color, double value)
    {
        public String Name = name;
        public Color Color = color;
        public double Value = value;
    }
}