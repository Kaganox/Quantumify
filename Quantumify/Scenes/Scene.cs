using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Quantumify.Nodes;
using Quantumify.Physics;
using Quantumify.Physics.Aether;
using Quantumify.Physics.Jitter;
using Raylib_cs;

namespace Quantumify.Scenes;

public class Scene
{
    public List<Node> Nodes = new List<Node>();
    public List<IDisposable> ToDispose = new List<IDisposable>();
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
        ToDispose.ForEach(obj => obj.Dispose());
        ToDispose.Clear();
        Nodes.ForEach(node => node.Update());
    }

    public void FixedUpdate()
    {
        Simulation.Step(1.0f / 60.0f);
        Nodes.ForEach(node => node.FixedUpdate());
    }

    public void AfterUpdate()
    {
        Nodes.ForEach(node => node.AfterUpdate());
    }

    public void Draw()
    {
        
        List<Node> drawPritority = new List<Node>(Nodes);
        drawPritority.Sort((a, b) => a.ZIndex.CompareTo(b.ZIndex));
        switch (_dimension)
        {
            case Dimension._2D:
                Raylib.BeginMode2D(SceneCamera.Camera.GetCamera2D());
                drawPritority.ForEach(node => node.Draw());
                Raylib.EndMode2D();
                break;
            case Dimension._3D:
                Raylib.BeginMode3D(SceneCamera.Camera.GetCamera3D());
                Raylib.DrawGrid(10, 1);
                drawPritority.ForEach(node => node.Draw());
                Raylib.EndMode3D();
                break;
        }
        
        drawPritority.ForEach(node => node.Overlay());
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
