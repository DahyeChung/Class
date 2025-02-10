using DB;
using UnityEngine;


public class DataTest : MonoBehaviour
{
    protected SkillTable skillTable;
    // Id	Name	Range	CoolDown	Duration	Luciferin


    protected DialogueTable dialogueTable;
    // Id	Speaker	Text

    void Start()
    {
        #region ID·Î È£Ãâ
        var skillTable = DB.DataTables.GetSkill(1001);
        Debug.Log("Skill Name : " + skillTable.Name);
        Debug.Log("Skill Range : " + skillTable.Range);
        Debug.Log("Skill CoolDown : " + skillTable.CoolDown);
        Debug.Log("Skill Duration : " + skillTable.Duration);
        Debug.Log("Skill Luciferin : " + skillTable.Luciferin);

        skillTable = DB.DataTables.GetSkill(1002);
        Debug.Log("Skill Name : " + skillTable.Name);
        Debug.Log("Skill Range : " + skillTable.Range);
        Debug.Log("Skill CoolDown : " + skillTable.CoolDown);
        Debug.Log("Skill Duration : " + skillTable.Duration);
        Debug.Log("Skill Luciferin : " + skillTable.Luciferin);


        var dialogueTable = DB.DataTables.GetDialogue(1001);
        Debug.Log("Dialogue Speaker : " + dialogueTable.Speaker);
        Debug.Log("Dialogue Text : " + dialogueTable.Text);
        #endregion

        Debug.Log("Dialogue lines total : " + DB.DataTables.GetDialogueList().Count);

    }
}
// 한글
