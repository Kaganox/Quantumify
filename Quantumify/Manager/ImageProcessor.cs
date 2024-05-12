using Raylib_cs;

namespace Quantumify.Manager;

public class ImageProcessor : IProcessor
{
    public object Load<T>(string path)
    {
        return Raylib.LoadImage(path);
    }

    public void Unload(object texture)
    {
        Raylib.UnloadImage((Image)texture);
    }
}