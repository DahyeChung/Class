using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLumiSpace : MonoBehaviour
{
    public float duration;
    private void OnEnable()
    {
        if(duration > 0f)
            StartCoroutine(Duration());
    }

    public virtual IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
      
        this.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
    
        if (other.gameObject.tag.Equals(Define.UnitType.Player.ToString()))
        {
            if(other.gameObject.GetComponent<PlayerInfo>())
                other.gameObject.GetComponent<PlayerInfo>().isHide = true;
            Debug.Log("other Name : " + other.gameObject.name);
        }
        //오브젝트꺼짐
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Define.UnitType.Player.ToString()))
        {
            if(other.gameObject.GetComponent<PlayerInfo>())
                other.gameObject.GetComponent<PlayerInfo>().isHide = false;
            Debug.Log("other Name : " + other.gameObject.name);
        }
    }
}
