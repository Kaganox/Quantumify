using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Nodes;

public class Tween : Node
{
    private List<TweenProperty> _tweenElements = new List<TweenProperty>();

    public float Past;
    public bool Running;
    public bool Loop;
    
    public override void Update()
    {
        base.Update();
        if (Running)
        {
            float minTime = _tweenElements.Max(x => x.Time+x.Delay);

            if (Past >= minTime)
            {
                Past = 0;
                if (!Loop)
                {
                    Running = false;
                    return;
                }
            }

            _tweenElements.ForEach(property =>
            {
                
                
                if (Past >= property.Delay && Past <= property.Time + property.Delay)
                {
                    property.Action((Past - property.Delay) / (property.Time));
                }
            });
            
            Past += DeltaTime;
        }
    }
    
    public Tween SetProperty(Action<float> lerp, float time, float delay = 0)
    {
        TweenProperty property = new TweenProperty(time, delay,lerp);
        _tweenElements.Add(property);
        return this;
        /*    (past) =>
        {
            x = Raymath.Lerp(start, end, past);
        }*/
    }
    
    
    public class TweenProperty(float time, float delay, Action<float> action)
    {
        public readonly float Delay = delay;
        public readonly float Time = time;
        public Action<float> Action = action;
    }
}