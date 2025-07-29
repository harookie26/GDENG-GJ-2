using TMPro;
using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel; //Assign in Inspector
    public TextMeshProUGUI innerDialogueText; //Assign in Inspector
    [SerializeField] private float innerDialogueDuration = 2f;

    private bool playerInRange = false;
    private bool hasInteracted = false;
    private Coroutine hideDialogueCoroutine;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasInteracted)
        {
            hasInteracted = true;
            GameState.doorsUnlocked = true;
            SoundManager.Instance.PlayDoorKeyUnlockSFX();
            //InteractionPrompt.Instance?.ShowPrompt("Huh? I don't know what happened. Lemme check again if the doors earlier have been unlocked.");
            ShowInnerDialogue("Huh where is that sound coming from? Lemme check again if the doors earlier have been unlocked.");
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
