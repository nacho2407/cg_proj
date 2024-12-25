using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 속도
    public float drag = 0.95f;    // 감속 계수
    public Rigidbody rb;          // Rigidbody 참조

    private Vector3 velocity;     // 내부 속도 벡터

    void Start()
    {
        // Rigidbody가 설정되지 않았다면 자동으로 가져오기
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Rigidbody 설정 초기화
        rb.useGravity = false;                 // 중력 비활성화
        rb.isKinematic = false;                // 물리 기반 이동 활성화
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // 충돌 감지 모드
    }

    void FixedUpdate()
    {
        // 입력 처리
        float inputX = Input.GetAxis("Horizontal"); // A/D 또는 좌/우 화살표 키
        float inputZ = Input.GetAxis("Vertical");   // W/S 또는 위/아래 화살표 키

        // 입력 방향 벡터 계산
        Vector3 inputDirection = new Vector3(inputX, 0, inputZ);

        // 입력에 따라 속도 추가
        velocity += inputDirection * moveSpeed * Time.fixedDeltaTime;

        // 속도 감속 (마찰 효과)
        velocity *= drag;

        // Rigidbody 속도 설정
        rb.velocity = new Vector3(velocity.x, 0, velocity.z); // Y축 고정
    }
}
