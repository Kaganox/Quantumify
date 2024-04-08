﻿using KaganoEngine.Scenes;
using KaganoEngine.Ultis;
using nkast.Aether.Physics2D.Collision;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Nodes;

public class Camera : Node
{
    Camera2D camera2D;
    Camera3D camera3D;

    public Camera() : base()
    {
        if (Game.game.dimension == Dimension._2D) {
            camera2D = new Camera2D();
            camera2D.Zoom = 1f;
            camera2D.Offset = new Vector2(Window.GetWidth() / 2, Window.GetHeight() / 2);
        }
        if (Game.game.dimension == Dimension._3D) { camera3D = new Camera3D(); };
    }

    public override void Update()
    {
        base.Update();
        camera2D.Target = new Vector2(globalPosition.X, globalPosition.Y);
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
        SceneCamera.SetActiveCamera(this);
        
    }
}