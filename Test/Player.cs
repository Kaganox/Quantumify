using Quantumify;
using Quantumify.Config;
using Quantumify.Manager;
using Quantumify.Nodes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Quantumify.Nodes.Nodes2D;
using Quantumify.Physics.Jitter;
using nkast.Aether.Physics2D.Dynamics;
using Vector2A = nkast.Aether.Physics2D.Common.Vector2;

namespace Test;

public class Player : RigidBody2D,SaveAble
{
    public int Coins;
    public Player() : base()
    {
       // ((TestGame)Game.Instance).SaveFile.AddSaveAble(this);
        Color = Color.Gray;
        Size = new Vector3(128, 128, 0);
        ZIndex = 1;
        //AddComponent(Collision2D);
    }
    public override void Update()
    {
        base.Update();

        Vector3 v = Input.Vector2Input() * 0.1f;
        Body.Position += new Vector2A(v.X, v.Y);
    }

    public override void Ready()
    {
        base.Ready();
        Body.Position = new Vector2A(0,-Size.Y);
    }

    public override void Draw()
    {
        base.Draw();
    }

    public override void Overlay()
    {
        base.Overlay();
        Raylib.DrawText("Coins: " + Coins, 0, 0,25,Color.Black);
    }

    public override void CollisionExit(Node node)
    {
        if(node is Floor enemy)
        {
            Logger.Warn("HIT");
        }
    }

    public NBT Write(NBT nbt)
    {
        nbt.SetFloat("x", Position.X);
        nbt.SetFloat("y", Position.Y);
        nbt.SetFloat("z", Position.Z);
        return nbt;
    }

    public void Read(NBT nbt)
    {
        
        float? x = nbt.GetFloat("x"),
               y = nbt.GetFloat("y"),
               z = nbt.GetFloat("z");
        Position = new Vector3(x ?? 0, y ?? 0, z ?? 0);
    }
}