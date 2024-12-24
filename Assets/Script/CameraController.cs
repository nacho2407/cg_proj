using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minFarPlane = 10f;
    public float maxFarPlane = 100f;
    public float farPlaneDecreaseRate = 1f;
    public float damageDecreaseFactor = 5f;
    public static CameraController Instance;

    private Camera cam;
    public UIManager uiManager;
    private float currentFarPlane;
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


        // Far Plane이 0 이하가 되면 게임 종료
        if (currentFarPlane <= 10)
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
