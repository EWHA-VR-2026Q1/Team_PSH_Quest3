using System.Collections;
using System.Collections.Generic;
using UnityEngine; // 이게 있어야 Input, Debug를 알아듣습니다!

public class JumpTest : MonoBehaviour
{
    void Update()
    {
        // 1. A 버튼 (조이스틱 0번) 물리 신호 체크
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("★ [물리 신호] A 버튼이 눌렸습니다!");
        }

        // 2. 'Jump'라는 이름의 세팅 체크
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("● [설정 인식] Jump 명령이 인식되었습니다!");
        }

        // 키보드 테스트용 (스페이스바)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("키보드 스페이스바 눌림!");
        }
    }
}