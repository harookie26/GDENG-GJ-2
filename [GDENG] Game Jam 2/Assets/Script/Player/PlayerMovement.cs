using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerControls playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;

    InputAction hideCursorAction;
    InputAction showCursorAction;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 9f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Transform groundCheckPoint;

    Rigidbody rb;
    bool isGrounded;
    float calculatedJumpForce;
    //private float walkSFXCooldown = 0.3f;
    //private float walkSFXTimer = 0f;

    private void OnEnable()
    {
        playerInput = new PlayerControls();

        moveAction = playerInput.PlayerMovement.Movement;
        jumpAction = playerInput.PlayerMovement.Jump;
        sprintAction = playerInput.PlayerMovement.Sprint;

        showCursorAction = playerInput.ToggleCursor.ShowCursor;
        hideCursorAction = playerInput.ToggleCursor.HideCursor;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        calculatedJumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInput.PlayerMovement.Enable();

        playerInput.ToggleCursor.Enable();
        showCursorAction.performed += _ => ShowCursorState();
        hideCursorAction.performed += _ => HideCursorState();
    }

    private void OnDisable()
    {
        showCursorAction.performed -= _ => ShowCursorState();
        hideCursorAction.performed -= _ => HideCursorState();
        
        playerInput.PlayerMovement.Disable(); 
        playerInput.ToggleCursor.Disable(); 
    }

    private void FixedUpdate()
    {
        if (playerInput.PlayerMovement.enabled) 
        {
            bool isSprinting = sprintAction.ReadValue<float>() > 0.1f;
            float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

            Vector2 input = moveAction.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                SoundManager.Instance?.StartWalkingLoop(isSprinting);
            }

            else
            {
                SoundManager.Instance?.StopWalkingLoop();
            }

            Transform cam = Camera.main.transform;
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = (camRight * input.x + camForward * input.y) * currentSpeed * Time.fixedDeltaTime;
            Vector3 targetPosition = rb.position + move;

            rb.MovePosition(targetPosition);

            if (groundCheckPoint != null)
            {
                isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundMask);
            }
            else
            {
                Debug.LogError("groundCheckPoint is not assigned in the Inspector.");
                isGrounded = false;
            }

            if (jumpAction.WasPressedThisFrame() && isGrounded)
            {
                Vector3 velocity = rb.linearVelocity;
                velocity.y = calculatedJumpForce;
                rb.linearVelocity = velocity;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void ShowCursorState()
    {
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerInput.PlayerMovement.Disable();
            Debug.Log("Cursor shown (via 'P'), player movement disabled.");
        }
        else
        {
            Debug.Log("Cursor already visible. Press 'O' to hide.");
        }
    }

    private void HideCursorState()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerInput.PlayerMovement.Enable();
            Debug.Log("Cursor hidden (via 'O'), player movement enabled.");
        }
        else
        {
            Debug.Log("Cursor already hidden. Press 'P' to show.");
        }
    }
}