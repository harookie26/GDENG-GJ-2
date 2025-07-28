using UnityEngine;

public class PuzzleDoorController : MonoBehaviour
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
            if (!GameState.doorsUnlocked)
            {
                SoundManager.Instance.PlayDoorLockedSFX();
                //HintUI.Instance?.ShowHint("This door is locked.");
                //InteractionPrompt.Instance?.ShowPrompt("This door is locked");
                return;
            }

            if (isOpen)
            {
                SoundManager.Instance.PlayDoorClosingSFX();
                if (classroomDoorAnimator != null)
                    classroomDoorAnimator.SetTrigger("Close");
                isOpen = false;
            }

            else
            {
                SoundManager.Instance.PlayDoorOpeningSFX();
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
