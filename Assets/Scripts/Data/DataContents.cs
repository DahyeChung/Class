using System;
using System.Collections.Generic;

namespace Data
{
    #region LevelData
    [Serializable]
    public class LevelData
    {
        public int Level;
        public int TotalExp;
        public int RequiredExp;
    }

    [Serializable]
    public class LevelDataLoader : ILoader<int, LevelData>
    {
        public List<LevelData> levels = new List<LevelData>();
        public Dictionary<int, LevelData> MakeDict()
        {
            Dictionary<int, LevelData> dict = new Dictionary<int, LevelData>();
            foreach (LevelData levelData in levels)
                dict.Add(levelData.Level, levelData);
            return dict;
        }
    }
    #endregion



    #region StageData
    [Serializable]
    public class StageData
    {
        public int StageIndex = 1;
        public string StageName;
        public int StageLevel = 1;
        public string MapName;
        public int StageSkill;

        public int FirstWaveCountValue;
        public int FirstWaveClearRewardItemId;
        public int FirstWaveClearRewardItemValue;

        public int SecondWaveCountValue;
        public int SecondWaveClearRewardItemId;
        public int SecondWaveClearRewardItemValue;

        public int ThirdWaveCountValue;
        public int ThirdWaveClearRewardItemId;
        public int ThirdWaveClearRewardItemValue;

        public int ClearReward_Gold;
        public int ClearReward_Exp;
        public string StageImage;
        public List<int> AppearingMonsters;
    }
    public class StageDataLoader : ILoader<int, StageData>
    {
        public List<StageData> stages = new List<StageData>();

        public Dictionary<int, StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            foreach (StageData stage in stages)
                dict.Add(stage.StageIndex, stage);
            return dict;
        }
    }
    #endregion


}