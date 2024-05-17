using System.Numerics;

namespace Quantumify.Nodes;

public class Tile
{
    
    public int Layer;
    public Vector3 Position;
    public Vector2 Atlas;

    public Dictionary<String, object> Data;
    
    public Tile(int layer, Vector3 position, Vector2 atlas)
    {
        this.Layer = layer;
        this.Position = position;
        this.Atlas = atlas;
    }

    public T GetData<T>(string key)
    {
        if (Data != null && Data.ContainsKey(key))
        {
            return (T)Data[key];
        }
        return default(T);
    }
    
    public void SetData(string key, object value)
    {
        if (Data == null)
        {
            Data = new();
        }
        Data[key] = value;
    }
    
    public void RemoveData(string key)
    {
        if (Data != null)
        {
            Data.Remove(key);
        }

        if (Data.Count == 0)
        {
            Data = null;
        }
    }
    
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Layer, Position);
    }
        
    public override bool Equals(object obj)
    {
        if (obj is Tile tile)
        {
            return this.Layer == tile.Layer && this.Position == tile.Position;
        }
        return base.Equals(obj);
    }
}