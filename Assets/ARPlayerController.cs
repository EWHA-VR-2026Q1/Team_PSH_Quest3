using UnityEngine;

public class ARPlayerController : MonoBehaviour
{
    [Header("조종 속도")]
    public float moveSpeed = 5.0f;
    public float lookSpeed = 0.2f;
    public float gravity = 9.81f;

    [Header("카메라 및 조이스틱 연결")]
    public Transform cameraTransform;
    public Joystick joystick;

    private CharacterController controller;
    private float rotX = 0f;
    private float rotY = 0f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        //  [진단 1] 컴포넌트 위치 확인
        if (controller == null)
        {
            Debug.LogError($"[오류] '{gameObject.name}' 오브젝트에 'Character Controller' 컴포넌트가 없습니다! 스크립트가 엉뚱한 오브젝트(예: Main Camera)에 붙어있거나 컴포넌트를 빼먹으셨을 수 있습니다.");
        }

        if (cameraTransform == null)
        {
            Debug.LogWarning(" [경고] Main Camera가 인스펙터 창에서 연결되지 않았습니다.");
        }

        if (cameraTransform != null)
        {
            rotX = cameraTransform.localRotation.eulerAngles.x;
            rotY = transform.localRotation.eulerAngles.y;
        }
    }

    void Update()
    {
        // ---------------------------------------------------
        // 회전 기능 (이전과 동일)
        // ---------------------------------------------------
#if UNITY_EDITOR
        if (Input.GetMouseButton(1)) 
        {
            rotY += Input.GetAxis("Mouse X") * lookSpeed * 10f;
            rotX -= Input.GetAxis("Mouse Y") * lookSpeed * 10f;
        }
#else
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    rotY += touch.deltaPosition.x * lookSpeed;
                    rotX -= touch.deltaPosition.y * lookSpeed;
                }
            }
        }
#endif

        if (rotX > 180f) rotX -= 360f;
        rotX = Mathf.Clamp(rotX, -70f, 70f);

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);
        }

        // ---------------------------------------------------
        // 이동 기능 및 원인 진단 로그
        // ---------------------------------------------------
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // [진단 2] 키보드 입력 시스템 먹통 확인 (New Input System Trap)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(moveX) < 0.01f && Mathf.Abs(moveZ) < 0.01f)
            {
                Debug.LogError(" [오류] 키보드 WASD는 눌렸으나 유니티가 키 값을 0으로 인식합니다! (프로젝트가 New Input System 전용으로 설정되어 낡은 코드가 안 먹히는 상태입니다.)");
            }
        }

        if (joystick != null && Mathf.Abs(moveX) < 0.1f && Mathf.Abs(moveZ) < 0.1f)
        {
            moveX = joystick.Horizontal;
            moveZ = joystick.Vertical;
        }

        Vector3 inputDir = (transform.forward * moveZ + transform.right * moveX);
        if (inputDir.magnitude > 1f) inputDir.Normalize();

        if (controller != null && controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVelocity = inputDir * moveSpeed;
        moveVelocity.y = verticalVelocity;

        if (controller != null)
        {
            controller.Move(moveVelocity * Time.deltaTime);

            // [진단 3] 실제로 움직이는 힘이 유니티에 전달되는지 확인
            if (inputDir.magnitude > 0.1f)
            {
                Debug.Log($"[이동 신호 정상 작동 중] 움직이는 속도 힘: {moveVelocity}");
            }
        }
    }
}