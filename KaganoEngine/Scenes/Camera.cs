using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Scenes;

public class Camera
{

    public static Camera2D camera2D = new Camera2D();
    public static Camera3D camera3D = new Camera3D();


    public static void SetActiveCamera(Camera2D camera2D)
    {
        camera2D = camera2D;
    }

    public static void SetActiveCamera(Camera3D camera3D)
    {
        camera3D = camera3D;
    }

}
