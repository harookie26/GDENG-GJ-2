using UnityEngine;
using TMPro;

public class SceneIntroDialogue : MonoBehaviour
{
    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel; // Assign in Inspector
    public TextMeshProUGUI innerDialogueText; // Assign in Inspector

    [Header("Dialogue Sequence")]
    [TextArea]
    public string[] dialogueLines; // Fill in Inspector with each line of dialogue
    [SerializeField] private float lineDuration = 2f; // Seconds per line

    private void Start()
    {
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(true);

        if (dialogueLines != null && dialogueLines.Length > 0)
            StartCoroutine(PlayDialogueSequence());
        else
            HideInnerDialogue();
    }

    private System.Collections.IEnumerator PlayDialogueSequence()
    {
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            if (innerDialogueText != null)
                innerDialogueText.text = dialogueLines[i];

            yield return new WaitForSeconds(lineDuration);
        }
        HideInnerDialogue();
    }

    private void HideInnerDialogue()
    {
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(false);
    }
}
