using UnityEngine;
using TMPro;
using static EventNames;

public class TimedDialogueOnInteract : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel; // Assign the UI panel
    public TextMeshProUGUI dialogueText; // Assign the TMP text component
    [TextArea]
    public string message = "This is a timed dialogue.";

    [Header("Interaction")]
    public string promptMessage = "Press F to interact";
    [SerializeField] private float dialogueDuration = 3f;

    private bool playerInRange = false;

    void Awake()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleEvents.ON_SEQUENCE_PUZZLE_SOLVED, OnSequencePuzzleSolved);
    }

    void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.PuzzleEvents.ON_SEQUENCE_PUZZLE_SOLVED, OnSequencePuzzleSolved);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt(promptMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
        if (dialogueText != null)
            dialogueText.text = message;
        StartCoroutine(HideDialogueAfterDelay());
    }

    private System.Collections.IEnumerator HideDialogueAfterDelay()
    {
        yield return new WaitForSeconds(dialogueDuration);
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void OnSequencePuzzleSolved()
    {
        Debug.Log("Sequence puzzle solved! TimedDialogueOnInteract received event.");
        // Add further logic here as needed
    }
}
