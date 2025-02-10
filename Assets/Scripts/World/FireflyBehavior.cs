using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            giveLuciferin();
        }



    }


    public void giveLuciferin()
    {
        if (PlayerInfo.instance.luciferin + 1 > PlayerInfo.instance.maxLuciferin)
        {
            //do nothing, do not collect object

            //do a little animation?
        }
        else
        {
            PlayerInfo.instance.luciferin++; //give luciferin


            //disable sphere collider
            Collider col = GetComponent<SphereCollider>();

            col.enabled = false;

            //start aniamtion

            StartCoroutine(SelfDestruct(1f)); //make object destroy after a certain time
        }
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
