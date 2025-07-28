using UnityEngine;
using TMPro;
using Game.InputSystem;
using static EventNames;

public class PlayerInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject inputPanel;

    private IInputReceiver currentReceiver;
    public static PlayerInputHandler Instance { get; private set; }

    public PlayerMovement playerMovement;
    private InputManager inputManager;

    private void Awake()
    {
        Instance = this;
        inputPanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        inputManager = FindFirstObjectByType<InputManager>();
        inputField.onSubmit.AddListener(OnInputFieldSubmit);
    }

    private void OnInputFieldSubmit(string input)
    {
        inputField.text = "";
        inputPanel.SetActive(false);
        inputField.DeactivateInputField();
        currentReceiver?.OnInputSubmitted(input);
        currentReceiver = null;
    }

    void Update()
    {
        if (inputPanel.activeSelf)
        {
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_DISABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_DISABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_DISABLED);
        }
        else
        {
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_ENABLED);    
        }

        if (inputPanel.activeSelf && inputField.isFocused &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            OnInputFieldSubmit(inputField.text);
        }
    }

    public void StartInput(IInputReceiver receiver)
    {
        currentReceiver = receiver;
        inputField.text = "";
        inputPanel.SetActive(true);
        inputField.ActivateInputField();
    }
}
