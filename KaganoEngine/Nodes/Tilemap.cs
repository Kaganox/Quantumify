using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mime;
using System.Drawing.Imaging;
using nkast.Aether.Physics2D.Common;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace KaganoEngine.Nodes;

public class Tilemap : Node
{
    
    //layer,position, atlas
    private Dictionary<int,Dictionary<int, Dictionary<int,Vector2>>> _tiles;
    public unsafe Tilemap(List<Image> layers, Dictionary<int,Vector2> atlas)
    {
        
        Image image = default;
        for (int layer = layers.Count - 1; layer >= 0; layer--)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = Raylib.GetImageColor(image,x,y);
                    SetTile(layer, new Vector2(x, y),atlas[Raylib.ColorToInt(color)]);
                }
            }
        }
        
        Color* pixels = (Color*)Raylib.MemAlloc(100*100*sizeof(Color));
        
        
        _tiles = new Dictionary<int, Dictionary<int, Dictionary<int, Vector2>>>();
    }

    public void SetTile(int layer,Vector2 position, Vector2 offset)
    {
        if (!_tiles.ContainsKey(layer))
        {
            Logger.Error("");
        }
        
        int x = (int)position.X,
            y = (int)position.Y;
        if (!_tiles[layer].ContainsKey(x))
        {
            _tiles[layer][x] = new Dictionary<int, Vector2>();
        }
        _tiles[layer][x][y] = offset;
    }

    public void GetTile(Vector2 position)
    {
        
    }
    
    public override void Draw()
    {
        
    }
}

