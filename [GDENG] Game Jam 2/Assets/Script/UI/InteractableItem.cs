using Game.Interactable;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("Item Label")]
    public string itemLabel = "Item";

    public event System.Action<InteractableItem> OnItemInteracted;

    private bool playerInRange = false;

    public void Interact()
    {
            OnItemInteracted?.Invoke(this);

            if (itemLabel == "Toilet")
            {
                SoundManager.Instance.PlayToiletFlushSFX();
            }

            if (itemLabel == "Faucet")
            {
                SoundManager.Instance.PlayFaucetOpenSFX();
            }
    }

    public Transform GetTransform() => transform;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log($"Player is now in range of interactable item: {itemLabel}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
