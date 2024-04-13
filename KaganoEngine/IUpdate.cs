namespace KaganoEngine;

public interface IUpdate
{
    public void Update();
    public void FixedUpdate();
    public void AfterUpdate();
    public void Draw();
}