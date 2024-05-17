using System.Numerics;

namespace Quantumify.Nodes;

public class Tile
{
    
    public int Layer;
    public Vector3 Position;
    public Vector2 Atlas;
    
    public Tile(int layer, Vector3 position, Vector2 atlas)
    {
        this.Layer = layer;
        this.Position = position;
        this.Atlas = atlas;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Layer, Position, Atlas);
    }
        
    public override bool Equals(object obj)
    {
        if (obj is Tile tile)
        {
            return this.Layer == tile.Layer && this.Position == tile.Position && this.Atlas == tile.Atlas;
        }
        return base.Equals(obj);
    }
}