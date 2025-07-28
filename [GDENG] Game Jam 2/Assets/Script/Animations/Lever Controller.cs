using Game.Interactable;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header("Lever Animator")]
    public Animator leverAnimator;

    [Header("Prompt Label and Message")]
    [Tooltip("Label shown above the prompt message.")]
    public string label = "Lever";
    [Tooltip("Text shown when player is near the lever.")]
    public string promptMessage = "Press F to interact";

    public event System.Action<LeverController> OnLeverPulled;

    private bool isOn = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                if (leverAnimator != null && !GameState.LeverPuzzleSolved)
                {
                    SoundManager.Instance.PlayLeverPullSFX();
                    leverAnimator.SetTrigger("On");
                }

                isOn = false;
            }
            else
            {
                if (leverAnimator != null && !GameState.LeverPuzzleSolved)
                {
                    SoundManager.Instance.PlayLeverPullSFX();
                    leverAnimator.SetTrigger("Off");
                }
                isOn = true;
            }
            OnLeverPulled?.Invoke(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt($"{label}\n{promptMessage}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
        }
    }
}
