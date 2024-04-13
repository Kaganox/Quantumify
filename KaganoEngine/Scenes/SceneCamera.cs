using KaganoEngine.Nodes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Scenes;

public class SceneCamera
{
    public static Camera Camera { get; private set; }


    public static void Init()
    {
        Camera = new Camera();
    }


    public static void SetActiveCamera(Camera camera)
    {
        Camera = camera;
    }
}
