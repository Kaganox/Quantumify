using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Scenes;

public class SceneManager
{
    public static Scene? scene3d;
    public static Scene? scene2d;

    public static Camera2D camera2D = new Camera2D();
    public static Camera3D camera3D = new Camera3D();
}
