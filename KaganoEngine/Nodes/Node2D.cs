using Raylib_cs;
using System.Numerics;
using KaganoEngine.Physics.Aether;
using KaganoEngine.Scenes;
using nkast.Aether.Physics2D.Dynamics;
using Vector2A = nkast.Aether.Physics2D.Common.Vector2;

namespace KaganoEngine.Nodes;

public class Node2D : Node {
    
    public Texture2D? Texture;
    public Color Color;
    
    public float Rotation;
    public Vector2? Origin;
    
    public Body? Body;
    public BodyType BodyType;

    private Vector3 _position;
    
    /// <summary>
    /// Represents a 2D node in the KaganoEngine.
    /// </summary>
    public Node2D(Vector2 pos, Texture2D? texture = null, Color? color = default, BodyType bodyType = BodyType.Dynamic) {
        this.GlobalPosition = new Vector3(pos.X, pos.Y, 0);
        this.Texture = texture;
        this.Color = color ?? Color.White;
        this.BodyType = bodyType;
        
        this.SetupPhysics();
    }

    /// <summary>
    /// Represents the position property of a Node2D object.
    /// </summary>
    /// <remarks>
    /// The Position property allows getting and setting the position of a Node2D in 3D space.
    /// Setting the position of a Node2D will also update its global position if it has a parent.
    /// If the Node2D has a physics body, setting the position will update the body's position accordingly.
    /// </remarks>
    public new Vector3 Position {
        get {
            if (this.Body != null) {
                return new Vector3(this.Body.Position.X, this.Body.Position.Y, 0);
            }
            else {
                return this._position;
            }
        }
        set {
            if (this.Body != null) {
                this.Body.Position = new Vector2A(value.X, value.Y);
            }
            else {
                this._position = value;
            }
        }
    }

    /// <summary>
    /// Initializes the physics properties of the Node2D.
    /// This method creates a physics body for the Node2D and sets its properties.
    /// </summary>
    public void SetupPhysics() {
        World world = ((Simulation2D) SceneManager.ActiveScene?.Simulation!).World;
        Vector2A pos = new Vector2A(this.Position.X, this.Position.Y);
        
        this.Body = world.CreateBody(pos, this.Rotation, this.BodyType);
        this.Body.CreateRectangle(this.Size.X, this.Size.Y, 1f,pos);
    }

    /// <summary>
    /// Draws the Node2D on the screen.
    /// If a texture is assigned, it will be drawn as an image; otherwise, a rectangle of the specified color will be drawn.
    /// </summary>
    public override void Draw() {
        base.Draw();
        int[] positionArray = VectorToIntArray(this.GlobalPosition);
        int[] sizeArray = VectorToIntArray(this.Size);

        if (this.Texture != null) {
            this.DrawImage(this.Position, this.Size, this.Texture.Value, this.Origin);
        }
        else {
            Raylib.DrawRectangle(positionArray[0], positionArray[1], sizeArray[0], sizeArray[1], this.Color);
        }
    }

    /// <summary>
    /// Draws the specified image on the screen at the specified position and size.
    /// If a texture is assigned, it will be drawn as an image; otherwise, a rectangle of the specified color will be drawn.
    /// </summary>
    /// <param name="position">The position of the image.</param>
    /// <param name="size">The size of the image.</param>
    /// <param name="texture">The texture to be drawn.</param>
    /// <param name="origin">Optional. The origin point of the image. If not specified, the center of the image will be used.</param>
    public void DrawImage(Vector3 position, Vector3 size, Texture2D texture, Vector2? origin = default) {
        Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle dest = new Rectangle(position.X, position.Y, size.X, size.Y);
        Vector2 finalOrigin = origin ?? new Vector2(dest.Width / 2, dest.Height / 2);
        
        Raylib.DrawTexturePro(texture, source, dest, finalOrigin, this.Rotation, this.Color);
    }

    /// <summary>
    /// Calculates the rotation angle in degrees to rotate towards the specified Node2D target.
    /// </summary>
    /// <param name="target">The target Node2D to rotate towards.</param>
    /// <returns>The rotation angle in degrees.</returns>
    public float RotateToNode(Node2D? target) {
        if (target == null) return 0;
        
        Vector2 direction = new Vector2(target.GlobalPosition.X - this.GlobalPosition.X, target.GlobalPosition.Y - this.GlobalPosition.Y);
        return (float) Math.Atan2(direction.Y, direction.X) * Raylib.RAD2DEG;
    }
}
