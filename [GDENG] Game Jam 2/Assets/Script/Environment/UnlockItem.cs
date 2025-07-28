using UnityEngine;
using TMPro;
using Game.Interactable;

public class UnlockItem : MonoBehaviour, IInteractable
{
    [Header("Unlock Settings")]
    public UnlockableDoor targetDoor; // Assign in Inspector

    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel;
    public TextMeshProUGUI innerDialogueText;
    [TextArea]
    public string unlockDialogue = "You found the key. The door can now be opened.";
    [SerializeField] private float innerDialogueDuration = 2f;

    private Coroutine hideDialogueCoroutine;

    public void Interact()
    {
        if (targetDoor != null)
        {
            targetDoor.UnlockDoor();
        }
        ShowInnerDialogue(unlockDialogue);
    }

    public Transform GetTransform() => transform;

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
