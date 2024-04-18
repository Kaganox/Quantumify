using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.DataStructures;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using KaganoEngine.Physics.Jitter;
using KaganoEngine.Scenes;

namespace KaganoEngine.Nodes;

public class RigidBody3D : Node3D
{
    public RigidBody RigidBody { get; private set; }

    public World World => ((Simulation3D)SceneManager.ActiveScene?.Simulation!).World;
    private ReadOnlyList<Shape> _shapes;
    private bool _setMassIntertia;
    private bool _nonMoving;
    private float _friction;
    private float _restitution;
    private bool _hadSetuped;
    
    public RigidBody3D(List<Shape> shapes,bool setMassIntertia = true, bool nonMoving = false, float friction = 0.2f, float restitution = 0f) : base()
    {
        _shapes = new ReadOnlyList<Shape>(shapes);
        _setMassIntertia = setMassIntertia;
        _nonMoving = nonMoving;
        _friction = friction;
        _restitution = restitution;
    }
    
    /*public new Vector3 Position
    {
      get => PhysicsConversion.FromJVector(RigidBody.Position);
      set => RigidBody.Position = PhysicsConversion.ToJVector(value);
    }*/

    public override void Ready()
    {
        base.Ready();
    }

    public override void Update()
    {
        base.Update();
        
        if (!_hadSetuped)
        {
            InitPhysic();
            _hadSetuped = true;
        }

        this.Position += PhysicsConversion.FromJVector(RigidBody.Position) - GlobalPosition;
    }

    public void InitPhysic()
    {
        this.RigidBody = this.World.CreateRigidBody();
        this.RigidBody.AddShape(this._shapes,this._setMassIntertia);
        this.RigidBody.IsStatic = this._nonMoving;
        this.RigidBody.Restitution = this._restitution;
        this.RigidBody.Friction = this._friction;
        this.RigidBody.Position = PhysicsConversion.ToJVector(this.GlobalPosition);
        this.RigidBody.Orientation = JMatrix.CreateFromQuaternion(PhysicsConversion.ToJQuaternion(Quaternion.Conjugate(this.Rotation)));
    }
    
    public override void Dispose()
    {
        base.Dispose();
        this.World.Remove(this.RigidBody);
    }
}