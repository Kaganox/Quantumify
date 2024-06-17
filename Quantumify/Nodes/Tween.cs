using Quantumify.Windowing;
using Raylib_cs;

namespace Quantumify.Nodes;

public class Tween : Node
{
    private List<TweenProperty> _tweenElements = new List<TweenProperty>();

    public float Past;
    public bool Running;
    public bool Loop;

    //public delegate void Property(float past,ref float value);
    
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
                    float useless = 0;
                    //property.Lerp.Invoke((Past - property.Delay) / (property.Time),ref useless); 
                    property.Action?.Invoke((Past - property.Delay) / (property.Time));
                }
            });
            
            Past += DeltaTime;
        }
    }
    
    public Tween SetProperty(Action<float> lerp, float time, float delay = 0)
    {
        Action<float, float> action = (past,value) => lerp.Invoke(past);
        TweenProperty property = new TweenProperty(time, delay,lerp);
        _tweenElements.Add(property);
        return this;
        /*    (past) =>
        {
            x = Raymath.Lerp(start, end, past);
        }*/
    }
    
    public Tween SetProperty(ref float value, float time, float delay, float start, float end)
    {
        TweenProperty property = new TweenProperty(time, delay,null);
        //property.Lerp += (float past, ref float f) => Lerp(past, ref value, start, end);
        _tweenElements.Add(property);
        return this;
        /*    (past) =>
        {
            x = Raymath.Lerp(start, end, past);
        }*/
    }
    
    private void Lerp(float past, ref float value,float start, float end)
    {
        value = Raymath.Lerp(start,end, past);
    }
    
    
    public class TweenProperty(float time, float delay, Action<float>? action = default)
    {
        public readonly float Delay = delay;
        public readonly float Time = time;
        public Action<float>? Action = action;

        //public Property Lerp;
    }
}