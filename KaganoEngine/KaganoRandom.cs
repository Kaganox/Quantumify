using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine;

public class KaganoRandom
{
    Random random = new Random();
    public int RangeInt(int min, int max)
    {
        return random.Next(min, max+1);
    }

    public void SetSeed()
    {

    }
}
