using Raylib_cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Scenes;

public static class SceneManager 
{
    public static Scene? activeScene;


    public static void ChangeScene(Scene? scene)
    {
        SceneManager.activeScene?.Dispose();
        SceneManager.activeScene = scene;
    }

    internal static void Update()
    {
        activeScene?.nodes.ForEach(node => node.Update());
    }

    internal static void Draw()
    {
        activeScene?.Draw();
    }

    internal static void FixedUpdate()
    {
        activeScene?.nodes.ForEach(node => node.FixedUpdate());
    }

    internal static void AfterUpdate()
    {

    }
}
