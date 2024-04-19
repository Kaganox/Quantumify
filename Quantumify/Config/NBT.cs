using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Quantumify.Config;

public class NBT
{
    public Dictionary<string, object> data = new Dictionary<string, object>();

    public T? Get<T>(string key)
    {
        return HasKey(key) ?
               Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data[key].ToString()!) :
               LogError<T>(key, "object");
    }

    public int? GetInt(string key) => HasKey(key) ? int.Parse(data[key].ToString()!) : LogError<int>(key, "int");
    public long? GetLong(string key) => HasKey(key) ? long.Parse(data[key].ToString()!) : LogError<long>(key, "long");
    public float? GetFloat(string key) => HasKey(key) ? float.Parse(data[key].ToString()!) : LogError<float>(key, "float");
    public string? GetString(string key) =>  HasKey(key) ? data[key].ToString()! : LogError<string>(key, "string");
    public bool? GetBool(string key) => HasKey(key) ? bool.Parse(data[key].ToString()!) : LogError<bool>(key, "bool");
    public double? GetDouble(string key) => HasKey(key) ? double.Parse(data[key].ToString()!) : LogError<double>(key, "double");
    public List<T>? GetList<T>(string key) => HasKey(key) ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data[key].ToString()!) : LogError<List<T>>(key, "list");
    public Dictionary<string, T>? GetDictionary<T>(string key)=> HasKey(key) ? Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, T>>(data[key].ToString()!) : LogError<Dictionary<string, T>>(key, "dictionary");
    public Vector3  GetVector3(string key) => HasKey(key) ? Newtonsoft.Json.JsonConvert.DeserializeObject<Vector3>(data[key].ToString()!) : LogError<Vector3>(key, "vector3");


    public void SetVector3(string key, Vector3 value) { data.Add(key, Newtonsoft.Json.JsonConvert.SerializeObject(value)); }
    public void SetString(string key, string value) { data.Add(key, value); }
    public void SetInt(string key, int value) { data.Add(key, value); }
    public void SetLong(string key, long value) { data.Add(key, value); }
    public void SetFloat(string key, float value) { data.Add(key, value); }
    public void SetDouble(string key, double value) { data.Add(key, value); }
    public void SetBool(string key, bool value) { data.Add(key, value); }
    public void SetList<T>(string key, List<T> value) { Set<List<T>>(key, value); }
    public void SetDictionary<T>(string key, Dictionary<string, T> value) => Set<Dictionary<string, T>>(key, value); 
    public void Set<T>(string key, T value) { data.Add(key, Newtonsoft.Json.JsonConvert.SerializeObject(value)); }

    public bool HasKey(string key)
    {
        return data.ContainsKey(key);
    }

    private T LogError<T>(string key,string type)
    {
        Logger.Error($"Key {key} not found {type} in NBT.");
        return default!;
    }
}
