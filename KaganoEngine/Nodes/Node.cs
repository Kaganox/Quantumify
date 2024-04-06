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

public abstract class Node
{
    private List<Component> components = new List<Component>();

    public Vector3 Position = new Vector3(0, 0, 0),
                   Scale = new Vector3(1, 1, 1),
                   Size = new Vector3(50, 50, 0),
                   globalPosition = new Vector3(0, 0, 0);

    public string uuid;
    private List<Node> children = new List<Node>();

    private Node? parent;
    public string parentUUID;

    /// <summary>
    /// Creates a node
    /// </summary>
    public Node(string? uuid = null)
    {
        this.uuid = uuid ?? Guid.NewGuid().ToString();
        Game.currentScene?.nodes.Add(this);
    }

    /// <summary>
    /// Runs every frame
    /// </summary>
    public virtual void Update()
    {
        globalPosition = Position;
        if (parent != null)
        {
            globalPosition += parent.globalPosition;
        }
        components.ForEach(component => component.Update());
    }

    public virtual void FixUpdate()
    {
        components.ForEach(component => component.FixedUpdate());
    }

    public virtual void Draw()
    {
        components.ForEach(component => component.Draw());

    }

    public int[] VectorToIntArray(Vector3 vector)
    {
        return new int[] { (int)vector.X, (int)vector.Y, (int)vector.Z };
    }

    public List<Node> GetChilds()
    {
        return children;
    }

    public Node? GetParent()
    {
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

    public void Destroy()
    {
        Scene? current = Game.currentScene;
        current?.nodes.Remove(this);
        components.ForEach(component => current?.components.Remove(component));
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
        components.ToList().ForEach(component =>
        {
            this.components.Add(component);
            Game.currentScene?.components.Add(component);
        });
    }

    public void RemoveComponent(params Component[] component)
    {
        components.ToList().ForEach(component =>
        {
            components.Remove(component);
            Game.currentScene?.components.Remove(component);
        });
    }

    public List<Component> GetComponents()
    {
        return components;
    }

    public virtual void Collide(Node interact)
    {

    }
}