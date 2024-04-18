using KaganoEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Dynamics;

namespace Test;

public class Enemy : RigidBody2D {
    public Enemy() : base(pos:new Vector2(350, 640 + 178), bodyType:BodyType.Static) {
        Size = new Vector3(128*5, 128, 0);
        Color = Raylib_cs.Color.Blue;

        //AddComponent(new Collision2D(this,BodyType.Static));
    }
}
