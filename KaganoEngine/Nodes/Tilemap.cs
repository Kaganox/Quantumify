using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mime;
using nkast.Aether.Physics2D.Common;

namespace KaganoEngine.Nodes;

public class Tilemap : Node
{
    
    //position, offset
    private Dictionary<int,Dictionary<int, Dictionary<int,Vector2>>> _tiles;
    public Tilemap()
    {
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

