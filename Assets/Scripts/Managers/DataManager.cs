using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
    // bool Validate();
}

public class DataManager
{ // COMMENTED OUT TO BE ABLE TO FIX COMPILATION ERRORS

    public Dictionary<int, TextData> Texts { get; private set; } = new Dictionary<int, TextData>();

    //public Dictionary<int, DialogueEventData> Dialogues { get; private set; }


    public void Init()
    {

        Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDict();



        // TODO : Multiple ending
        // Endings = LoadXml<EndingDataLoader, int, EndingData>("EndingData").MakeDict();

        //Dialogues = LoadXml<DialogueEventDataLoader, int, DialogueEventData>("DialogueEventData").MakeDict();

    }

    #region XML
    private Value LoadSingleXml<Value>(string name)
    {
        XmlSerializer xs = new XmlSerializer(typeof(Value));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Value)xs.Deserialize(stream);
    }

    private Loader LoadXml<Loader, Key, Value>(string name) where Loader : ILoader<Key, Value>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/Xml/" + name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Loader)xs.Deserialize(stream);
    }
    #endregion

    #region JSON
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }
    #endregion

}
