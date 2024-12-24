using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    public float minFarPlane = 0f;
    public float maxFarPlane = 100f;
    public float farPlaneDecreaseRate = 1f;
    public float damageDecreaseFactor = 5f;
    public static CameraController Instance;

    private Camera cam;
    public UIManager uiManager;
    private float currentFarPlane;
    public Text healthText;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        currentFarPlane = maxFarPlane;
        UpdateCameraClipping();
    }

    void Update()
    {
        DecreaseFarPlane();
        UpdateCameraClipping();

        UpdateHealthUI();
        // Far Plane이 0 이하가 되면 게임 종료
        if (currentFarPlane <= 0)
        {
            uiManager.GameOver();
        }
    }


    void DecreaseFarPlane()
    {
        currentFarPlane -= farPlaneDecreaseRate * Time.deltaTime;
        currentFarPlane = Mathf.Max(currentFarPlane, minFarPlane);
    }

    void UpdateCameraClipping()
    {
        cam.farClipPlane = currentFarPlane;
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentFarPlane.ToString("F1");
        }
    }

    public void DecreaseViewOnDamage()
    {
        currentFarPlane -= damageDecreaseFactor;
        currentFarPlane = Mathf.Max(currentFarPlane, minFarPlane);
    }

    public void IncreaseFarPlane(float amount)
    {
        currentFarPlane = Mathf.Min(currentFarPlane + amount, maxFarPlane);
    }

}
