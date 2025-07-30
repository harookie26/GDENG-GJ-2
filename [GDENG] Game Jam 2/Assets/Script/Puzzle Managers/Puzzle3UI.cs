using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EventNames;
using Game.InputSystem;

public class Puzzle4UI : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Button shutDownButton;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerInputHandler.Instance.StartInput(this);
        InteractionPrompt.Instance?.HidePrompt();


    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        shutDownButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_ENABLED);

        });

    }

    private void Update()
    {
    }

    public void OnInputSubmitted(string input)
    {
        if (input == "GDENG01")
        {
            Debug.Log("OfficePC: Correct code!");
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_PUZZLE3_SOLVED);
        }
        else
            Debug.Log("OfficePC: Incorrect code.");
    }

}
