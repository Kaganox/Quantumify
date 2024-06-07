using Newtonsoft.Json;

namespace Quantumify.Config;

public class JsonBuilder
{
    Dictionary<string,object> data;
    
    public JsonBuilder()
    {
        data = new Dictionary<string, object>();
    }
    
    
    public JsonBuilder Add(string key, object value)
    {
        if (!data.ContainsKey(key))
        {
            data[key] = value;
        }
        return this;
    }

    public JsonBuilder Set(string key, object value)
    {
        data[key] = value;
        return this;
    }
    
    public string ToJson()
    {
        return JsonConvert.SerializeObject(data);
    }
    
    public string PrettyJson()
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }
    
    public JsonBuilder Clear()
    {
        data.Clear();
        return this;
    }
    
    public JsonBuilder Remove(string key)
    {
        data.Remove(key);
        return this;
    }

    /// <summary>
    /// file will saved in Documents/{path}
    /// </summary>
    /// <param name="fileName"></param>
    public JsonBuilder Save(string fileName)
    {
        
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + fileName;

        string[] folders = path.Replace("\\", "/").Replace("//","/").Split("/");

        string newPath = "";
        
        foreach (string folder in folders)
        {
            newPath += folder;
            if (folder.Contains("."))
            {
                
                if (!File.Exists(newPath))
                {
                    File.Create(newPath).Close();
                    File.WriteAllText(newPath,this.PrettyJson());
                }
            }
            else
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
            }
            newPath += "/";
            
        }
        return this;
    }

    public JsonBuilder Load(string fileName)
    {
        data = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + fileName))!;
        return this;
    }

    public override string ToString()
    {
        return ToJson();
    }
}