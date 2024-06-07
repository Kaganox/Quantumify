using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Quantumify.Scenes;

namespace Quantumify.Nodes;

public abstract class Node : IUpdate,IDisposable
{
    internal List<string> tags = new List<string>();
    public int ZIndex { get; set; }
    public Vector3 Position = new Vector3(0, 0, 0);
    public Vector3 Scale = new Vector3(1, 1, 1);
    public Vector3 Size = new Vector3(50, 50, 0);
    public Vector3 GlobalPosition = new Vector3(0, 0, 0);

    public string uuid;
    public string parentUUID;
    
    private bool _isReady;
    
    private List<Node> children = new List<Node>();

    private Node? parent;

    public float DeltaTime => Raylib.GetFrameTime();
    
    /// <summary>
    /// Creates a node
    /// </summary>
    public Node(string? uuid = default) {
        this.uuid = uuid ?? Guid.NewGuid().ToString();
        SceneManager.ActiveScene?.Nodes.Add(this);
    }
    
    
    /// <summary>
    /// Does called if object is ready
    /// </summary>
    public virtual void Ready() {
        
    }

    public virtual void Overlay()
    {
        
    }

    /// <summary>
    /// Runs every frame
    /// </summary>
    public virtual void Update() {
        
        GlobalPosition = Position;
        
        if (parent != null) {
            GlobalPosition += parent.GlobalPosition;
        }
    }

    public virtual void AfterUpdate() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Draw() {

    }

    public int[] VectorToIntArray(Vector3 vector) {
        return new int[] { (int)vector.X, (int)vector.Y, (int)vector.Z };
    }

    public List<Node> GetChilds() {
        return children;
    }

    public Node? GetParent() {
        return parent;
    }

    /// <summary>
    /// Add the node as a Child of this object
    /// </summary>
    /// <param name="node"></param>
    /// <exception cref="Exception">Gives an error if you try to add his self as child</exception>
    public void AddChild(Node node)
    {
        if (this == node)
        {
            throw new Exception("Can't add his self as child");
        }
        if (node.parent != null)
        {
            node.parent.children.Remove(node);
        }
        children.Add(node);
        node.parent = this;
        node.parentUUID = this.uuid;
    }

    public void RemoveChild(Node node)
    {
        children.Remove(node);
        node.parent = null;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Node node)
        {
            return this.uuid == node.uuid;
        }
        return false;
    }

    public void AddTag(string tag)
    {
        tags.Add(tag);
    }
    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }
    public void RemoveTag(string tag)
    {
        tags.Remove(tag);
    }

    public void Destroy()
    {
        OnDestroy();
        Scene? current = SceneManager.ActiveScene;
        //current?.Nodes.Remove(this);
        this.children.ForEach(child => child.Destroy());
        current?.ToDispose.Add(this);
    }
    
    public virtual void OnDestroy()
    {
    }

    /// <summary>
    /// Convert the Node to json (If you use it pls make sure what should public and what not)
    /// </summary>
    /// <returns>string of all public variables in Json format</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public virtual void Collide(Node interact)
    {
    }
    
    

    public virtual void Dispose()
    {
        SceneManager.ActiveScene.Nodes.Remove(this);
    }
}