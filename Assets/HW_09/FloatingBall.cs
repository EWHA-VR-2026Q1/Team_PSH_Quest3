using UnityEngine;

public class FloatingBall : MonoBehaviour
{
    public Rigidbody rb;
    public float waterLevel = -2.92f; // 수영장 물 Y값에 맞춰 조절하세요
    public float floatThreshold = 1f;
    public float waterDrag = 1f;

    void FixedUpdate()
    {
        if (transform.position.y < waterLevel)
        {
            float forceMultiplier = Mathf.Clamp01(waterLevel - transform.position.y) * floatThreshold;
            rb.AddForce(Vector3.up * forceMultiplier, ForceMode.VelocityChange);
            
            // 이 부분을 drag로 수정!
            rb.drag = waterDrag; 
        }
        else
        {
            // 이 부분도 drag로 수정!
            rb.drag = 0f; 
        }
    }
}