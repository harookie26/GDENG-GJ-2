using Game.Interactable;
using TMPro;
using UnityEngine;

public class SurgeryDoorController : MonoBehaviour, IInteractable
{
    [Header("Door Animator & Clips")]
    public Animator classroomDoorAnimatorLeft;
    public Animator classroomDoorAnimatorRight;
    public string doorOpenAnimationLeft = "ClassroomDoorOpenLeft";
    public string doorCloseAnimationLeft = "ClassroomDoorCloseLeft";

    public string doorOpenAnimationRight = "ClassroomDoorOpenRight";
    public string doorCloseAnimationRight = "ClassroomDoorCloseRight";

    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel; //Assign in Inspector
    public TextMeshProUGUI innerDialogueText; //Assign in Inspector
    [SerializeField] private float innerDialogueDuration = 2f;

    private bool isOpen = false;
    private bool playerInRange = false;
    private Coroutine hideDialogueCoroutine;

    public void Interact()
    {
            if (!GameState.doorsUnlocked)
            {
                SoundManager.Instance.PlayDoorLockedSFX();
                ShowInnerDialogue("The door is locked.");
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
            HideInnerDialogue();
        }
    }

    private void ShowInnerDialogue(string message)
    {
        if (innerDialogueText != null)
            innerDialogueText.text = message;
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(true);

        if (hideDialogueCoroutine != null)
            StopCoroutine(hideDialogueCoroutine);
        hideDialogueCoroutine = StartCoroutine(HideInnerDialogueAfterDelay());
    }

    private void HideInnerDialogue()
    {
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(false);

        if (hideDialogueCoroutine != null)
        {
            StopCoroutine(hideDialogueCoroutine);
            hideDialogueCoroutine = null;
        }
    }

    private System.Collections.IEnumerator HideInnerDialogueAfterDelay()
    {
        yield return new WaitForSeconds(innerDialogueDuration);
        HideInnerDialogue();
    }
}
