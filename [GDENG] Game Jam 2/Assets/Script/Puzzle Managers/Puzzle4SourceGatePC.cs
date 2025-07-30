using Game.Interactable;
using UnityEngine;
using static EventNames;

public class Puzzle4SourceGatePC : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Puzzle4UI;
    public void Interact()
    {
        if (Puzzle4UI.activeSelf)
        {
            Debug.Log("Puzzle4: Puzzle UI is already active.");
            return;
        }

        SoundManager.Instance.PlayComputerStartSFX();
        Puzzle4UI.SetActive(true);

        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_DISABLED);

    }

    public Transform GetTransform() => transform;
}
