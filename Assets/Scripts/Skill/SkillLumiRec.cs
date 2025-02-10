using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLumiRec : SkillBase
{
 
  public override void Shot()
  {
    switch (skillDir)
    {
      case SkillDir.Normal:
        if (GetComponent<ParticleSystem>())
        {
          GetComponent<ParticleSystem>().Stop();
          GetComponent<ParticleSystem>().Play();
        }

        break;
      
      case SkillDir.Forward:

        //rigidbody.velocity = dir * skillTable.Star; //star ->speed
        break;
      
      
      default:
        
        break;
    }
    
  }

  private void OnTriggerEnter(Collider other)
  {
    
    if (other.gameObject.tag.Equals(Define.Item.Lumi.ToString()))
    {
      Debug.Log("other Name tag: " + other.gameObject.tag);
      other.gameObject.GetComponent<ItemLumi>().GetItem();
    }
    //오브젝트꺼짐
  }


  private void Update()
  {
      
  }
}
