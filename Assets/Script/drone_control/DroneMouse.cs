using UnityEngine;

public class MouseAimAndShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 총알 발사 위치
    public float bulletSpeed = 20f; // 총알 속도
    public float bulletLifetime = 5f; // 총알 수명 (초 단위)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        AimAtMouse();
        if (Input.GetMouseButtonDown(0)) // 좌클릭 감지
        {
            Shoot();
        }
    }

    void AimAtMouse()
    {
        // 마우스 위치 가져오기
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Y축을 기준으로 평면 정의

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);

            // 오브젝트가 Y축 기준으로만 회전하도록 설정
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0; // Y축 고정
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 부드럽게 회전
        }
    }

    void Shoot()
    {
        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 총알 모양 조형
        CreateBulletShape(bullet);

        // 총알에 속도 부여
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
        }

        // 총알 삭제 (수명 종료)
        Destroy(bullet, bulletLifetime);
    }
    
    void CreateBulletShape(GameObject bullet)
    {
        // Sphere를 총알 모양으로 조형
        Transform bulletTransform = bullet.transform;
        bulletTransform.localScale = new Vector3(0.05f, 0.05f, 0.1f); // X, Y는 좁게, Z는 길게
    }
}
