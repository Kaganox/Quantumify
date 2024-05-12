using System.Net.Mime;
using System.Numerics;
using Quantumify.Scenes;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Quantumify.Nodes.Nodes2D;

public class TileMap : Node2D
{


    public int TileSize;
    Texture2D _atlas;
    //layer,position, atlas
    private Dictionary<int,Dictionary<int, Dictionary<int,Vector2>>> _tiles;
    public unsafe TileMap(List<Image> layers, Texture2D atlas,Dictionary<int,Vector2> atlasMap) : base(null,new Color(0,0,0,0))
    {
        SceneManager.ActiveScene?.Nodes.Add(this);
        _tiles = new Dictionary<int, Dictionary<int, Dictionary<int, Vector2>>>();
        _atlas = atlas;
        for (int layer = layers.Count - 1; layer >= 0; layer--)
        {
            Image image = layers[layer];
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = Raylib.GetImageColor(image,x,y);
                    
                    int decolor = Raylib.ColorToInt(color);
                    Logger.Warn(color.R + " " + color.G + " " + color.B);
                    if (atlasMap.ContainsKey(decolor))
                    {
                        if(atlasMap[decolor]==null){continue;}
                        SetTile(layer, new Vector2(x, y),atlasMap[decolor]);
                    }
                }
            }
        }
        
        //Color* pixels = (Color*)Raylib.MemAlloc(100*100*sizeof(Color));
    }

    public void SetTile(int layer,Vector2 position, Vector2 offset)
    {
        if (!_tiles.ContainsKey(layer))
        {
            _tiles[layer] = new Dictionary<int, Dictionary<int, Vector2>>();
        }
        
        int x = (int)position.X,
            y = (int)position.Y;
        if (!_tiles[layer].ContainsKey(x))
        {
            _tiles[layer][x] = new Dictionary<int, Vector2>();
        }
        _tiles[layer][x][y] = offset;
        Logger.Error("ADDED");
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
        base.Draw();
        foreach (KeyValuePair<int,Dictionary<int, Dictionary<int,Vector2>>> layer in _tiles)
        {
            foreach (KeyValuePair<int, Dictionary<int, Vector2>> x in _tiles[layer.Key])
            { 

                foreach (KeyValuePair<int, Vector2> y in _tiles[layer.Key][x.Key])
                {

                    Vector2 atlas = _tiles[layer.Key][x.Key][y.Key];

                    //TODO: math texture via atlas
                    DrawTile(new Vector3(x.Key*20, y.Key*20,0),_atlas, new Vector2(y.Value.X*TileSize,y.Value.Y*TileSize));
                }
            }   
        }
    }
    
    public void DrawTile(Vector3 position, Texture2D texture, Vector2? origin = default, float rotation = 0) {
        float finalSizeX = TileSize * TileSize * 1;
        float finalSizeY = finalSizeX;
        
        Rectangle source = new Rectangle(origin.Value.X, origin.Value.Y, TileSize, TileSize);
        Rectangle dest = new Rectangle(position.X-finalSizeX/2, position.Y-finalSizeY/2, finalSizeX,finalSizeY);
        Vector2 finalOrigin = origin ?? new Vector2(dest.Width / 2, dest.Height / 2);
        
        Raylib.DrawTexturePro(texture, source, dest, finalOrigin, rotation, Color.White);
    }
}

