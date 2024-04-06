using KaganoEngine;
using KaganoEngine.Nodes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Test;

public class Player : Node2D
{

    public override void Update()
    {
        base.Update();
        
        Position += Input.Vector2Input()*0.1f;
    }

    public override void Collide(Node interact)
    {
        if(interact is Enemy enemy)
        {
            Console.WriteLine("HIT");
        }
    }
}