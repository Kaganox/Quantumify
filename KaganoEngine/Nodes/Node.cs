using KaganoEngine.Components;
using KaganoEngine.Scenes;
using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Nodes;

public abstract class Node : IUpdate
{
    private List<Component> components = new List<Component>();
    internal List<string> tags = new List<string>();

    public Vector3 Position = new Vector3(0, 0, 0),
                   Scale = new Vector3(1, 1, 1),
                   Size = new Vector3(50, 50, 0),
                   GlobalPosition = new Vector3(0, 0, 0);

    public string uuid;
    public string parentUUID;

    private List<Node> children = new List<Node>();

    private Node? parent;

    /// <summary>
    /// Creates a node
    /// </summary>
    public Node(string? uuid = default) {
        this.uuid = uuid ?? Guid.NewGuid().ToString();
        SceneManager.ActiveScene?.Nodes.Add(this);
    }
    
    /// <summary>
    /// Runs every frame
    /// </summary>
    public virtual void Update() {
        components.ForEach(component => component.Update());
        
        GlobalPosition = Position;
        
        if (parent != null) {
            GlobalPosition += parent.GlobalPosition;
        }
    }

    public virtual void AfterUpdate() {
        components.ForEach(component => component.AfterUpdate());
    }

    public virtual void FixedUpdate() {
        components.ForEach(component => component.FixedUpdate());
    }

    public virtual void Draw() {
        components.ForEach(component => component.Draw());

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
        Scene? current = SceneManager.ActiveScene;
        current?.Nodes.Remove(this);
        components.ForEach(component => current?.Components.Remove(component));
    }

    /// <summary>
    /// Convert the Node to json (If you use it pls make sure what should public and what not)
    /// </summary>
    /// <returns>string of all public variables in Json format</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public void AddComponent(params Component[] components)
    {
        components.ToList().ForEach((Action<Component>)(component =>
        {
            this.components.Add(component);
            SceneManager.ActiveScene?.Components.Add(component);
        }));
    }

    public void RemoveComponent(params Component[] component)
    {
        components.ToList().ForEach((Action<Component>)(component =>
        {
            components.Remove(component);
            SceneManager.ActiveScene?.Components.Remove(component);
        }));
    }

    public List<Component> GetComponents()
    {
        return components;
    }

    public virtual void Collide(Node interact)
    {
    }
}