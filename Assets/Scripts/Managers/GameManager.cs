using Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static GameData;

[Serializable]
public class StageClearInfo
{
    // TODO : Decide Stage Clear Condition
    public int StageIndex = 1;
    public bool isKilledBoss = false;
    public bool isSpareBoss = false;
    public bool isClear = false;
}

[Serializable]
public class MissionInfo
{
    public int Progress;
    public bool IsRewarded;
}


[Serializable]
public class GameData
{
    public int UserLevel = 1;
    public string UserName = "Player";
    // TODO : Add character data
    // public int Stamina = Define.MAX_STAMINA;

    public List<Character> Characters = new List<Character>();
    public ContinueData ContinueInfo = new ContinueData();
    public StageData CurrentStage = new StageData();
    public Dictionary<int, StageClearInfo> DicStageClearInfo = new Dictionary<int, StageClearInfo>();

    // Item?
    // Equipment Inventory?
    // Mission Data?



    public bool BGMOn = true;
    public bool EffectSoundOn = true;

    [Serializable]
    public class ContinueData
    {
        // TODO : what condition will allow isContinue
        // public bool isContinue { get { return SavedBattleSkill.Count > 0; } } 

        // TODO : Add any player data that needs to be saved
        public int PlayerDataId;
        public float Hp;
        public float MaxHp;
        public float MaxHpBonusRate = 1;
        public float HealBonusRate = 1;
        public float HpRegen;
        public float Atk;
        public float AttackRate = 1;
        public float Def;
        public float DefRate;
        public float MoveSpeed;
        public float MoveSpeedRate = 1;
        public float TotalExp;
        public int Level = 1;
        public float Exp;
        public float CriRate;
        public float CriDamage = 1.5f;
        public float DamageReduction;

        // public Dictionary<Define.SkillType, int> SavedBattleSkill = new Dictionary<Define.SkillType, int>();

        public int WaveIndex;
        public void Clear()
        {
            PlayerDataId = 0;
            Hp = 0f;
            MaxHp = 0f;
            MaxHpBonusRate = 1f;
            HealBonusRate = 1f;
            HpRegen = 0f;
            Atk = 0f;
            AttackRate = 1f;
            Def = 0f;
            DefRate = 0f;
            MoveSpeed = 0f;
            MoveSpeedRate = 1f;
            TotalExp = 0f;
            Level = 1;
            Exp = 0f;
            CriRate = 0f;
            CriDamage = 1.5f;
            DamageReduction = 0f;
        }
    }
}
public class GameManager
{
    #region GameData Updatable
    public GameData _gameData = new GameData();

    // Stage Clear Info Dictionary, Update Achievement when stage clear and call save game method
    public Dictionary<int, StageClearInfo> DicStageClearInfo
    {
        get { return _gameData.DicStageClearInfo; }
        set
        {
            _gameData.DicStageClearInfo = value;
            // Managers.Achievement.StageClear(); // update achievement
            SaveGame(); // auto save
        }
    }
    public ContinueData ContinueInfo
    {
        get { return _gameData.ContinueInfo; }
        set
        {
            _gameData.ContinueInfo = value;
        }
    }

    public StageData CurrentStageData
    {
        get { return _gameData.CurrentStage; }
        set { _gameData.CurrentStage = value; }
    }


    // Character (Player, NPC, Boss)
    public List<Character> Characters
    {
        get { return _gameData.Characters; }
        set
        {
            _gameData.Characters = value;
            EquipInfoChanged?.Invoke();
        }
    }

    public int UserLevel
    {
        get { return _gameData.UserLevel; }
        set { _gameData.UserLevel = value; }
    }
    public string UserName
    {
        get { return _gameData.UserName; }
        set { _gameData.UserName = value; }
    }



    // TODO : Map class to load? Should I add?
    // public Map CurrentMap { get; set; }

