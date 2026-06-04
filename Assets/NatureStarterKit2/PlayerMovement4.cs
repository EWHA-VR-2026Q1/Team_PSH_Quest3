using UnityEngine;

public class PlayerMovement4 : MonoBehaviour
{
    public float moveSpeed = 5f;        // 이동 속도
    public float rotateSpeed = 0.2f;    // 모바일 회전 속도
    public float mouseRotateSpeed = 2f; // PC 회전 속도
    public float gravity = 9.8f;        // 중력값

    public FixedJoystick joystick;   // 조이스틱 연결용

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        rotationY = currentRotation.y; // 시작 각도 저장

        if (joystick == null)
        {
            joystick = FindObjectOfType<FixedJoystick>(); // FixedJoystick으로 수정 완료
        }
    }

    void Update()
    {
        HandleRotation(); // 시점 회전

        if (controller.isGrounded)
        {
            float moveX = 0f;
            float moveZ = 0f;

            if (joystick != null)
            {
                moveX = joystick.Horizontal;
                moveZ = joystick.Vertical;
            }

            if (moveX == 0 && moveZ == 0)
            {
                moveX = Input.GetAxis("Horizontal"); // PC 키보드 호환
                moveZ = Input.GetAxis("Vertical");
            }

            Vector3 desiredMove = transform.right * moveX + transform.forward * moveZ;
            desiredMove.y = 0;

            if (desiredMove.magnitude > 0.1f)
            {
                desiredMove.Normalize();
                moveDirection = desiredMove * moveSpeed;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime; // 중력 계산
        controller.Move(moveDirection * Time.deltaTime); // 최종 이동
    }

    void HandleRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // 조이스틱 영역 제외 화면만 회전 처리
                if (touch.position.x > Screen.width * 0.3f || touch.position.y > Screen.height * 0.5f)
                {
                    rotationY += touch.deltaPosition.x * rotateSpeed;
                    rotationX -= touch.deltaPosition.y * rotateSpeed;
                    rotationX = Mathf.Clamp(rotationX, -40f, 40f);

                    transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
                    Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
                }
            }
        }
        else if (Input.GetMouseButton(1)) // PC 마우스 우클릭 회전
        {
            rotationY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
            rotationX -= Input.GetAxis("Mouse Y") * mouseRotateSpeed;
            rotationX = Mathf.Clamp(rotationX, -40f, 40f);

            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
}