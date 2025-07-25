using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    private bool playerInRange = false;
    private bool hasInteracted = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasInteracted)
        {
            hasInteracted = true;
            GameState.doorsUnlocked = true;
            InteractionPrompt.Instance?.ShowPrompt("Huh? I don't know what happened. Lemme check again if the doors earlier have been unlocked.");
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
