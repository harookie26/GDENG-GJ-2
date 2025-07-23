using UnityEngine;

public class MorgueDrawerController : MonoBehaviour
{
    [Header("Drawer Door Animator & Clips")]
    public Animator drawerDoorAnimator;
    public string doorOpenAnimation = "Drawer Door Open";
    public string doorCloseAnimation = "Drawer Door Close";

    [Header("Drawer Bed Animator & Clips")]
    public Animator drawerBedAnimator;
    public string bedOpenAnimation = "Morgue Bed";
    public string bedCloseAnimation = "Morgue Bed Close";

    private bool isOpen = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isOpen)
            {
                if (drawerDoorAnimator != null)
                    drawerDoorAnimator.SetTrigger("Close");
                if (drawerBedAnimator != null)
                    drawerBedAnimator.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                if (drawerDoorAnimator != null)
                    drawerDoorAnimator.SetTrigger("Open");
                if (drawerBedAnimator != null)
                    drawerBedAnimator.SetTrigger("Open");
                isOpen = true;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt("Press F to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
        }
    }
}
