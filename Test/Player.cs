using KaganoEngine;
using KaganoEngine.Config;
using KaganoEngine.Manager;
using KaganoEngine.Nodes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Components;
using KaganoEngine.Physics.Jitter;
using nkast.Aether.Physics2D.Dynamics;
using Vector2 = nkast.Aether.Physics2D.Common.Vector2;

namespace Test;

public class Player : Node2D,SaveAble
{
    private Collision2D Collision2D;
    public Player() : base()
    {
        Position = new Vector3(400, 640,0);
        Program program = Program.program;
        texture = program.contentManager.Load<Texture2D>("player.png");
        program.saveFile.AddSaveAble(this);
        color = Color.White;
        Size = new Vector3(128, 128, 0);
        Collision2D = new Collision2D(this);
        AddComponent(Collision2D);
    }
    public override void Update()
    {
        base.Update();

        Vector3 v = Input.Vector2Input() * 0.1f;

        Collision2D.AddVelocity(new Vector2(v.X, v.Y));
        Rotation = RotateToNode(Program.enemy);
    }

    public override void Collide(Node interact)
    {
        if(interact is Enemy enemy)
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