using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Components;
using KaganoEngine.Nodes;
using KaganoEngine.Physics;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Physics.Jitter;
using Raylib_cs;

namespace KaganoEngine.Scenes;

public class Scene
{
    public List<Node> Nodes = new List<Node>();
    public List<Component> Components = new List<Component>();
    public readonly ISimulation Simulation;

    private Dimension _dimension;

    public Scene(Dimension dimension = Dimension._2D)
    {
        this._dimension = dimension;
    }

    public Scene(Dimension dimension, Physics3DSettings? physic3DSettings = default) : this(dimension)
    {
        Simulation = new Simulation3D(physic3DSettings ?? new());
    }

    public Scene(Dimension dimension, Physic2DSettings? physic2DSettings = default) : this(dimension)
    {
        Simulation = new Simulation2D(physic2DSettings ?? new());
    }

    public void Update()
    {
        Nodes.ForEach(node => node.Update());
    }

    public void FixedUpdate()
    {
        Nodes.ForEach(node => node.FixedUpdate());
        Simulation.Step(1f / 60f);
    }

    public void AfterUpdate()
    {
        Nodes.ForEach(node => node.AfterUpdate());
    }

    public void Draw()
    {
        switch (_dimension)
        {
            case Dimension._2D:
                Raylib.BeginMode2D(SceneCamera.Camera.GetCamera2D());
                Nodes.ForEach(node => node.Draw());
                Raylib.EndMode2D();
                break;
            case Dimension._3D:
                Raylib.BeginMode3D(SceneCamera.Camera.GetCamera3D());
                Raylib.DrawGrid(10, 1);
                Nodes.ForEach(node => node.Draw());
                Raylib.EndMode3D();
                break;
        }
    }

    public List<Node> GetNodesByTag(string tag)
    {
        return Nodes.FindAll(node => node.HasTag(tag));
    }
    //Raylib.DrawCube(new Vector3(0, 0, 0), 1, 1, 1, Color.Red);

    public void Dispose()
    {

    }
}
