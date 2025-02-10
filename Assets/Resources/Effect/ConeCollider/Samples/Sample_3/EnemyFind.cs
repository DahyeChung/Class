using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyFind : MonoBehaviour {
    
    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
	    if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
	    {
		    Debug.Log("other Name : " + other.gameObject.name);
	    }
    }

    void OnTriggerExit(Collider other)
    {
      
    }
}
