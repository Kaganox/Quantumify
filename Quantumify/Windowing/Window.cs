using Raylib_cs;

namespace Quantumify.Windowing;

public class Window
{
    /// <inheritdoc cref="Raylib.SetWindowTitle"/>
    public static void SetTitle(string title) => Raylib.SetWindowTitle(title);
    /// <inheritdoc cref="Raylib.SetWindowIcon"/>
    public static void SetIcon(Image icon) => Raylib.SetWindowIcon(icon);
    /// <inheritdoc cref="Raylib.SetWindowSize"/>
    public static void SetSize(int width, int height) => Raylib.SetWindowSize(width, height);
    /// <inheritdoc cref="Raylib.SetWindowPosition"/>
    public static void SetPosition(int x, int y) => Raylib.SetWindowPosition(x, y);
    /// <inheritdoc cref="Raylib.SetWindowMonitor"/>
    public static void SetMonitor(int monitor) => Raylib.SetWindowMonitor(monitor);
    /// <inheritdoc cref="Raylib.SetWindowMinSize"/>
    public static void SetMinSize(int width, int height) => Raylib.SetWindowMinSize(width, height);
    /// <inheritdoc cref="Raylib.SetWindowMaxSize"/>
    public static void SetMaxSize(int width, int height) => Raylib.SetWindowMaxSize(width, height);

    
    /// <inheritdoc cref="Raylib.CloseWindow"/>
    public static void Close() => Raylib.CloseWindow();

    
    /// <inheritdoc cref="Raylib.GetScreenWidth"/>
    public static int GetWidth() => Raylib.GetScreenWidth();
    /// <inheritdoc cref="Raylib.GetScreenHeight"/>
    public static int GetHeight() => Raylib.GetScreenHeight();
}
