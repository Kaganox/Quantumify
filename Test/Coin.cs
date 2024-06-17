using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;
using Quantumify;
using Quantumify.Nodes;
using Quantumify.Nodes.Nodes2D;
using Raylib_cs;

namespace Test;

public class Coin : RigidBody2D
{
    private float _xMult;
    private int type;
    public Coin(float xMult,Texture2D? texture = null) : base(texture, color:Color.Yellow, bodyType:BodyType.Static)
    {
        this._xMult = xMult;
        Size = new Vector3(32, 32, 0);
        Enter = true;
        
        type = Rand.ChanchesMap<int>(
            new Dictionary<int, int>()
            {
                { 15, 0 }, // 15% gold
                { 25, 1 }, // 25% silver
                { 60, 2 }, // 60% bronze
            });

        switch (type)
        {
            case 0:
                Color = Color.Gold;
                break;
            case 1:
                Color = Color.Gray;
                break;
            case 2:
                Color = Color.Brown;
                break;
        }
        
    }

    public override void Ready()
    {
        base.Ready();
        
        //Body. = new Vector2(50+(this._xMult*2)*this.Size.X, -50);
    }

    public override void CollisionEnter(Node node)
    {
        base.CollisionEnter(node);
        if (node is Player player)
        {
            Collect();
        }
    }

    public override void OnClicked()
    {
        Collect();
    }

    public void Collect()
    {
        Player.Instance.Coins += 3-type;
        Destroy();
    }
    
    public override void Draw()
    {
        base.Draw();
    }
}