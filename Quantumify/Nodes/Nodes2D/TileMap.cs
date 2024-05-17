using System.Numerics;
using Quantumify.Scenes;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Quantumify.Nodes.Nodes2D;

public class TileMap : Node2D
{
    public int TileSize;
    
    public Vector3 Size;
    public List<Tile> Tiles;
    public delegate void Clicked(Tile tile,MouseButton button);

    public event Clicked? OnClicked;
    
    Texture2D _atlas;
    public TileMap(List<Image> layers, Texture2D atlas,Dictionary<int,Vector2> atlasMap) : base(null,new Color(0,0,0,0))
    {
        Tiles = new List<Tile>();
        Size = new(10, 10, 0);
        _atlas = atlas;

        this.Rotation = 35;
        
        SceneManager.ActiveScene?.Nodes.Add(this);
        
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
    }

    protected void SortTiles()
    {
        Tiles.Sort((x,y) => x.Layer.CompareTo(y.Layer));
    }

    public void SetTile(int layer,Vector2 position, Vector2 offset)
    {
        if (!Tiles.Contains(new Tile(layer, new Vector3(position.X, position.Y,0), offset)))
        {
            Tiles.Add(new Tile(layer, new Vector3(position.X, position.Y,0), offset));
            Logger.Error("CREATED NEW");
        }
        else
        {
            Logger.Error("UPDATED");
            GetTile(layer,position).Atlas = offset;
        }
        //Logger.Error("ADDED "+position.X+", "+position.Y);
        SortTiles();
    }
    
    public void RemoveTile(int layer, Vector2 position)
    {
        Tiles.Remove(GetTile(layer, position));
    }

    public Tile GetTile(int layer, Vector2 position)
    {
        return Tiles.Find(t => t.Layer == layer &&
                               Math.Abs(t.Position.X - position.X) < 1 &&
                               Math.Abs(t.Position.Y - position.Y) < 1) ??
               throw new InvalidOperationException();
    }

    public Vector2 PositionToTileMaop(Vector2 position)
    {
        return new Vector2(position.X/TileSize, position.Y/TileSize);
    }
    
    public override void Draw()
    {
        base.Draw();
        int i = 0;
        while (i < Tiles.Count)
        {
            Tile _tile = Tiles.ElementAt(i);
            HandleTile(_tile);
            i++;
        }
    }

    public void HandleTile(Tile tile)
    {
        float finalSize = this.Scale.X * this.Size.X * (TileSize + 0.5f);

        DrawTile(tile.Position, finalSize,_atlas, tile.Atlas * TileSize, 0, new Vector2(0, 0));
        
        Vector3 transformedPosition = tile.Position * new Vector3(finalSize, finalSize, 0) - new Vector3(finalSize/2, finalSize/2, 0);
        Vector2 screen = Raylib.GetWorldToScreen2D(new Vector2(transformedPosition.X, transformedPosition.Y), SceneCamera.Camera.GetCamera2D());
        Rectangle screenRect = new Rectangle(screen.X, screen.Y, finalSize, finalSize);
        
        if (Raylib.CheckCollisionCircleRec(Raylib.GetMousePosition(), 0.001f, screenRect))
        {
            foreach (MouseButton button in Enum.GetValues(typeof(MouseButton)))
            {
                ClickedTask(tile, button);
            }
        }
    }

    private void ClickedTask(Tile tile,MouseButton button)
    {
        if (Raylib.IsMouseButtonPressed(button))
        {
            OnClicked?.Invoke(tile,button);
        }
    }
    
    public void DrawTile(Vector3 position, float finalSize, Texture2D texture, Vector2 origin, float rotation, Vector2 tilemapCenter) {

        // Apply scaling to the position
        Vector3 scaledPosition = position * new Vector3(finalSize, finalSize, 0);

        // Calculate the rotated position around the tilemap center
        Vector2 tilePosition = new Vector2(scaledPosition.X, scaledPosition.Y);
        Vector2 rotatedPosition = RotatePoint(tilePosition, tilemapCenter, rotation);

        Rectangle source = new Rectangle(origin.X, origin.Y, TileSize, TileSize);
        Rectangle dest = new Rectangle(rotatedPosition.X, rotatedPosition.Y, finalSize, finalSize);

        // Adjust the origin for the rotation
        Vector2 adjustedOrigin = new Vector2(finalSize / 2, finalSize / 2);

        Raylib.DrawTexturePro(texture, source, dest, adjustedOrigin, rotation, Color.White);
    }
    
    public Vector3 ToTilePosition(Vector3 position)
    {
        return position - this.Position / TileSize;
    }

    // Function to rotate a point around a center
    public Vector2 RotatePoint(Vector2 point, Vector2 center, float angle) {
        float rad = MathF.PI * angle / 180.0f;
        float cos = MathF.Cos(rad);
        float sin = MathF.Sin(rad);
        Vector2 translatedPoint = new Vector2(point.X - center.X, point.Y - center.Y);
        float newX = translatedPoint.X * cos - translatedPoint.Y * sin + center.X;
        float newY = translatedPoint.X * sin + translatedPoint.Y * cos + center.Y;
        return new Vector2(newX, newY);
    }
}

