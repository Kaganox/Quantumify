using Raylib_cs;

namespace Quantumify.Nodes;

public class Timer : Node
{
    
    public delegate void TimerRanOut();
    public TimerRanOut? OnTimerRanOut;
    
    public float TimeLeft { get; private set; }
    public bool Running { get; private set; }
    public bool Repeat;
    public float Time;

    public Timer()
    {
        Repeat = false;
        Time = 10;
    }


    public override void Update()
    {
        base.Update();
        if (Running)
        {
            TimeLeft -= DeltaTime;
            if (TimeLeft <= 0)
            {
                OnTimerRanOut?.Invoke();
                if (Repeat)
                {
                    Start();
                }
                else
                {
                    Running = false;
                }
            }
        }
    }

    public void Start()
    {
        TimeLeft = Time;
        Running = true;
    }
    
    public void Stop()
    {
        Running = false;
    }
}