using UnityEngine;
using TMPro;
using static EventNames;

public class EnableFragmentRestroom : MonoBehaviour
{
    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel;
    public TextMeshProUGUI innerDialogueText;

    [Header("Inner Dialogue Messages")]
    [TextArea]
    public string sealedDialogue = "You feel a mysterious force preventing you from interacting with the fragment.";
    [TextArea]
    public string interactDialogue = "You interact with the fragment. Something changes...";

    [Header("Interaction Prompt")]
    public string interactPrompt = "Press F to interact";

    [Header("Inner Dialogue Duration")]
    [SerializeField] private float innerDialogueDuration = 3f;

    [Header("Fragment Model")]
    public GameObject fragmentModel; 


    private bool playerInRange = false;
    private bool isSealed = true;
    private Coroutine hideDialogueCoroutine;

    public void UnsealFragment()
    {
        isSealed = false;
        if (fragmentModel != null)
        {
            fragmentModel.SetActive(true);
            Debug.Log("Fragment model enabled in restroom.");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isSealed)
            {
                ShowInnerDialogue(sealedDialogue);
            }
            else
            {
                ShowInnerDialogue(interactDialogue);
                // Add your fragment interaction logic here
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt(interactPrompt);
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
