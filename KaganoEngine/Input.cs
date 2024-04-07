using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine;

public class Input
{

    /// <inheritdoc cref="Raylib.IsKeyDown"/>
    public static bool IsKeyDown(KeyboardKey key) => Raylib.IsKeyDown(key);


    /// <inheritdoc cref="Raylib.IsKeyUp"/>
    public static bool IsKeyUp(KeyboardKey key) => Raylib.IsKeyUp(key);

    /// <inheritdoc cref="Raylib.IsKeyPressed"/>
    public static bool IsKeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);

    /// <inheritdoc cref="Raylib.IsKeyReleased"/>
    public static bool IsKeyReleased(KeyboardKey key) => Raylib.IsKeyReleased(key);


    /// <inheritdoc cref="Raylib.IsKeyPressedRepeat"/>
    public static bool IsKeyPressedRepeat(KeyboardKey key) => Raylib.IsKeyPressedRepeat(key);


    /// <summary>
    /// Detect of four keys (default W,A,S,D)
    /// </summary>
    /// <returns>Return a movement Vector</returns>
    public static Vector3 Vector2Input(KeyboardKey up = KeyboardKey.W, KeyboardKey down = KeyboardKey.S, KeyboardKey left = KeyboardKey.A, KeyboardKey right = KeyboardKey.D)
    {
        Vector3 movement = new Vector3();

        movement.X = (Raylib.IsKeyDown(right) && right != KeyboardKey.Null ? 1 : 0) - (Raylib.IsKeyDown(left) && left != KeyboardKey.Null ? 1 : 0);
        movement.Y = (Raylib.IsKeyDown(down) && down != KeyboardKey.Null ? 1 : 0) - (Raylib.IsKeyDown(up) && up != KeyboardKey.Null ? 1 : 0);

        return movement;
    }
}
