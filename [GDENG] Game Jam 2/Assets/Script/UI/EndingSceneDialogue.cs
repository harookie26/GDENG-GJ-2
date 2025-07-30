using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class EndingSceneDialogue : MonoBehaviour
{
    [Header("Ending Dialogue UI")]
    public GameObject endingDialoguePanel;
    public TextMeshProUGUI endingDialogueText;

    [Header("Dialogue Sequence")]
    [TextArea]
    public string[] endingLines = new string[] {
        "You find the requested file.",
        "It has your name on it.",
        "Your photograph.",
        "A diagnosis.",
        "A treatment record.",
        "Stamped:",
        "    “Subject #21: Split successful. Integration failed.”",
        $"    “Admittance Date: {GetAdmittanceDate()}”",
        "“Welcome back to Karlheinz.”"
    };
    [SerializeField] private float lineDuration = 2f;
    [Header("Main Menu Scene Name")]
    public string mainMenuSceneName = "Main Menu";

    private Coroutine dialogueCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("EndingSceneDialogue: 'P' pressed, triggering ending dialogue sequence.");
            StartEndingSequence();
        }
    }

    public void StartEndingSequence()
    {
        if (endingDialoguePanel != null)
            endingDialoguePanel.SetActive(true);

        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
    }

    private IEnumerator PlayDialogueSequence()
    {
        if (endingDialogueText == null || endingLines == null || endingLines.Length == 0)
            yield break;

        endingDialogueText.text = "";
        for (int i = 0; i < endingLines.Length; i++)
        {
            endingDialogueText.text = endingLines[i];
            yield return new WaitForSeconds(lineDuration);
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }

    private static string GetAdmittanceDate()
    {
        return DateTime.Now.ToString("MMMM dd, yyyy");
    }
}
