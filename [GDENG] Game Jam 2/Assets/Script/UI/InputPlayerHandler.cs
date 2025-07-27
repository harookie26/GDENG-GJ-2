using UnityEngine;
using TMPro;
using Game.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject inputPanel;

    private IInputReceiver currentReceiver;
    public static PlayerInputHandler Instance { get; private set; }

    private PlayerMovement playerMovement;
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
        // Only disable controls and camera when the input panel is open
        if (inputPanel.activeSelf)
        {
            if (playerMovement != null)
            {
                playerMovement.playerInput.PlayerMovement.Disable();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inputManager != null)
            {
                inputManager.enabled = false;
            }
        }
        else
        {
            if (playerMovement != null)
            {
                playerMovement.playerInput.PlayerMovement.Enable();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (inputManager != null)
            {
                inputManager.enabled = true;
            }
        }

        if (inputPanel.activeSelf && inputField.isFocused &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            // This is now handled by OnInputFieldSubmit, but you can keep this for redundancy.
            OnInputFieldSubmit(inputField.text);
        }
    }

    public void StartInput(IInputReceiver receiver)
    {
        currentReceiver = receiver;
        inputField.text = "";
        inputPanel.SetActive(true);  // Show the panel
        inputField.ActivateInputField();
    }
}
