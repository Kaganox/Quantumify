using KaganoEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Components;
using nkast.Aether.Physics2D.Dynamics;

namespace Test;

public class Enemy : Node2D
{
    public Enemy() : base()
    {
        Position = new Vector3(350, 640 + 148,0);
        Size = new Vector3(128*5, 128, 0);
        
        AddComponent(new Collision2D(this,BodyType.Static));
    }
}
