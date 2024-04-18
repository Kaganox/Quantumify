using Quantumify.Nodes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Quantumify.Nodes.Nodes3D;

namespace Quantumify.Scenes;

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
