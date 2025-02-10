using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerExcelData
{

}

public class PlayerData : MonoBehaviour
{

}
/* COMMENTED OUT TO BE ABLE TO FIX COMPILATION ERRORS

[Serializable, XmlRoot("ArrayOfPlayerData")]
public class PlayerDataLoader : ILoader<int, PlayerData>
{
   [XmlElement("PlayerData")]
   public List<PlayerData> _characterDatas = new List<PlayerData>();

   public Dictionary<int, PlayerData> MakeDict()
   {
       Dictionary<int, PlayerData> dic = new Dictionary<int, PlayerData>();

       foreach (PlayerData data in _characterDatas)
           dic.Add(data.ID, data);

       return dic;
   }


   public bool Validate()
   {
       return true;
   }
}
*/