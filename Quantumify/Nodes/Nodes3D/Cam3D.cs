using System.Numerics;
using Quantumify.Scenes;
using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Nodes.Nodes3D;

public class Cam3D : Node,ICamera
{
    Camera3D camera3D;
    Camera2D camera2D;
    
    public Cam3D() : base()
    {
        camera2D = new Camera2D();
     
        camera2D.Zoom = 1f;
        camera2D.Offset = new Vector2(Window.GetWidth() / 2, Window.GetHeight() / 2);
        
        camera3D = new Camera3D();
        camera3D.Up = Vector3.UnitY;          // Camera up vector (rotation towards target)
        camera3D.FovY = 45.0f;
        camera3D.Position = new Vector3(10,10,10);// Camera field-of-view Y
        camera3D.Projection = CameraProjection.Perspective;             // Camera mode type
        camera3D.Target = new Vector3(0, 0, 0);
    }

    public override void Update()
    {
        base.Update();
        
        camera2D.Target = new Vector2(GlobalPosition.X, GlobalPosition.Y);
        camera3D.Position = GlobalPosition;
        camera3D.Target = new Vector3(0, 0, 0);
        if (Game.Instance.dimension == Dimension._3D)
        {
            Raylib.UpdateCamera(ref camera3D, CameraMode.Orbital);
        }
    }
    
    public Camera3D GetCamera3D() => camera3D;
    public Camera2D GetCamera2D() => camera2D;
    public Raylib_cs.Camera3D GetCamera() => camera3D;

    public void SetActiveCamera()
    {
        SceneCamera.SetActiveCamera(this);
    }
}
