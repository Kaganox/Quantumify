using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine;

public class KaganoRandom
{
    private static Random random = new Random();
    
    public static int seed { get; private set; }
    
    /// <summary>
    /// Generates a int between min and max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RangeInt(int min, int max)
    {
        return random.Next(min, max+1);
    }

    
    /// <summary>
    /// set to a specific seed
    /// </summary>
    /// <param name="seed"></param>
    public static void SetSeed(int seed)
    {
        random = new Random(seed);
    }

    
    /// <summary>
    /// Randomize the seed
    /// </summary>
    public static void Randomize()
    {
        int seed = new Random().Next(int.MinValue, int.MaxValue);
        random = new Random(seed);
    }
    
    /// <summary>
    /// Get Random element of List
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T RandomOf<T>(List<T> list)
    {
        return list[RangeInt(0, list.Count - 1)];
    }
    
    /// <summary>
    /// Get Random element of Array
    /// </summary>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T RandomOf<T>(T[] array)
    {
        return RandomOf<T>(array.ToList());
    }

    
    /// <summary>
    /// Returns true if the given chance arrive
    /// </summary>
    /// <returns></returns>
    public static bool Chanche(int chance, int max = 100)
    {
        return chance <= RangeInt(0, max);
    }

    
    /// <summary>
    /// set the privot chance by the key
    /// </summary>
    /// <param name="map"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ChanchesMap<T>(Dictionary<int, T> map)
    {
        int total = 0;
        int lastIndex = 0;
        for(int i = 0; i < map.Count; i++)
        {
            total += map.ElementAt(i).Key;
            if (i == map.Count - 1)
            {
                lastIndex = i;
            }
        }
        int random = RangeInt(0, total);
        int current = 0;
        foreach (KeyValuePair<int, T> entry in map)
        {
            if (random <= entry.Key)
            {
                return entry.Value;
            }
            current += entry.Key;
        }
        return map[lastIndex];
    }
}
