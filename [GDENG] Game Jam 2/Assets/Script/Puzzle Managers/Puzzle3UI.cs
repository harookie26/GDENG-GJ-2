using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static EventNames;
using Game.InputSystem;
using TMPro;

public class Puzzle3UI : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Button shutDownButton;
    [SerializeField] private TMP_Text passwordResponse;
    [SerializeField] private GameObject passwordResponsefield;


    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerInputHandler.Instance.StartInput(this);
        EventBroadcaster.Instance.PostEvent(UIEvents.ON_INTERACTION_PROMPT_HIDE);

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
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_PUZZLE3_SOLVED);
            passwordResponsefield.SetActive(true);
            passwordResponse.text = "Source Gate Control door unlocked.";
        }
        else
        {
            passwordResponsefield.SetActive(true);
            passwordResponse.text = "Incorrect Password";
        }
    }

}
