using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject Press1;
    public GameObject Press2;
    public GameObject Press3;
    public GameObject Press4;
    public GameObject Press5;
    // Start is called before the first frame update

    private void Start()
    {
        Press1.SetActive(true);
        Press2.SetActive(false);
        Press3.SetActive(false);
        Press4.SetActive(false);
        Press5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F1)) ������ ������ �˾��� Ȱ��ȭ �ǰ� �ٸ� �˾��� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Press1.SetActive(true);
            Press2.SetActive(false);
            Press3.SetActive(false);
            Press4.SetActive(false);
            Press5.SetActive(false);
        }
        // if (Input.GetKeyDown(KeyCode.F2)) ������ ������ �˾��� Ȱ��ȭ �ǰ� �ٸ� �˾��� ��Ȱ��ȭ
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Press1.SetActive(false);
            Press2.SetActive(true);
            Press3.SetActive(false);
            Press4.SetActive(false);
            Press5.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Press1.SetActive(false);
            Press2.SetActive(false);
            Press3.SetActive(true);
            Press4.SetActive(false);
            Press5.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Press1.SetActive(false);
            Press2.SetActive(false);
            Press3.SetActive(false);
            Press4.SetActive(true);
            Press5.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            Press1.SetActive(false);
            Press2.SetActive(false);
            Press3.SetActive(false);
            Press4.SetActive(false);
            Press5.SetActive(true);
        }
    }
}
