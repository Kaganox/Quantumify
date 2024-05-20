using System.Numerics;
using Raylib_cs;

namespace Quantumify.Windowing;

public static class Engine
{
    
    public static Color LerpColor(Color colorA , Color colorB, float value)
    {
        int R = (int)Raymath.Lerp((float)colorA.R, (float)colorB.R, value);
        int G = (int)Raymath.Lerp((float)colorA.G, (float)colorB.G, value);
        int B = (int)Raymath.Lerp((float)colorA.B, (float)colorB.B, value);
        int A = (int)Raymath.Lerp((float)colorA.A, (float)colorB.A, value);
        return new Color(R, G, B, A);
    }
    
    public static bool CheckCollisionCircleSector(Vector2 position, float radius, float startAngle, float endAngle, Vector2 point)
    {
        // Normalize angles to be within [0, 360)
        startAngle = startAngle % 360.0f;
        endAngle = endAngle % 360.0f;
        
        if (startAngle < 0) startAngle += 360.0f;
        if (endAngle < 0) endAngle += 360.0f;
        
        // Calculate the difference between the angles
        float angleDiff = endAngle - startAngle;
        if (angleDiff < 0) angleDiff += 360.0f;
        
        // Calculate the vector from the center of the circle to the point
        Vector2 diff = new Vector2(point.X - position.X, point.Y - position.Y);
        float distance = MathF.Sqrt(diff.X * diff.X + diff.Y * diff.Y);

        // Check if the point is outside the circle
        if (distance > radius)
        {
            return false;
        }
        
        // Calculate the angle of the point relative to the center of the circle
        float pointAngle = MathF.Atan2(diff.Y, diff.X) * (180.0f / MathF.PI);
        if (pointAngle < 0) pointAngle += 360.0f;

        // Check if the point angle is within the sector angle range
        if (startAngle <= endAngle)
        {
            if (pointAngle >= startAngle && pointAngle <= endAngle)
            {
                return true;
            }
        }
        else
        {
            if (pointAngle >= startAngle || pointAngle <= endAngle)
            {
                return true;
            }
        }

        return false;
    }
    
    public static Color RandomColor()
    {
        return new Color(Rand.RangeInt(0, 255), Rand.RangeInt(0, 255), Rand.RangeInt(0, 255), 255);
    }
}