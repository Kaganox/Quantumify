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
    public static Cam3D Camera { get; private set; }


    public static void Init()
    {
        Camera = new Cam3D();
    }

    public static void SetActiveCamera(Cam3D cam3D)
    {
        Camera = cam3D;
    }
}
