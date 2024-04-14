using nkast.Aether.Physics2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Physics.Aether;

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
        World.Step(timeStep);
    }

    public void Dispose()
    {
        World.Clear();
    }
}
