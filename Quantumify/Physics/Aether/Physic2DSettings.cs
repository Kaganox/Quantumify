using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Quantumify.Physics.Aether;
public struct Physic2DSettings
{
    public Vector2 Gravity;
    public readonly int VelocityIterations;
    public readonly int PositionIterations;

    public Physic2DSettings()
    {
        this.Gravity = new Vector2(0.0f, 9.80665f);
        this.VelocityIterations = 6;
        this.PositionIterations = 2;
    }
}
