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
        activeScene?.Update();
    }

    
    internal static void AfterUpdate()
    {
        activeScene?.Update();
    }
    
    internal static void FixedUpdate()
    {
        activeScene?.FixedUpdate();
    }

    internal static void Draw()
    {
        activeScene?.Draw();
    }
}
