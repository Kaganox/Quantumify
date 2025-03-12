using System.Numerics;
using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Dynamics.World;
using Jitter2.Collision.Shapes;
using Quantumify.Physics.Aether;
using Quantumify.Scenes;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Transform = Box2D.NetStandard.Common.Transform;

namespace Quantumify.Nodes.Nodes2D;

public class RigidBody2D : Node2D
{
    private List<Node> Collides;
    public World World => ((Simulation2D)SceneManager.ActiveScene?.Simulation!).World;
    private bool _hadSetuped;
    public Body? Body;
    public BodyType BodyType;
    public bool Enter;
    
    
    
    private BodyDef BodyDef;
    private FixtureDef FixtureDef;

    public Vector2 Velocity;
    public Vector2 Pos => Body?.Position ?? new Vector2(0,0);
    public RigidBody2D(Texture2D? texture = null, Color? color = default, BodyType bodyType = BodyType.Dynamic,FixtureDef? fixtureDef = default) : base(texture, color)
    {
        this.BodyType = bodyType;
        _hadSetuped = false;
        Collides = new List<Node>();
        Velocity = new Vector2(0,0);
        
        
        Vector3 finalSize = this.Scale * this.Size;
        
        FixtureDef = fixtureDef ?? new FixtureDef
        {
            shape = new PolygonShape(finalSize.X/2,finalSize.Y/2),
            density = 1
        };
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
            Body.SetAwake(true);
        }

        Position += newPosition;


        Vector2 v = SceneCamera.Camera.GetCamera2D().Target;
        
        //Raylib.DrawCircle((int)Raylib.GetMousePosition().X,(int)Raylib.GetMousePosition().Y,10,Color.Red);
        
        
        Rectangle rect = new Rectangle(GlobalPosition.X - v.X + Raylib.GetScreenWidth()/2, GlobalPosition.Y - v.Y + Raylib.GetScreenHeight()/2, Size.X*Scale.X, Size.Y*Scale.Y);
        //Raylib.DrawRectanglePro(rect, new Vector2(0, 0), 0,Color.Gold);
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),
                rect)&&Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            this.OnClicked();
        }

        
        CheckCollide();
        
        
    }

    public void CheckCollide()
    {
        List<Node> conntact = new List<Node>();
        for (Body body = this.World.GetBodyList(); body != null; body = body.GetNext()) {
            foreach (Node node in SceneManager.ActiveScene!.Nodes)
            {
                if (node is RigidBody2D rigid)
                {
                    if (body == rigid.Body)
                    {
                        
                        conntact.Add(node);
                    }
                }
            }
        }

        for(int i = 0; i < Collides.Count; i++)
        {
            
            Node node = Collides[i];
            if (!conntact.Contains(node))
            {
                this.CollisionExit(node);
                conntact.Remove(node);
                Collides.Remove(node);
                i--;
            }
        }
        
        conntact.ForEach(c =>
        {
            this.CollisionEnter(c);
            this.Collides.Add(c);
        });
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Collides.ForEach(this.Collide);
    }

    public Vector3 BodyVector3() => new Vector3(Body.Position.X, Body.Position.Y,0);
    
    /// <summary>
    /// Initializes the physics properties of the Node2D.
    /// This method creates a physics body for the Node2D and sets its properties.
    /// </summary>
    public void SetupPhysics() {
        
        
        BodyDef = new BodyDef
        {
            position = new Vector2(GlobalPosition.X,GlobalPosition.Y),
            angle = Rotation,
            type = this.BodyType
        };

        
        
        World world = ((Simulation2D) SceneManager.ActiveScene?.Simulation!).World;
        Vector2 pos = new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y);
        Vector2 size = new Vector2(this.Size.X * this.Scale.X, this.Size.Y * this.Scale.Y);
        this.Body = world.CreateBody(this.BodyDef); //pos, this.Rotation, this.BodyType);
        this.Body.CreateFixture(this.FixtureDef);//this.Size.X*this.Scale.X, this.Size.Y*this.Scale.X, 0,new Vector2(size.X,size.Y)/2);
        //this.Body.CreateCircle(System.Drawing.Size.X/2,pos+size);


        Transform transform = this.Body.GetTransform();
        this.Body.SetTransform(new Vector2(GlobalPosition.X,GlobalPosition.Y),Rotation);
    
        /*this.Body.OnCollision += (sender, other, contact) =>
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
        this.Body.Mass = 1000;*/
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
        World.DestroyBody(Body);
    }
}