    #region Sound Option
    public bool BGMOn
    {
        get { return _gameData.BGMOn; }
        set
        {
            if (_gameData.BGMOn == value)
                return;
            _gameData.BGMOn = value;
            if (_gameData.BGMOn == false)
            {
                Managers.Sound.Stop(Define.Sound.Bgm);
            }
            else
            {
                // TODO: Multiple BGMs setting : Sample_TitleBGM_Josh
                string name = "BGM/Sample_TitleBGM_Josh";
                if (Managers.Scene.CurrentScene.SceneType == Define.Scene.GameScene)
                    name = "BGM/Sample_TitleBGM";

                Managers.Sound.Play(Define.Sound.Bgm, name);
            }
        }
    }

    public bool EffectSoundOn
    {
        get { return _gameData.EffectSoundOn; }
        set { _gameData.EffectSoundOn = value; }
    }



    #endregion
    #endregion



    #region Action
    public event Action<Vector3> OnMoveDirChanged;
    public event Action EquipInfoChanged;
    // public event Action OnResourcesChagned;
    public Action OnMonsterDataUpdated;
    #endregion

    public bool IsLoaded = false;
    public bool IsGameEnd = false;
    // public CameraController CameraController { get; set; }

    public void Init()
    {
        _path = Application.persistentDataPath + "/SaveData.json";

        if (LoadGame())
            return;

        #region Unit Init
        //PlayerPrefs.SetInt("ISFIRST", 1);

        //Character character = new Character();
        //character.SetInfo(CHARACTER_DEFAULT_ID);
        //character.isCurrentCharacter = true;

        //Characters = new List<Character>();
        //Characters.Add(character);
        #endregion

        //CurrentStageData = Managers.Data.StageDic[1];

        //foreach (Data.StageData stage in Managers.Data.StageDic.Values)
        //{
        //    StageClearInfo info = new StageClearInfo
        //    {
        //        StageIndex = stage.StageIndex,
        //        MaxWaveIndex = 0,
        //        isOpenFirstBox = false,
        //        isOpenSecondBox = false,
        //        isOpenThirdBox = false,
        //    };
        //    _gameData.DicStageClearInfo.Add(stage.StageIndex, info);
        //}

        IsLoaded = true;
        SaveGame();
    }



    #region InGame

    public void GameOver()
    {
        IsGameEnd = true;
        //Player.StopAllCoroutines();
        //Managers.UI.ShowPopupUI<UI_GameoverPopup>().SetInfo();
    }

    //public (int hp, int atk) GetCurrentChracterStat()
    //{
    //    int hpBonus = 0;
    //    int AtkBonus = 0;
    //    var (equipHpBonus, equipAtkBonus) = GetEquipmentBonus();

    //    Character ch = CurrentCharacter;

    //    hpBonus = (equipHpBonus);
    //    AtkBonus = (equipAtkBonus);

    //    return (hpBonus, AtkBonus);
    //}


    #endregion


    #region Save&Load
    string _path;

    public void SaveGame()
    {
        //if (Player != null)
        //{
        //    _gameData.ContinueInfo.SavedBattleSkill = Player.Skills?.SavedBattleSkill;
        //    _gameData.ContinueInfo.SavedSupportSkill = Player.Skills?.SupportSkills;
        //}

        // Convert Ojbect to Json
        string jsonStr = JsonConvert.SerializeObject(_gameData);
        File.WriteAllText(_path, jsonStr);
    }

    public bool LoadGame()
    {
        if (PlayerPrefs.GetInt("ISFIRST", 1) == 1)
        {
            string path = Application.persistentDataPath + "/SaveData.json";
            if (File.Exists(path))
                File.Delete(path);
            return false;
        }

        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonConvert.DeserializeObject<GameData>(fileStr);
        if (data != null)
            _gameData = data;

        // TODO : Add any additional data loading here ( Equipment, etc)

        IsLoaded = true;
        return true;
    }

    public void ClearContinueData()
    {
        ContinueInfo.Clear();
        //Current Stage = 0;
        SaveGame();
    }


    #endregion
}

