//using System.Collections;
//using UnityEngine;

//public class PlayerGadgetController : MonoBehaviour
//{
//    public PoolManangerKOR poolManager;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // poolManager = GetComponent<PoolManangerKOR>();
//    }
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            SpawnFlashbang();
//        }
//    }

//    void SpawnFlashbang()
//    {
//        GameObject flashbang = poolManager.GetSpawn("FlashBang");

//        if (flashbang == null)
//        {
//            Debug.Log("�÷��ù� ���� ����");
//            return;
//        }
//        flashbang.transform.position = this.transform.position + new Vector3(0, 1, 2); // �÷��̾� �տ� ����
//        // TODO : �÷��̾ �ٶ󺸴� �������� �����ǵ��� ����

//        StartCoroutine(ReturnFlashbang(flashbang)); // ���� �ð� �� Ǯ�� ��ȯ
//    }

//    IEnumerator ReturnFlashbang(GameObject flashbang)
//    {
//        var gadget3 = DB.DataTables.GetSkill(1003);
//        yield return new WaitForSeconds(gadget3.Duration); // �������� Duration��ŭ ���
//        poolManager.TakeSpawn(flashbang);
//    }
//}
