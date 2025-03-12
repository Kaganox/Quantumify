using System.Numerics;
using LibNoise.Combiner;
using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Gui;

public class SpiderChart : GuiElement
{
    private int _sectorDegree;
    private Dictionary<String, int> _datas;
    private Color _color;
    private int _size;
    public SpiderChart(int size,int levelCount,Dictionary<String, int> datas, Color? color = default) : base()
    {
        this._size = size;
        this._datas = datas;
        _sectorDegree = 360 / (datas.Count);
        _color = color ?? Color.White;
        
        
    }

    public override void Overlay()
    {
        base.Overlay();
        
        int i = 0;
        List<Vector2> background = new List<Vector2>();
        List<Vector2> levels = new List<Vector2>(); 
        List<Vector2> lines = new List<Vector2>(); 
        foreach (var key in _datas)
        {

            Vector2 degress = Engine.DegreesToVector(_sectorDegree * i);
            
            Vector2 line = degress*_size;
            background.Add(degress*_size*1.2f+this.Position);
            
            Vector2 level = Engine.DegreesToVector(_sectorDegree * i)*key.Value+this.Position;
            
            levels.Add(level);
            lines.Add(line);
            //background.Add(Engine.DegreesToVector(_sectorDegree * i + _sectorDegree*.5f));
            Raylib.DrawRectangle((int)level.X,(int)level.Y,10,10,_color);
            
            i++;
        }
        
        Engine.DrawFilledPolygon(background.ToArray(),Color.Gray);

        for (int j = 0; j < 2; j++)
        {
            Raylib.DrawPolyLines(this.Position,background.Count,_size*1.19f+j,0,Color.DarkGray);
        }

        //Engine.DrawFilledPolygon(background.ToArray(),Color.Gray);
        Engine.DrawFilledPolygon(levels.ToArray(),Color.Blue);
        int index = 0;

        foreach (Vector2 v2 in lines)
        {
            Raylib.DrawLine((int)this.Position.X,(int)this.Position.Y,(int)this.Position.X+(int)v2.X,(int)this.Position.Y+(int)+v2.Y,Color.Black);
            Raylib.DrawText(_datas.Keys.ToArray()[index].ToString(),(int)v2.X,(int)v2.Y,_size/10,Color.Black);

            index++;
        }

    }
    
}