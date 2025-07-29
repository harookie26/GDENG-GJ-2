using Game.Interactable;
using UnityEngine;

public class HintController : MonoBehaviour, IInteractable
{
    [Header("Hint Settings")]
    [TextArea]
    public string hintMessage = "This is the hint for the player."; //Set hint in Inspector

    private bool playerInRange = false;
    private bool hintShown = false;

    public void Interact()
    {
 
            if (!hintShown)
            {
                SoundManager.Instance.PlayPaperRufflingSFX();
                HintUI.Instance?.ShowHint(hintMessage); //Show hint
                hintShown = true;
            }

            else
            {
                SoundManager.Instance.PlayPaperRufflingSFX();
                HintUI.Instance?.HideHint(); //Hide hint if F pressed again
                hintShown = false;
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

            if (hintShown)
            {
                HintUI.Instance?.HideHint();
                hintShown = false;
            }
        }
    }
}
