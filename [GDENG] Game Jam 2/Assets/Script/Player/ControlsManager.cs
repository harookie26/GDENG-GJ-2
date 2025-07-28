using UnityEngine;
using static EventNames;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject InputManager;
    [SerializeField] private GameObject FirstPersonCam;
    [SerializeField] private GameObject PlayerMovement;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_CONTROLS_DISABLED, DisableControls);
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_CONTROLS_ENABLED, EnableControls);
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED, DisableControls);
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED, EnableControls);
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED, DisableControls);
        EventBroadcaster.Instance.AddObserver(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED, EnableControls);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_CONTROLS_DISABLED, DisableControls);
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_CONTROLS_ENABLED, EnableControls);
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED, DisableControls);
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED, EnableControls);
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED, DisableControls);
        EventBroadcaster.Instance.RemoveActionAtObserver(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED, EnableControls);
    }

    private void DisableControls()
    {
        InputManager.SetActive(false);
        FirstPersonCam.SetActive(false);
        PlayerMovement.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void EnableControls()
    {
        InputManager.SetActive(true);
        FirstPersonCam.SetActive(true);
        PlayerMovement.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
