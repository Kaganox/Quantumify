using System.Numerics;
using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Gui;

public class ImageRect : GuiElement
{
    public Texture2D Texture;
    public delegate void ClickEvent();

    public ClickEvent OnClick;

    public bool HoverAble;
    private bool _pressed;
    
    public float Rotation;

    public Color Color;


    public ImageRect()
    {
        Color = Color.White;
    }
    
    public override void Overlay()
    {
        base.Overlay();
        
        Color color = Color;
        
        float finalSizeX = Texture.Width * Size.X * Scale.X;
        float finalSizeY = Texture.Height * Size.Y * Scale.Y;
        Rectangle dest = new Rectangle(Position.X-finalSizeX/2, Position.Y-finalSizeY/2, finalSizeX,finalSizeY);
        dest.Position += dest.Size / 2;
        
        
        
        if (CheckCollisionPointRec(Raylib.GetMousePosition(),dest,Rotation))
        {
            if (HoverAble)
            {
                color = Engine.LerpColor(color, Color.Black, 0.25f);
            }
            if (Raylib.IsMouseButtonPressed(MouseButton.Left)&&!_pressed)
            {
                _pressed = true;
                OnClick?.Invoke();
            }
            else
            {
                _pressed = false;
            }
        }
        
        DrawImage(Position,Size,Scale,Texture,color,Rotation);

        
        //Raylib.DrawTexture(Texture,(int)Position.X,(int)Position.Y,color);
    }
    

    private bool CheckCollisionPointRec(Vector2 point, Rectangle rec, float rotation) {
        float rotationInRadians = rotation * Raylib.DEG2RAD;
        
        float centerX = rec.X + rec.Width / 2f;
        float centerY = rec.Y + rec.Height / 2f;

        float deltaX = point.X - centerX;
        float deltaY = point.Y - centerY;

        float rotatedX = centerX + (deltaX * MathF.Cos(rotationInRadians) - deltaY * MathF.Sin(rotationInRadians));
        float rotatedY = centerY + (deltaX * MathF.Sin(rotationInRadians) + deltaY * MathF.Cos(rotationInRadians));

        return rotatedX >= rec.X && rotatedY >= rec.Y && rotatedX <= (rec.X + rec.Width) && rotatedY <= (rec.Y + rec.Height);
    }

    
    public void DrawImage(Vector2 position, Vector2 size, Vector3 scale, Texture2D texture, Color? color = default, float rotation = 0,Vector2? origin = default) {
        float finalSizeX = texture.Width * size.X * scale.X;
        float finalSizeY = texture.Height * size.Y * scale.Y;
        
        Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle dest = new Rectangle(position.X+finalSizeX/2, position.Y+finalSizeY/2, finalSizeX,finalSizeY);
        Vector2 finalOrigin = origin ?? new Vector2(dest.Width / 2, dest.Height / 2);
        
        Raylib.DrawTexturePro(texture, source, dest, finalOrigin, rotation, color ?? Color.White);
    }

}