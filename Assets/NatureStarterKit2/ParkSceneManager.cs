using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ParkSceneManager : MonoBehaviour
{
    public TextMeshProUGUI guidanceText;
    public Button nextSceneButton;       // 나갈 때 나타날 다음 씬 버튼

    void Start()
    {
        // 1. 처음 공원 씬 시작하자마자 뜨는 문구 딱 하나!
        guidanceText.text = "화면을 터치하여 공원을 가볍게 조깅해 보세요.";

        // 버튼은 처음엔 숨겨둡니다.
        if (nextSceneButton != null) nextSceneButton.gameObject.SetActive(false);
    }

    // 플레이어가 처음에 들어왔던 문틀(PortalDoor)을 다시 통과해 나갈 때 발동
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 2. 문을 통해 나가는 순간, 자막은 그대로 두고 버튼만 짠! 하고 나타남
            if (nextSceneButton != null)
            {
                nextSceneButton.gameObject.SetActive(true);
                nextSceneButton.GetComponentInChildren<TextMeshProUGUI>().text = "공원에서 나가기";
            }
        }
    }
}