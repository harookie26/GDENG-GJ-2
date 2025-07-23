using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [Header("Item Label")]
    public string itemLabel = "Item";

    public event System.Action<InteractableItem> OnItemInteracted;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            OnItemInteracted?.Invoke(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
