using Game.InputSystem;
using Game.Interactable;
using UnityEngine;

using static EventNames;

public class Puzzle3OfficePC
    : MonoBehaviour, IInputReceiver, IInteractable
{
    public void Interact()
    {
        Debug.Log("Puzzle3: Player interacted with the Office PC.");
        PlayerInputHandler.Instance.StartInput(this);
    }

    public Transform GetTransform() => transform;

    public void OnInputSubmitted(string input)
    {
        if (input == "1234")
        {
            Debug.Log("OfficePC: Correct code!");
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_PUZZLE3_SOLVED);
        }
        else
            Debug.Log("OfficePC: Incorrect code.");
    }
}
