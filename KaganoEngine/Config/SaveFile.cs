using KaganoEngine.Nodes;
using KaganoEngine.Scenes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine.Config;

public class SaveFile
{
    private List<SaveAble> _saveAbles = new List<SaveAble>();
    public static Dictionary<string, Type> types = new Dictionary<string, Type>()
    {
        {"node", typeof(Node)},
        {"node2d", typeof(Node2D)},
        {"node3d", typeof(Node3D)},
    };
    List<NBT> _nbts = new List<NBT>();
    bool _encrypt = true;
    string _encryptKey = "b14ca5898a4e4133bbce2ea2315a1916";
    string path;
    public SaveFile(string path) { 
        this.path = path;
    }

    public void Read()
    {
        CreateFile();
        string[] content = File.ReadAllLines(path);
        foreach (string line in content)
        {
            NBT nbt = Newtonsoft.Json.JsonConvert.DeserializeObject<NBT>(DecryptString(line))!;
            if(nbt == null) continue;
            JsonToNode(nbt);
        }
    }
    public void JsonToNode(NBT nbt)
    {
        Type typeData = types[nbt.GetString("type")!];
        Node node = (Node)Activator.CreateInstance(typeData)!;//JsonConvert.DeserializeObject(json, typeData)!;
        if(node is SaveAble saveAble) { saveAble.Read(nbt); }
        //SceneManager.scene2d?.nodes.Add(node);
    }

    public bool Exists()
    {
        return File.Exists(path);
    }

    public void Write(params SaveAble[] saveAbles)
    {
        CreateFile();
        List<string> strings = new List<string>();


        List<SaveAble> allSaveAbles = saveAbles.ToList().Concat(_saveAbles).ToList();
        foreach (SaveAble saveeAble in allSaveAbles)
        {
            NBT nbt = new NBT();
            nbt.SetString("type", saveeAble.GetType().ToString().ToLower());
            saveeAble.Write(nbt);
            strings.Add(EncryptString(Newtonsoft.Json.JsonConvert.SerializeObject(nbt)));
        }
        File.WriteAllLines(path, strings);
    }

    public static void RegisterTyp<T>()
    {
        types.Add(typeof(T).ToString().ToLower(), typeof(T));
    }
    public static void RegisterTyp(params Type[] types)
    {
        types.ToList().ForEach(x => RegisterTyp(x));
    }


    public void AddSaveAble(SaveAble saveAble)
    {
        _saveAbles.Add(saveAble);
    }

    public void CreateFile()
    {
        if(!File.Exists(path)) File.Create(path).Close();
    }

    //TODO create crypto class for this code
    public string EncryptString(string text)
    {
        if (!this._encrypt)
        {
            return text;
        }

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(this._encryptKey);
        aes.IV = new byte[16];

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(text);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    /**
     * Decrypt string
     */
    public string DecryptString(string text)
    {
        if (!this._encrypt)
        {
            return text;
        }

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(this._encryptKey);
        aes.IV = new byte[16];

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(Convert.FromBase64String(text));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }

    public string Build()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
