using UnityEngine;

public class SurgeryDoorController : MonoBehaviour
{
    [Header("Door Animator & Clips")]
    public Animator classroomDoorAnimatorLeft;
    public Animator classroomDoorAnimatorRight;
    public string doorOpenAnimationLeft = "ClassroomDoorOpenLeft";
    public string doorCloseAnimationLeft = "ClassroomDoorCloseLeft";

    public string doorOpenAnimationRight = "ClassroomDoorOpenRight";
    public string doorCloseAnimationRight = "ClassroomDoorCloseRight";

    private bool isOpen = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!GameState.doorsUnlocked)
            {
                SoundManager.Instance.PlayDoorLockedSFX();
                //InteractionPrompt.Instance?.ShowPrompt("This door is locked");
                return;
            }

            if (isOpen)
            {
                SoundManager.Instance.PlayDoorClosingSFX();
                if (classroomDoorAnimatorLeft != null)
                    classroomDoorAnimatorLeft.SetTrigger("Close");
                if (classroomDoorAnimatorRight != null)
                    classroomDoorAnimatorRight.SetTrigger("Close");
                isOpen = false;
            }

            else
            {
                SoundManager.Instance.PlayDoorOpeningSFX();
                if (classroomDoorAnimatorLeft != null)
                    classroomDoorAnimatorLeft.SetTrigger("Open");
                if (classroomDoorAnimatorRight != null)
                    classroomDoorAnimatorRight.SetTrigger("Open");
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
