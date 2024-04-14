using Jitter2.LinearMath;
using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Scenes;
using nkast.Aether.Physics2D.Dynamics;
using Vector2A = nkast.Aether.Physics2D.Common.Vector2;

namespace KaganoEngine.Nodes;

public class Node2D : Node
{
    public Texture2D? texture = null;

    public Color color = Color.White;
    public float Rotation = 0;
    public Vector2? Origin = null;
    public Body _body;
    public BodyType _bodyType;

    public new Vector3 Position
    {
        get
        {
            if (_body == null)
            {
                Logger.Info("Body is null");
                return new Vector3(0,0,0);
            }
            return new Vector3(_body.Position.X, _body.Position.Y, 0);
        }
        set
        {
            if (_body != null)
            {
                _body.Position = new Vector2A(value.X, value.Y);
            }
        }
    }

    public Node2D(Texture2D? texture = null,BodyType bodyType = BodyType.Dynamic) : base()
    {
        this._bodyType = bodyType;
        this.texture = texture;
        color = texture == null ? Color.Black : Color.White;
    }
    public override void Update() 
    {
        base.Update();
        if(_body == null) { InitPhysics(); }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Rotation = _body.Rotation;
        Position = new Vector3(_body.Position.X, _body.Position.Y, 0);
    }

    public void InitPhysics()
    {
        World world = ((Simulation2D)SceneManager.activeScene.Simulation).World;
        Vector2 position = new Vector2(Position.X, Position.Y);
        _body = world.CreateBody(new Vector2A(Position.X,Position.Y),Rotation,_bodyType);
        _body.Mass = 1000;
        _body.CreateRectangle(Size.X, Size.Y, 1f,new Vector2A(Position.X,Position.Y));
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
    

    public override void Draw()
    {
        base.Draw();
        int[] positionArray = VectorToIntArray(globalPosition);
        int[] sizeArrray = VectorToIntArray(Size);

        if (texture == null)
        {
            Raylib.DrawRectangle(positionArray[0], positionArray[1], sizeArrray[0], sizeArrray[1], color);
        }
        else
        {
            DrawImage(Position, Size, texture.Value, Origin);
        }
    }

    public void DrawImage(Vector3 position, Vector3 size, Texture2D texture, Vector2? _origin = default)
    {
        Texture2D texture2D = texture;
        Rectangle source = new Rectangle(0, 0, texture2D.Width, texture2D.Height);
        Rectangle dest = new Rectangle(position.X, position.Y, size.X, size.Y);
        Vector2 origin = (_origin == null ? new Vector2(dest.Width / 2, dest.Height / 2) : (Vector2)_origin);
        Raylib.DrawTexturePro(texture2D, source, dest, origin, Rotation, color);
    }

    public float RotateToNode(Node2D target)
    {
        if(target == null) { return 0; }
        Vector2 direction = new Vector2(target.globalPosition.X - globalPosition.X, target.globalPosition.Y - globalPosition.Y);

        // Calculate the angle of the direction vector
        float angle = (float)Math.Atan2(direction.Y, direction.X);

        // Convert the angle from radians to degrees
        float angleDegrees = angle * (180 / (float)Math.PI);

        return angleDegrees;
    }
}
