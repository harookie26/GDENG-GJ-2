using Game.Interactable;
using UnityEngine;

using static EventNames;

public class Puzzle3OfficePC
    : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject Puzzle3UI;
    public void Interact()
    {
        Debug.Log("Puzzle3: Player interacted with the Office PC.");

        SoundManager.Instance.PlayComputerStartSFX();
        Puzzle3UI.SetActive(true);

        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED);
        EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_DISABLED);

    }

    public Transform GetTransform() => transform;

}
