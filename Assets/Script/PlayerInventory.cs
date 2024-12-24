using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int maxFloppyDisks = 6;
    private int collectedFloppyDisks = 0;
    public Text floppyDiskCountText;

    public void CollectFloppyDisk()
    {
        if (collectedFloppyDisks < maxFloppyDisks)
        {
            collectedFloppyDisks++;
            UpdateFloppyDiskUI();
        }
    }

    public bool HasCollectedAllFloppyDisks()
    {
        return collectedFloppyDisks >= maxFloppyDisks;
    }

    void UpdateFloppyDiskUI()
    {
        if (floppyDiskCountText != null)
        {
            floppyDiskCountText.text = $"Floopy Disk: {collectedFloppyDisks}/{maxFloppyDisks}";
        }
    }
}
