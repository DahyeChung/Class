using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLumi : ItemBase
{
    public string getEffect;
    public string nextEffect;
    public float nextEffectDelayTime = 1f;
    public override void GetItem()
    {
        this.gameObject.SetActive(false);
        
        if(!string.IsNullOrEmpty(getEffect))
            PoolMananger.instance.GetSpawn(getEffect, transform.position, Quaternion.identity);

        if(!string.IsNullOrEmpty(nextEffect))
            Invoke("DelayLumiSpace", nextEffectDelayTime);
    }


    public void DelayLumiSpace()
    {
        var ob = PoolMananger.instance.GetSpawn(nextEffect, transform.position, Quaternion.identity);

    }
}
