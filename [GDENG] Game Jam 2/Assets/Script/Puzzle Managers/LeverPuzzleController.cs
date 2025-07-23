using UnityEngine;

public class LeverPuzzleController : MonoBehaviour
{
    public LeverController silverLever;
    public LeverController redLever;
    public LeverController yellowLever;

    private LeverController[] sequence;
    private int currentStep = 0;
    private bool puzzleSolved = false;

    void Start()
    {
        sequence = new LeverController[] { silverLever, redLever, yellowLever };

        silverLever.OnLeverPulled += OnLeverPulled;
        redLever.OnLeverPulled += OnLeverPulled;
        yellowLever.OnLeverPulled += OnLeverPulled;

        Debug.Log("LeverPuzzleController initialized. Waiting for correct lever sequence.");
    }

    private void OnLeverPulled(LeverController lever)
    {
        if (puzzleSolved)
        {
            Debug.Log("Puzzle already solved. Ignoring further lever pulls.");
            return;
        }

        Debug.Log($"Lever pulled: {lever.label}");

        if (lever == sequence[currentStep])
        {
            Debug.Log($"Correct lever! Step {currentStep + 1} of {sequence.Length}.");
            currentStep++;
            if (currentStep >= sequence.Length)
            {
                puzzleSolved = true;
                Debug.Log("Puzzle solved! Broadcasting event.");
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleEvents.ON_LEVER_PUZZLE_SOLVED);
            }
        }
        else
        {
            Debug.Log("Wrong lever! Sequence reset.");
            currentStep = 0;
            // You can add feedback here, e.g. InteractionPrompt.Instance?.ShowPrompt("Wrong lever! Try again.");
        }
    }
}
