using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public Vector3 bulletScale = new Vector3(0.2f, 0.2f, 0.2f);

    private Transform playerTransform;
    private float timer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.position = transform.position;
        bullet.transform.localScale = bulletScale;

        Vector3 direction = (playerTransform.position - transform.position).normalized;

        EnemyBullet bulletComponent = bullet.AddComponent<EnemyBullet>();
        bulletComponent.SetSpeed(bulletSpeed);
        bulletComponent.SetDirection(direction);

        SphereCollider sc = bullet.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 0.2f;

        Destroy(bullet, bulletLifetime);
    }
}

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CameraController.Instance != null)
            {
                CameraController.Instance.DecreaseViewOnDamage();
            }
            Destroy(gameObject);
        }
    }
}
