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
//            Debug.Log("플래시뱅 생성 실패");
//            return;
//        }
//        flashbang.transform.position = this.transform.position + new Vector3(0, 1, 2); // 플레이어 앞에 생성
//        // TODO : 플레이어가 바라보는 방향으로 생성되도록 수정

//        StartCoroutine(ReturnFlashbang(flashbang)); // 일정 시간 후 풀에 반환
//    }

//    IEnumerator ReturnFlashbang(GameObject flashbang)
//    {
//        var gadget3 = DB.DataTables.GetSkill(1003);
//        yield return new WaitForSeconds(gadget3.Duration); // 데이터의 Duration만큼 대기
//        poolManager.TakeSpawn(flashbang);
//    }
//}
