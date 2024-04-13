using System.Drawing;
using System.Numerics;
using KaganoEngine.Nodes;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Scenes;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Dynamics.Contacts;
using Raylib_cs;
using Vector2 = nkast.Aether.Physics2D.Common.Vector2;

namespace KaganoEngine.Components;

public class Collision2D : Component
{
    private readonly Body _body;
    public Collision2D(Node2D node, BodyType bodyType = BodyType.Dynamic) : base(node)
    {
        
        World world = ((Simulation2D)SceneManager.activeScene.Simulation).World;
        Vector2 position = new Vector2(node.Position.X, node.Position.Y);
        _body = world.CreateBody(position,node.Rotation,bodyType);
        _body.Mass = 1000;
        _body.CreateRectangle(node.Size.X, node.Size.Y, 1f,new Vector2());
        _body.Awake = true;
        
        
        _body.OnCollision += (sender, other, contact) =>
        {
            Logger.Info("enter");
            return true;
        };

        _body.OnSeparation += (sender, other, contact) =>
        {
            Logger.Info("exit");
        };
    }
    
    public override void Update()
    {
    }

    public override void AfterUpdate()
    {
    }

    public override void FixedUpdate()
    {
        node.Position = new Vector3(_body.Position.X, _body.Position.Y, 0);
        if (node is Node2D node2D) node2D.Rotation = _body.Rotation;

    }
    
    public Vector2 AddVelocity(Vector2 position)
    {
        _body.Awake = true;
        return _body.Position += position;
    }
}