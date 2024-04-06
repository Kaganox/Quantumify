using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Config;

public interface SaveAble
{
    public NBT Write(NBT nbt);
    public void Read(NBT nbt);
}
