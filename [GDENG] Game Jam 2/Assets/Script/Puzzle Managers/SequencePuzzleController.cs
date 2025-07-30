using UnityEngine;

public class SequencePuzzleController : MonoBehaviour
{
    [Header("Interactable Items (in correct sequence)")]
    public InteractableItem[] sequenceItems;

    [Header("Object to Activate on Puzzle Solved")]
    public GameObject objectToActivate;

    [Header("Fragment Restroom Script")]
    public EnableFragmentRestroom fragmentRestroom; 

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

                // Track fragment acquisition globally
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AcquireFragment(2); 
                    Debug.Log("Fragment 2 acquired via SequencePuzzleController.");
                }

                if (fragmentRestroom != null)
                {
                    fragmentRestroom.UnsealFragment();
                    Debug.Log("Fragment restroom unsealed by SequencePuzzleController.");
                }
            }
        }
        else
        {
            Debug.Log("Wrong item! Sequence reset.");
            currentStep = 0;
        }
    }

}
