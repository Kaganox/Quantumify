using System.Numerics;
using nkast.Aether.Physics2D.Dynamics;
using Quantumify;
using Quantumify.Nodes;
using Quantumify.Nodes.Nodes2D;
using Raylib_cs;

namespace Test;

public class Coin : RigidBody2D
{
    private float _xMult;
    public Coin(float xMult,Texture2D? texture = null) : base(texture, Color.Yellow, BodyType.Static)
    {
        this._xMult = xMult;
        Size = new Vector3(32, 32, 0);
        Enter = true;
    }

    public override void Ready()
    {
        base.Ready();
        Body.Position = new nkast.Aether.Physics2D.Common.Vector2(50+(this._xMult*2)*this.Size.X, -50);
    }

    public override void CollisionEnter(Node node)
    {
        base.CollisionEnter(node);
        if (node is Player player)
        {
            player.Coins++;
            Destroy();
        }
    }

    public override void Draw()
    {
        base.Draw();
    }
}