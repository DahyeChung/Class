using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLogic : MonoBehaviour
{

    public float cutsceneTime;
   
    public string sceneTransitionName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endCutscene(cutsceneTime));
    }

    // Update is called once per frame
    void Update()
    {
       
    }



    public IEnumerator endCutscene(float cutsceneTime)
    {

        yield return new WaitForSeconds(cutsceneTime);

        SceneManager.LoadScene(sceneTransitionName);

    }
}
