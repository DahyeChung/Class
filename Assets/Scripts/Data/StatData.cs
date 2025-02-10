using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StatData : MonoBehaviour
{

}
/* COMMENTED OUT TO BE ABLE TO FIX COMPILATION ERRORS
 * 
[Serializable, XmlRoot("ArrayOfStatData")]


public class StatDataLoader : ILoader<int, StatData>
{

   [XmlElement("StatData")]
   public List<StatData> _statData = new List<StatData>();

   public Dictionary<int, StatData> MakeDict()
   {
       Dictionary<int, StatData> dic = new Dictionary<int, StatData>();

       foreach (StatData data in _statData)
           dic.Add(data.ID, data);

       return dic;
   }

   public bool Validate()
   {
       return true;
   }

}
   */