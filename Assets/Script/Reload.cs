using UnityEngine;

public class Reload : MonoBehaviour
{
    public int bulletAmount = 20;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Gun playerGun = other.GetComponent<Gun>();
            if (playerGun != null)
            {
                playerGun.Reload(bulletAmount);
                Destroy(gameObject);
            }
        }
    }
}
