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
    public static Scene? ActiveScene;


    public static void ChangeScene(Scene? scene)
    {
        SceneManager.ActiveScene?.Dispose();
        SceneManager.ActiveScene = scene;
    }

    internal static void Update()
    {
        ActiveScene?.Update();
    }

    
    internal static void AfterUpdate()
    {
        ActiveScene?.Update();
    }
    
    internal static void FixedUpdate()
    {
        ActiveScene?.FixedUpdate();
    }

    internal static void Draw()
    {
        ActiveScene?.Draw();
    }
}
