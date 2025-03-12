using System.Numerics;
using Box2D.NetStandard.Dynamics.World;
using Quantumify;
using Quantumify.Nodes;
using Quantumify.Windowing;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Test;

public class Provinze : Node
{
    
    public static Dictionary<int,Provinze> Provinzes = new Dictionary<int, Provinze>();
    
    private Texture2D _texture2D;
    public int Color;

    public Color RenderColor;
    public Provinze(Texture2D texture, Vector3 position,int color) : base()
    {
        _texture2D = texture;
        this.Color = color;

        Provinzes[color] = this;
        this.Position = position;
        Scale = new Vector3(1, 1, 1);
        Size = new Vector3(1, 1, 1);
        this.RenderColor = Raylib_cs.Color.Red;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Draw()
    {
        if (Color == 0)
        {
            return;
        }
        Raylib.DrawTexture(_texture2D, (int)Position.X, (int)Position.Y, RenderColor);
        base.Draw();
    }

    public void OnClick()
    {
        Color color = Engine.RandomColor();
        foreach (var neighbour in GetNeighbours())
        {
            neighbour.RenderColor = color;
        }

    }
    
    public List<Provinze> GetNeighbours()
    {
        List<Provinze> list = new List<Provinze>();
        foreach (int color in ProvinceGenerator.Neighbours[Color])
        {
            list.Add(GetProvinze(color));
        }
        return list;
    }
    
    public static Provinze? GetProvinze(int color)
    {
        return Provinzes[color];
    }
    
    public static List<Provinze> FindPath(Provinze provinze)
    {
        
        return null;
    }
}