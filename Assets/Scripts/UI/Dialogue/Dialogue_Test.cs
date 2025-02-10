using UnityEngine;

public class Dialogue_Test : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            var popupUI = Managers.UI.ShowPopupUI<UI_DialoguePopup>();

            popupUI.SetInfo(10000, 10022);

            // 직접 출력
            // Debug.Log("text : " + Managers.Data.Texts[10000].eng);


        }
    }

}
