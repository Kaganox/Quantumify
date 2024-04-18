namespace KaganoEngine;

public interface IUpdate
{
    protected virtual int Priority()
    {
        return 0;
    }
    
    protected virtual void Update() {}
    protected virtual void FixedUpdate() {}
    protected virtual void AfterUpdate() {}
    protected virtual void Draw() {}
}