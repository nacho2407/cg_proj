using UnityEngine;
public class Battery : MonoBehaviour
{
    public float farPlaneIncrease = 20f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            CameraController.Instance.IncreaseFarPlane(farPlaneIncrease);
            Destroy(gameObject);
        }
    }
}
