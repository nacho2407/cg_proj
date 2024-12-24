
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 10f;
    [SerializeField]
    float bulletLifetime = 5f;
    [SerializeField]
    public Vector3 bulletScale = new Vector3(0.2f, 0.2f, 0.2f);
    
    public int maxBullets = 20;
    private int currentBullets;

    public Text bulletCountText;

    // Start is called before the first frame update
    void Start()
    {
        currentBullets = maxBullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBullets > 0)
        {
            SpawnBullet();
            currentBullets--;
        }
        UpdateBulletCountUI();
    }
    public void Reload(int amount)
    {
        currentBullets = Mathf.Min(currentBullets + amount, maxBullets);
        // UI 업데이트
        UpdateBulletCountUI();
    }

    void UpdateBulletCountUI()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + currentBullets + " / " + maxBullets;
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        bullet.transform.position = transform.position + transform.forward * 2f;

        bullet.transform.localScale = bulletScale;

        Bullet bulletComponent = bullet.AddComponent<Bullet>();
        bulletComponent.SetSpeed(bulletSpeed);

        bulletComponent.SetDirection(transform.forward);

        SphereCollider sc = bullet.AddComponent<SphereCollider>();

        sc.isTrigger = true;
        sc.radius = 0.2f;

        Destroy(bullet, bulletLifetime);
    }
}


public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    public int damage = 50;

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        Debug.Log("object hit!!!!");
        Destroy(this.gameObject);
    }
}
