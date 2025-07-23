using UnityEngine;

public class SequencePuzzleController : MonoBehaviour
{
    [Header("Interactable Items (in correct sequence)")]
    public InteractableItem[] sequenceItems; // Assign in the correct order

    private int currentStep = 0;
    private bool puzzleSolved = false;

    void Start()
    {
        foreach (var item in sequenceItems)
        {
            item.OnItemInteracted += OnItemInteracted;
        }
        Debug.Log("SequencePuzzleController initialized. Waiting for correct item sequence.");
    }

    private void OnItemInteracted(InteractableItem item)
    {
        if (puzzleSolved)
        {
            Debug.Log("Puzzle already solved. Ignoring further interactions.");
            return;
        }

        Debug.Log($"Item interacted: {item.itemLabel}");

        if (item == sequenceItems[currentStep])
        {
            Debug.Log($"Correct item! Step {currentStep + 1} of {sequenceItems.Length}.");
            currentStep++;
            if (currentStep >= sequenceItems.Length)
            {
                puzzleSolved = true;
                Debug.Log("Sequence puzzle solved! Broadcasting event.");
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleEvents.ON_SEQUENCE_PUZZLE_SOLVED);
            }
        }
        else
        {
            Debug.Log("Wrong item! Sequence reset.");
            currentStep = 0;
            // Optionally show feedback: InteractionPrompt.Instance?.ShowPrompt("Wrong item! Try again.");
        }
    }
}
