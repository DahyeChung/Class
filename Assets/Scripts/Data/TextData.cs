using System;
using System.Collections.Generic;
using System.Xml.Serialization;


public class TextData
{
    [XmlAttribute]
    public int ID { get; set; }
    [XmlAttribute]
    public string kor { get; set; }
    [XmlAttribute]
    public string eng { get; set; }
}

[Serializable, XmlRoot("ArrayOfTextData")]
public class TextDataLoader : ILoader<int, TextData>
{
    [XmlElement("TextData")]
    public List<TextData> _textData = new List<TextData>();

    public Dictionary<int, TextData> MakeDict()
    {
        Dictionary<int, TextData> dic = new Dictionary<int, TextData>();

        foreach (TextData data in _textData)
            dic.Add(data.ID, data);

        return dic;
    }

    public bool Validate()
    {
        return _textData.Count > 0; // check if there is any data

        // return true;
    }
}