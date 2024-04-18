using System.Numerics;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Scenes;
using nkast.Aether.Physics2D.Dynamics;
using Raylib_cs;

using Vector2A = nkast.Aether.Physics2D.Common.Vector2;

namespace KaganoEngine.Nodes;

public class RigidBody2D : Node2D
{

    public World World => ((Simulation2D)SceneManager.ActiveScene?.Simulation!).World;
    private bool _hadSetuped;
    public Body? Body;
    public BodyType BodyType;
    public RigidBody2D(Vector2 pos, Texture2D? texture = null, Color? color = default, BodyType bodyType = BodyType.Dynamic) : base(pos, texture, color)
    {
        this.BodyType = bodyType;
        _hadSetuped = false;
    }

    public override void Update()
    {
        base.Update();

        if (!_hadSetuped)
        {
            SetupPhysics();
            _hadSetuped = true;
        }
        
        Vector3 newPosition = BodyVector3() - GlobalPosition;
        if (newPosition == Vector3.Zero)
        {
            Body.Awake = true;
        }

        Position += newPosition;
    }
    
    public Vector3 BodyVector3() => new Vector3(Body.Position.X, Body.Position.Y,0);
    
    /// <summary>
    /// Initializes the physics properties of the Node2D.
    /// This method creates a physics body for the Node2D and sets its properties.
    /// </summary>
    public void SetupPhysics() {
        World world = ((Simulation2D) SceneManager.ActiveScene?.Simulation!).World;
        Vector2A pos = new Vector2A(this.GlobalPosition.X, this.GlobalPosition.Y);
        Vector2A size = new Vector2A(this.Size.X * this.Scale.X, this.Size.Y * this.Scale.Y);
        this.Body = world.CreateBody(pos, this.Rotation, this.BodyType);
        this.Body.CreateRectangle(this.Size.X*this.Scale.X, this.Size.Y*this.Scale.X, 0,new Vector2A(size.X,0)/2);
        //this.Body.CreateCircle(System.Drawing.Size.X/2,pos+size);

        this.Body.Mass = 1000;
    }

    public override void Draw()
    {
        base.Draw();
    }

    public override void Dispose()
    {
        base.Dispose();
        World.Remove(Body);
    }
}