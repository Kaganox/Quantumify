using System.Numerics;
using System.Threading.Tasks.Dataflow;
using nkast.Aether.Physics2D.Dynamics;
using Quantumify.Physics.Aether;
using Quantumify.Scenes;
using Raylib_cs;
using Vector2A = nkast.Aether.Physics2D.Common.Vector2;

namespace Quantumify.Nodes.Nodes2D;

public class RigidBody2D : Node2D
{
    private List<Node> Collides;
    public World World => ((Simulation2D)SceneManager.ActiveScene?.Simulation!).World;
    private bool _hadSetuped;
    public Body? Body;
    public BodyType BodyType;
    public bool Enter;
    
    public Vector2A Pos => Body?.Position ?? new Vector2A(0,0);
    public RigidBody2D(Texture2D? texture = null, Color? color = default, BodyType bodyType = BodyType.Dynamic) : base(texture, color)
    {
        this.BodyType = bodyType;
        _hadSetuped = false;
        Collides = new List<Node>();
    }

    
    public override void Update()
    {
        base.Update();

        if (!_hadSetuped)
        {
            SetupPhysics();
            Ready();
            _hadSetuped = true;
        }
        
        Vector3 newPosition = BodyVector3() - GlobalPosition;
        if (newPosition == Vector3.Zero)
        {
            Body.Awake = true;
        }

        Position += newPosition;


        Vector2 v = SceneCamera.Camera.GetCamera2D().Target;
        
        //Raylib.DrawRectanglePro(rect, new Vector2(0, 0), 0,Color.Gold);
        //Raylib.DrawCircle((int)Raylib.GetMousePosition().X,(int)Raylib.GetMousePosition().Y,10,Color.Red);
        
        
        Rectangle rect = new Rectangle(GlobalPosition.X - v.X + Raylib.GetScreenWidth()/2, GlobalPosition.Y - v.Y + Raylib.GetScreenHeight()/2, Size.X, Size.Y);
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),
                rect)&&Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            this.OnClicked();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Collides.ForEach(node => this.Collide(node));
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


        this.Body.OnCollision += (sender, other, contact) =>
        {
            RigidBody2D node = GetNodeFromFixture(other);
            if (!Collides.Contains(node))
            {
                Collides.Add(node);
                return !(this.Enter || node.Enter);
            }
            CollisionEnter(node);
            return true;
        };

        this.Body.OnSeparation += (sender, other, contact) =>
        {
            RigidBody2D node = GetNodeFromFixture(other);
            if (Collides.Contains(node))
            {
                Collides.Remove(node);
            }
            CollisionExit(node);
        };
        this.Body.Mass = 1000;
    }

    public RigidBody2D GetNodeFromFixture(Fixture fixture)
    {
        foreach (var node in SceneManager.ActiveScene!.Nodes)
        {
            if (node is RigidBody2D rigidBody2D)
            {
                if (fixture.Body == rigidBody2D.Body)
                {
                    return rigidBody2D;
                }
            }
        }
        return default!;
    }

    public virtual void CollisionEnter(Node node)
    {
        
    }

    public virtual void CollisionExit(Node node)
    {
        
    }
    
    public virtual void OnClicked()
    {
        
    }
    
    public override void Draw()
    {
        base.Draw();
    }
    
    public override void Ready()
    {
        base.Ready();
    }

    public override void Dispose()
    {
        base.Dispose();
        World.Remove(Body);
    }
}