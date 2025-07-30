using Game.Interactable;
using UnityEngine;
using static EventNames;

public class MainGate : MonoBehaviour, IInteractable
{
    [Header("Ending Dialogue Reference")]
    public EndingSceneDialogue endingSceneDialogue; // Assign in Inspector

    private bool _isMainGateUnlocked = false;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(PuzzleEvents.ON_MAIN_GATE_UNLOCKED, MainGateUnlocked);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(PuzzleEvents.ON_MAIN_GATE_UNLOCKED, MainGateUnlocked);
    }

    public void Interact()
    {
        Debug.Log("Interacted with Main Gate");
        if (_isMainGateUnlocked)
        {
            EventBroadcaster.Instance.PostEvent(GameStateEvents.ON_GAME_END);
            if (endingSceneDialogue != null)
                endingSceneDialogue.StartEndingSequence();
        }
        else
        {
            Debug.Log("Main Gate is locked, interaction failed.");
            // Can add UI feedback or sound here
        }
    }

    public Transform GetTransform() => transform;

    private void MainGateUnlocked()
    {
        _isMainGateUnlocked = true;
    }
}
