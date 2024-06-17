using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Joints;
using Box2D.NetStandard.Dynamics.World;

namespace Quantumify.Physics.Aether;

public class Simulation2D : IDisposable, ISimulation
{
    public readonly World World;

    private Physic2DSettings _settings;

    public Simulation2D(Physic2DSettings settings)
    {
        _settings = settings;
        World = new World(settings.Gravity);
    }

    public void Step(float timeStep)
    {
        World.Step(timeStep,this._settings.VelocityIterations, this._settings.PositionIterations);
    }

    public void Dispose()
    {
        for (Body body = this.World.GetBodyList(); body != null; body = body.GetNext()) {
            this.World.DestroyBody(body);
        }
            
        for (Joint joint = this.World.GetJointList(); joint != null; joint = joint.GetNext()) {
            this.World.DestroyJoint(joint);
        }
    }
}
