using UnityEngine;
using TMPro;
using static EventNames;
using Game.Interactable;

public class ExamRoomTableInteraction : MonoBehaviour, IInteractable
{
    public TextMeshPro deliriousMessageText;
    public TypewriterEffect typewriter;

    [Header("Fragment Model")]
    public GameObject fragmentModel; 

    private bool playerInRange = false;
    private bool hasInteractedWithTable = false;

    public void Interact()
    {
        hasInteractedWithTable = true;

        EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE);
        EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED);
    }

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ShowMessage);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, HideMessage);
        EventBroadcaster.Instance.AddObserver(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED, ShowFragment);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ShowMessage);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, HideMessage);
        EventBroadcaster.Instance.RemoveActionAtObserver(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED, ShowFragment);
    }

    private void ShowMessage()
    {
        if (hasInteractedWithTable && deliriousMessageText != null && typewriter != null)
        {
            deliriousMessageText.gameObject.SetActive(true);
            string message = "hidden beneath the skin";
            typewriter.StartTyping(message, deliriousMessageText);
        }
    }

    private void HideMessage()
    {
        if (deliriousMessageText != null)
        {
            typewriter.Clear(deliriousMessageText);
            deliriousMessageText.gameObject.SetActive(false);
            hasInteractedWithTable = false;
        }
        if (fragmentModel != null)
            fragmentModel.SetActive(false);
    }

    private void ShowFragment()
    {
        if (fragmentModel != null)
        {
            fragmentModel.SetActive(true);
            Debug.Log("Fragment model activated after classroom puzzle solved.");
        }
    }

    public Transform GetTransform() => transform;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt("Press F to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
            EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
        }
    }
}

