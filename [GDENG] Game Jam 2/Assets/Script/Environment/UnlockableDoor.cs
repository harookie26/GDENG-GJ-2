using UnityEngine;
using DG.Tweening;
using Game.Interactable;
using TMPro;

public class UnlockableDoor : MonoBehaviour, IInteractable
{
    public enum DoorOpenDirection { Left, Right }
    [Header("Door Opening Settings")]
    [SerializeField] private DoorOpenDirection openDirection = DoorOpenDirection.Right;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openDuration = 1f;

    [Header("Lock Settings")]
    [SerializeField] private string lockedPrompt = "The door is locked. There seems to be a note next to it.";

    [Header("Inner Dialogue UI")]
    public GameObject innerDialoguePanel;
    public TextMeshProUGUI innerDialogueText;
    [SerializeField] private float innerDialogueDuration = 2f;

    private bool isOpen = false;
    private Quaternion initialRotation;
    private bool isUnlocked = false;
    private bool playerInRange = false;
    private Coroutine hideDialogueCoroutine;

    private void Awake()
    {
        initialRotation = transform.rotation;
        isUnlocked = false;
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(false);
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        if (playerInRange)
            InteractionPrompt.Instance?.ShowPrompt("Press F to interact.");
    }

    public void Interact()
    {
        if (!isUnlocked)
        {
            InteractionPrompt.Instance?.ShowPrompt(lockedPrompt);
            ShowInnerDialogue(lockedPrompt);
            return;
        }

        Debug.Log("UnlockableDoor: Player interacted with the door.");
        ToggleDoor();
    }

    public Transform GetTransform() => transform;

    private void ToggleDoor()
    {
        if (isOpen)
        {
            transform.DORotateQuaternion(initialRotation, openDuration);
            isOpen = false;
        }
        else
        {
            float targetY = openDirection == DoorOpenDirection.Right ? openAngle : -openAngle;
            Quaternion targetRotation = Quaternion.Euler(0, initialRotation.eulerAngles.y + targetY, 0);
            transform.DORotateQuaternion(targetRotation, openDuration);
            isOpen = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt(isUnlocked ? "Press F to interact." : lockedPrompt);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
            HideInnerDialogue();
        }
    }

    private void ShowInnerDialogue(string message)
    {
        if (innerDialogueText != null)
            innerDialogueText.text = message;
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(true);

        if (hideDialogueCoroutine != null)
            StopCoroutine(hideDialogueCoroutine);
        hideDialogueCoroutine = StartCoroutine(HideInnerDialogueAfterDelay());
    }

    private void HideInnerDialogue()
    {
        if (innerDialoguePanel != null)
            innerDialoguePanel.SetActive(false);

        if (hideDialogueCoroutine != null)
        {
            StopCoroutine(hideDialogueCoroutine);
            hideDialogueCoroutine = null;
        }
    }

    private System.Collections.IEnumerator HideInnerDialogueAfterDelay()
    {
        yield return new WaitForSeconds(innerDialogueDuration);
        HideInnerDialogue();
    }
}
