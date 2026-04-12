using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float waterLevel = -13.747f; // 물의 Y축 높이
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 공의 현재 위치가 물 높이보다 낮아지면
        if (transform.position.y < waterLevel)
        {
            // 1. 위치를 강제로 물 위로 고정 (절대 안 가라앉음)
            Vector3 pos = transform.position;
            pos.y = waterLevel;
            transform.position = pos;

            // 2. 물리적으로 아래로 내려가는 힘을 0으로 차단
            if (rb != null)
            {
                Vector3 vel = rb.velocity; // 에러 해결 포인트!
                if (vel.y < 0) 
                {
                    vel.y = 0;
                    rb.velocity = vel;
                }
            }
        }
    }
}