using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    // 가고 싶은 씬 이름을 유니티에서 직접 타이핑할 수 있게 
    public string targetSceneName;

    // 이 박스(Trigger) 안에 들어왔을 때 자동으로 실행되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 닿은 물체의 태그가 "Player"라면 
        if (other.CompareTag("Player"))
        {
            // 설정한 이름의 씬으로 이동
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
