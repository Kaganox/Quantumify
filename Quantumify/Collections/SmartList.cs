namespace Quantumify.Collections;

public class SmartList<T> : List<T>
{
    public T GetRandom()
    {
        return Rand.GetRandom(this);
    }
    
    public void Shuffle()
    {
        Rand.Shuffle(this);
    }
}