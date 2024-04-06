using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaganoEngine.Components;
using KaganoEngine.Nodes;

namespace KaganoEngine.Scenes;

public class Scene
{
    public List<Node> nodes = new List<Node>();
    public List<Component> components = new List<Component>();
    public Scene()
    {

    }
    public void ChangeScene(Scene? scene)
    {
        Game.currentScene = scene;
        Game.currentScene?.Dispose();
    }

    public void Dispose()
    {

    }
}
