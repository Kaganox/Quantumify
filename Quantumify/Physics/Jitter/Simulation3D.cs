using Jitter2;
using Jitter2.Collision;
using Jitter2.SoftBodies;
using Quantumify.Physics.Jitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantumify.Physics.Jitter;

public class Simulation3D : IDisposable, ISimulation
{

    public readonly World World;

    private Physics3DSettings _settings;

    /// <summary>
    /// Initializes a new instance of the Simulation class with the specified physics settings.
    /// </summary>
    /// <param name="settings">The physics settings to configure the simulation.</param>
    public Simulation3D(Physics3DSettings settings)
    {
        this._settings = settings;

        this.World = new World(settings.MaxBodies, settings.MaxContacts, settings.MaxConstraints)
        {
            Gravity = PhysicsConversion.ToJVector(settings.Gravity),
            UseFullEPASolver = settings.UseFullEPASolver
        };

        this.World.DynamicTree.Filter = DynamicTreeCollisionFilter.Filter;
        this.World.BroadPhaseFilter = new BroadPhaseCollisionFilter(this.World);
        this.World.NarrowPhaseFilter = new TriangleEdgeCollisionFilter();
    }

    /// <summary>
    /// Performs a single step in the physics simulation based on the given time step and multi-threading flag.
    /// </summary>
    /// <param name="timeStep">The duration of the step in seconds.</param>
    public void Step(float timeStep)
    {
        this.World.Step(timeStep, this._settings.MultiThreaded);
    }

    public void Dispose()
    {
        this.World.Clear();
    }
}