using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Quantumify.Nodes.Nodes2D;

public class TileMap : Node2D
{


    public int TileSize;
    //layer,position, atlas
    private Dictionary<int,Dictionary<int, Dictionary<int,Vector2>>> _tiles;
    public unsafe TileMap(List<Image> layers, Dictionary<int,Vector2> atlas) : base(null,new Color(0,0,0,0))
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

    public Vector2 GetTile(int layer, Vector2 position)
    {
        int x = (int)position.X/TileSize,
            y = (int)position.Y/TileSize;

        if (_tiles.ContainsKey(layer) && _tiles[layer].ContainsKey(x) && _tiles[layer][x].ContainsKey(y))
        {
            return _tiles[layer][x][y];
        }

        Logger.Error($"Tile not found: layer: {layer}, pos: {x}.{y}");
        return default;
    }

    public Vector2 PositionToTileMaop(Vector2 position)
    {
        return new Vector2(position.X/TileSize, position.Y/TileSize);
    }
    
    public override void Draw()
    {
        foreach (KeyValuePair<int,Dictionary<int, Dictionary<int,Vector2>>> layer in _tiles)
        {
            foreach (KeyValuePair<int, Dictionary<int, Vector2>> x in _tiles[layer.Key])
            {
                foreach (KeyValuePair<int, Vector2> y in _tiles[layer.Key][x.Key])
                {
                    Vector2 atlas = _tiles[layer.Key][x.Key][y.Key];

                    //TODO: math texture via atlas
                    DrawImage(new Vector3(x.Key * TileSize, y.Key * TileSize,0), new Vector3(TileSize,TileSize,0), new Vector3(1,1,0),default);
                }
            }   
        }
    }
}

