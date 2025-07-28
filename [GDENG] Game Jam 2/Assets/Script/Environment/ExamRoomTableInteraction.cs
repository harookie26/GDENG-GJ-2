using UnityEngine;
using TMPro;
using static EventNames;
public class ExamRoomTableInteraction : MonoBehaviour
{
    public TextMeshPro deliriousMessageText;
    public TypewriterEffect typewriter;

    private bool playerInRange = false;
    private bool hasInteractedWithTable = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasInteractedWithTable)
        {
            hasInteractedWithTable = true;



            EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE);
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED);
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ShowMessage);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, HideMessage);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ShowMessage);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, HideMessage);
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
    }

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
