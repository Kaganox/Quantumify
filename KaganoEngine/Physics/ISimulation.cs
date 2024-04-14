using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Physics;

public interface ISimulation
{
    void Step(float timeStep);
}
