using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// 이 클래스는 대화 이벤트의 엑셀 데이터 형식을 정의합니다.
// 각 필드는 XML 속성으로 직렬화되어 저장/로드됩니다.
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

// 이 클래스는 대화 이벤트 데이터를 정의합니다.
// 질문과 해당 답변들을 묶어서 관리합니다.
public class DialogueEventData
{
    [XmlAttribute]
    public int questionID;
    [XmlAttribute]
    public int enemyType; // 상사한테?
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
[Serializable, XmlRoot("ArrayOfDialogueEventData")] // XML 루트 태그 정의
public class DialogueEventDataLoader : ILoader<int, DialogueEventData>
{
    [XmlElement("DialogueEventData")] // XML 태그 이름 정의
    public List<DialogueEventData> _dialogueEventData = new List<DialogueEventData>(); // 대화 이벤트 데이터 리스트

    public Dictionary<int, DialogueEventData> MakeDict()
    {
        // 질문 ID를 키로, DialogueEventData를 값으로 하는 딕셔너리를 생성합니다.
        Dictionary<int, DialogueEventData> dic = new Dictionary<int, DialogueEventData>();

        foreach (DialogueEventData data in _dialogueEventData)
            dic.Add(data.questionID, data); // 각 질문 ID와 데이터를 추가

        return dic;
    }

    /*
        XML 데이터를 로드하여 DialogueEventData 객체 리스트를 생성합니다.
        MakeDict() 메서드를 통해 질문 ID를 키로 하는 딕셔너리를 생성하여 빠른 검색이 가능하도록 합니다. 
     */

    // 데이터 유효성 검사 메서드
    public bool Validate()
    {
        return true;
    }
}