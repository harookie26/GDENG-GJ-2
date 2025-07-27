using Game.InputSystem;
using UnityEngine;

using static EventNames;

public class Puzzle3 : MonoBehaviour, IInputReceiver
{
    [SerializeField] private GameObject officePC;
    [SerializeField] private GameObject puzzleInfo;
    // [SerializeField] private UI puzzleInfoHint;

    private enum PuzzleTarget { None, OfficePC, PuzzleInfo }
    private PuzzleTarget currentTarget = PuzzleTarget.None;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(ItemEvents.ON_ITEM_INTERACT, OnItemInteract);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(ItemEvents.ON_ITEM_INTERACT, OnItemInteract);
    }

    private void OnItemInteract()
    {
        if (IsPlayerNearbySingleton.Instance.IsPlayerNearby(officePC, 2.0f))
        {
            Debug.Log("Puzzle3: Player interacted with the Office PC.");
            currentTarget = PuzzleTarget.OfficePC;
            PlayerInputHandler.Instance.StartInput(this);
        }
        else if (IsPlayerNearbySingleton.Instance.IsPlayerNearby(puzzleInfo, 2.0f))
        {
            Debug.Log("Puzzle3: Player interacted with the Puzzle Info.");

        }
    }

    public void OnInputSubmitted(string input)
    {
        switch (currentTarget)
        {
            case PuzzleTarget.OfficePC:
                if (input == "1234")
                {
                    Debug.Log("OfficePC: Correct code!");
                    EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_PUZZLE3_SOLVED);
                }
                else
                    Debug.Log("OfficePC: Incorrect code.");
                break;
        }

        currentTarget = PuzzleTarget.None;
    }
}
