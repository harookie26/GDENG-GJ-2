using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Animator & Clips")]
    public Animator classroomDoorAnimator;
    public string doorOpenAnimation = "ClassroomDoorOpen";
    public string doorCloseAnimation = "ClassroomDoorClose";

    private bool isOpen = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isOpen)
            {
                if (classroomDoorAnimator != null)
                    classroomDoorAnimator.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                if (classroomDoorAnimator != null)
                    classroomDoorAnimator.SetTrigger("Open");
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
