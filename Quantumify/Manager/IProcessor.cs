using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantumify.Manager;

public interface IProcessor
{
    object Load<T>(string path);
    void Unload(object texture);
}
