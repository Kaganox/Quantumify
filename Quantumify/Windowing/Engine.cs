using System.Diagnostics.Contracts;
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
    
    public static Vector3 Vector2ToVector3(Vector2 vector2)
    {
        return new Vector3(vector2.X, vector2.Y, 0);
    }
    
    public static Vector2 Vector3ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.X, vector3.Y);
    }
    
    public static Color IntToColor(int colorInt) {
        Color color = new Color();
        color.R = (byte)((colorInt >> 24) & 0xFF); // Extract red component
        color.G = (byte)((colorInt >> 16) & 0xFF); // Extract green component
        color.B = (byte)((colorInt >> 8) & 0xFF);  // Extract blue component
        color.A = (byte)(colorInt & 0xFF);         // Extract alpha component
        return color;
    }

    public static Vector2 DegreesToVector(double degrees)
    {
        double radians = degrees * Math.PI / 180.0;
        return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }
    
    /// <summary>
    /// Draws a filled polygon and its outline.
    /// </summary>
    /// <param name="points">Array of Vector2 points defining the polygon.</param>
    /// <param name="fillColor">Color to fill the polygon.</param>
    /// <param name="outlineColor">Color for the polygon outline.</param>
    public static void DrawPolygon(Vector2[] vertices, Color color)
    {
        int vertexCount = vertices.Length;

        for (int i = 0; i < vertexCount; i++)
        {
            // Calculate the next vertex index (wrap around to the first vertex)
            int nextIndex = (i + 1) % vertexCount;

            // Draw a line between the current vertex and the next vertex
            Raylib.DrawLineEx(vertices[i], vertices[nextIndex], 2, color);
        }
    }
    
    static Vector2[] SortVerticesCounterClockwise(Vector2[] vertices)
    {
        // Calculate the centroid (average position of all vertices)
        Vector2 centroid = new Vector2(
            vertices.Average(v => v.X),
            vertices.Average(v => v.Y)
        );

        // Sort vertices based on their angle relative to the centroid
        return vertices
            .OrderBy(v => MathF.Atan2(v.Y - centroid.Y, v.X - centroid.X))
            .ToArray();
    }

    public static void DrawFilledPolygon(Vector2[] vertices, Color color)
    {
        vertices = SortVerticesCounterClockwise(vertices);
        int vertexCount = vertices.Length;

        if (vertexCount < 3)
        {
            // A polygon must have at least 3 vertices to be valid
            return;
        }

        // Use a triangle fan to draw the filled polygon
        for (int i = 1; i < vertexCount - 1; i++)
        {
            DrawTriangle(vertices[0], vertices[i], vertices[i + 1], color);
        }
    }
    static void DrawTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color color)
    {
        // Sort vertices by their y-coordinates (ascending order)
        if (p2.Y < p1.Y) Swap(ref p1, ref p2);
        if (p3.Y < p1.Y) Swap(ref p1, ref p3);
        if (p3.Y < p2.Y) Swap(ref p2, ref p3);

        // Split the triangle into a flat-bottom and a flat-top part
        Vector2 p4 = new Vector2(
            p1.X + (p2.Y - p1.Y) / (p3.Y - p1.Y) * (p3.X - p1.X),
            p2.Y
        );

        // Draw both parts
        FillFlatBottomTriangle(p1, p2, p4, color);
        FillFlatTopTriangle(p2, p4, p3, color);
    }

    static void FillFlatBottomTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color color)
    {
        float invslope1 = (p2.X - p1.X) / (p2.Y - p1.Y);
        float invslope2 = (p3.X - p1.X) / (p3.Y - p1.Y);

        float curx1 = p1.X;
        float curx2 = p1.X;

        for (int scanlineY = (int)p1.Y; scanlineY <= (int)p2.Y; scanlineY++)
        {
            Raylib.DrawLine((int)curx1, scanlineY, (int)curx2, scanlineY, color);
            curx1 += invslope1;
            curx2 += invslope2;
        }
    }

    static void FillFlatTopTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color color)
    {
        float invslope1 = (p3.X - p1.X) / (p3.Y - p1.Y);
        float invslope2 = (p3.X - p2.X) / (p3.Y - p2.Y);

        float curx1 = p3.X;
        float curx2 = p3.X;

        for (int scanlineY = (int)p3.Y; scanlineY >= (int)p1.Y; scanlineY--)
        {
            Raylib.DrawLine((int)curx1, scanlineY, (int)curx2, scanlineY, color);
            curx1 -= invslope1;
            curx2 -= invslope2;
        }
    }

    static void Swap(ref Vector2 a, ref Vector2 b)
    {
        Vector2 temp = a;
        a = b;
        b = temp;
    }

}