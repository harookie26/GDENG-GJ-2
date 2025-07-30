using Game.Interactable;
using UnityEngine;

public class MorgueDrawerController : MonoBehaviour, IInteractable
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

    public void Interact()
    {
            if (isOpen)
            {
                SoundManager.Instance.PlayMorgueDrawerCloseSFX();
                if (drawerDoorAnimator != null)
                    drawerDoorAnimator.SetTrigger("Close");
                if (drawerBedAnimator != null)
                    drawerBedAnimator.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                SoundManager.Instance.PlayMorgueDrawerOpenSFX();
                if (drawerDoorAnimator != null)
                    drawerDoorAnimator.SetTrigger("Open");
                if (drawerBedAnimator != null)
                    drawerBedAnimator.SetTrigger("Open");
                isOpen = true;
            }
    }

    public Transform GetTransform() => transform;

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
