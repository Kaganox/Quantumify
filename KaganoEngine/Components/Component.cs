using KaganoEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Components;

public abstract class Component
{
    protected internal Node node { get; internal set; }

    public Component(Node node)
    {
        this.node = node;
    }

    public virtual void Update()
    {
        
    }

    public virtual void AfterUpdate()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Draw()
    {

    }
}
