using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 3.0f; // 올라가는 속도

    private void OnTriggerStay(Collider other)
    {
        // 내 몸(Player 태그)이 사다리에 닿아 있는 동안
        if (other.CompareTag("Player"))
        {
            // 조이스틱을 위로 밀면(Vertical > 0) 위로 올라감
            float v = Input.GetAxis("Vertical");
            if (v > 0)
            {
                other.transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
            }
        }
    }
}