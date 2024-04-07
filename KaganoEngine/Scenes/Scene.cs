using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Components;
using KaganoEngine.Nodes;
using Raylib_cs;

namespace KaganoEngine.Scenes;

public class Scene
{
    public List<Node> nodes = new List<Node>();
    public List<Component> components = new List<Component>();
    Dimension dimension;
    public Scene(Dimension dimension = Dimension._2D)
    {
        this.dimension = dimension;
    }

    public void Draw()
    {
        switch (dimension)
        {
            case Dimension._2D:
                Raylib.BeginMode2D(Camera.camera2D);
                nodes.ForEach(node => node.Draw());
                Raylib.EndMode2D();
                break;
            case Dimension._3D:
                Raylib.BeginMode3D(Camera.camera3D);
                Raylib.DrawGrid(10, 1);
                nodes.ForEach(node => node.Draw());
                Raylib.EndMode3D();
                break;
        }
    }
    //Raylib.DrawCube(new Vector3(0, 0, 0), 1, 1, 1, Color.Red);

    public void Dispose()
    {

    }
}
