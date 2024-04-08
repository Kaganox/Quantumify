using KaganoEngine.Scenes;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Nodes;

public class CameraNode : Node
{
    Camera2D camera2D;
    Camera3D camera3D;

    public CameraNode() : base()
    {
        if(Game.game.dimension == Dimension._2D) {
            camera2D = new Camera2D();
        }
        if (Game.game.dimension == Dimension._3D)
        {
            camera3D = new Camera3D();
        }
    }

    public override void Update()
    {
        base.Update();
        camera2D.Offset = new Vector2(globalPosition.X, globalPosition.Y);

        camera3D.Position = globalPosition;
    }

    public Camera2D GetCamera2D()
    {
        return camera2D;
    }

    public Camera3D GetCamera3D()
    {
        return camera3D;
    }

    public void SetActiveCamera()
    {
        switch (Game.game.dimension) 
        {
            case Dimension._2D:
                Camera.SetActiveCamera(camera2D);
                break;
            case Dimension._3D:
                Camera.SetActiveCamera(camera3D);
                break;
        }
    }
}
