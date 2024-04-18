using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantumify.Manager;

public class ModelProcessor : IProcessor
{
    public object Load<T>(string path)
    {
        return Raylib.LoadModel(path);
    }

    public void Unload(object texture)
    {
        Raylib.UnloadModel((Model)texture);
    }
}
