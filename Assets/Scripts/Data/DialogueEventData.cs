using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// �� Ŭ������ ��ȭ �̺�Ʈ�� ���� ������ ������ �����մϴ�.
// �� �ʵ�� XML �Ӽ����� ����ȭ�Ǿ� ����/�ε�˴ϴ�.
public class DialogueEventExcelData
{
    [XmlAttribute]
    public int questionID;
    [XmlAttribute]
    public int answerID;
    [XmlAttribute]
    public int resultID;
    [XmlAttribute]
    public int difWorkAbility;
    [XmlAttribute]
    public int difLikability;
    [XmlAttribute]
    public int difLuck;
    [XmlAttribute]
    public int difStress;
    [XmlAttribute]
    public int difMoney;
    [XmlAttribute]
    public int difBlock;
    [XmlAttribute]
    public int enemyType;
}

// �� Ŭ������ ��ȭ �̺�Ʈ �����͸� �����մϴ�.
// ������ �ش� �亯���� ��� �����մϴ�.
public class DialogueEventData
{
    [XmlAttribute]
    public int questionID;
    [XmlAttribute]
    public int enemyType; // �������?
    [XmlArray]
    public List<DialogueAnsData> answers = new List<DialogueAnsData>();
}

public class DialogueAnsData
{
    [XmlAttribute]
    public int answerID;
    [XmlAttribute]
    public int resultID;
    [XmlAttribute]
    public int difWorkAbility;
    [XmlAttribute]
    public int difLikeability;
    [XmlAttribute]
    public int difLuck;
    [XmlAttribute]
    public int difStress;
    [XmlAttribute]
    public int difMoney;
    [XmlAttribute]
    public int difBlock;
}
[Serializable, XmlRoot("ArrayOfDialogueEventData")] // XML ��Ʈ �±� ����
public class DialogueEventDataLoader : ILoader<int, DialogueEventData>
{
    [XmlElement("DialogueEventData")] // XML �±� �̸� ����
    public List<DialogueEventData> _dialogueEventData = new List<DialogueEventData>(); // ��ȭ �̺�Ʈ ������ ����Ʈ

    public Dictionary<int, DialogueEventData> MakeDict()
    {
        // ���� ID�� Ű��, DialogueEventData�� ������ �ϴ� ��ųʸ��� �����մϴ�.
        Dictionary<int, DialogueEventData> dic = new Dictionary<int, DialogueEventData>();

        foreach (DialogueEventData data in _dialogueEventData)
            dic.Add(data.questionID, data); // �� ���� ID�� �����͸� �߰�

        return dic;
    }

    /*
        XML �����͸� �ε��Ͽ� DialogueEventData ��ü ����Ʈ�� �����մϴ�.
        MakeDict() �޼��带 ���� ���� ID�� Ű�� �ϴ� ��ųʸ��� �����Ͽ� ���� �˻��� �����ϵ��� �մϴ�. 
     */

    // ������ ��ȿ�� �˻� �޼���
    public bool Validate()
    {
        return true;
    }
}