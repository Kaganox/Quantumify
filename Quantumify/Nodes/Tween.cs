namespace Quantumify.Nodes;

public class Tween : Node
{
    private List<Action> _actions = new List<Action>();
    
    public override void Update()
    {
        base.Update();
        _actions.ForEach(a => a.Invoke());
    }
    
    public void SetProperty<T>(T start, T end, float time)
    {
        _actions.Add(() =>
        {
            Lerp(Convert.ToDouble(start),  Convert.ToDouble(end), time);
            
        });
    }
    
    public double Lerp(double property, double value, float time)
    {
        return property + (value - property) * time;
    }
}