using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 8.0f; // 점프 힘을 좀 더 키웠습니다.
    public float gravity = 20.0f;
    public float climbSpeed = 3.0f;
    public float waterLevel = -13.74f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isOnLadder = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. 사다리 로직
        if (isOnLadder)
        {
            float v = Input.GetAxis("Vertical");
            if (v != 0)
            {
                moveDirection = Vector3.up * v * climbSpeed;
                controller.Move(moveDirection * Time.deltaTime);
                return; 
            }
        }

        // 2. 바닥 판정 수정 (여기가 핵심!)
        // 캐릭터 컨트롤러의 기본 기능 + 살짝 아래를 체크해서 더 정확하게 만듭니다.
        bool isAtWater = transform.position.y <= waterLevel + 0.15f;
        bool isGrounded = controller.isGrounded || isAtWater;

        if (isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            forward.y = 0;
            right.y = 0;

            moveDirection = (forward.normalized * v) + (right.normalized * h);
            moveDirection *= speed;

            // [수정] 퀘스트 A 버튼(JoystickButton0)을 직접 인식하게 함
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
            else if (isAtWater && moveDirection.y < 0)
            {
                moveDirection.y = 0;
            }
        }

        // 3. 중력 적용
        moveDirection.y -= gravity * Time.deltaTime;

        // 4. 이동 실행
        controller.Move(moveDirection * Time.deltaTime);

        // 5. 수영장 바닥 뚫기 방지
        if (transform.position.y < waterLevel)
        {
            Vector3 pos = transform.position;
            pos.y = waterLevel;
            transform.position = pos;
            if(moveDirection.y < 0) moveDirection.y = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder")) isOnLadder = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder")) isOnLadder = false;
    }
}