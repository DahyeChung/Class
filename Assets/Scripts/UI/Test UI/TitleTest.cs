using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleTest : MonoBehaviour
{

    public string firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("bab");
    }

    public void Startgame()
    {
        Debug.Log("goo");
       SceneManager.LoadScene(firstLevel);
    }
}
