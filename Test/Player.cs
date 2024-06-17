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

namespace Test;

public class Player : RigidBody2D,SaveAble
{
    public int Coins;
    public static Player Instance;
    public Player() : base()
    {
        Instance = this;
       // ((TestGame)Game.Instance).SaveFile.AddSaveAble(this);
        Color = Color.Gray;
        Size = new Vector3(128, 128, 0);
        ZIndex = 1;
        //AddComponent(Collision2D);
    }
    public override void FixedUpdate()
    {
        base.Update();

        Vector3 v = Input.Vector2Input() * 5f;
        Body.SetTransform(Body.GetTransform().p + new Vector2(v.X, v.Y),Rotation);
    }

    public override void Ready()
    {
        base.Ready();
        Body.SetTransform( new Vector2(0,-Size.Y),Rotation);
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