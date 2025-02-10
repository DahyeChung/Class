using UnityEngine;

public class InitializeEasySpreadsheet : MonoBehaviour
{
    // 싱글톤으로 

    private readonly DB.DataTables _dataTables = new DB.DataTables();

    private void Awake()
    {
        // Load all data when app starts.
        _dataTables.Load(new EasySpreadsheet.EsDataLoaderResources());

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        var skillData = DB.DataTables.GetSkill(1); // ID
        var diaglogueData = DB.DataTables.GetDialogue(1); // ID

        // var skillData = DB.DataTables.GetSkillList();

        // Get Skill = ID를 가져옴 
        //if (heroData != null)
        //{
        //    var icon = heroData.Icon;
        //}
    }
}