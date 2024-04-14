using nkast.Aether.Physics2D.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Physics.Aether;
public struct Physic2DSettings
{
    public Vector2 Gravity;

    public Physic2DSettings()
    {
        Gravity = new Vector2(0, 9.81f);
    }
}
