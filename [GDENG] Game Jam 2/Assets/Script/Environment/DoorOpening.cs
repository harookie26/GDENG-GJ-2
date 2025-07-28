using UnityEngine;
using DG.Tweening;
using static EventNames;
using Game.Interactable;

public class DoorOpening : MonoBehaviour, IInteractable
{
    public enum DoorOpenDirection { Left, Right }
    [Header("Door Opening Settings")]
    [SerializeField] private DoorOpenDirection openDirection = DoorOpenDirection.Right;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openDuration = 1f;

    private bool isOpen = false;
    private Quaternion initialRotation;

    private void Awake()
    {
        //EventBroadcaster.Instance.AddObserver(ItemEvents.ON_ITEM_INTERACT, Interact);
        initialRotation = transform.rotation;
    }

    private void OnDestroy()
    {
        //EventBroadcaster.Instance.RemoveActionAtObserver(ItemEvents.ON_ITEM_INTERACT, Interact);
    }

    public void Interact()
    {
        Debug.Log("DoorOpening: Player interacted with the door.");
        ToggleDoor();
    }

    public Transform GetTransform() => transform;

    private void ToggleDoor()
    {
        if (isOpen)
        {
            //Close the door
            SoundManager.Instance.PlayDoorClosingSFX();
            transform.DORotateQuaternion(initialRotation, openDuration);
            isOpen = false;
        }

        else
        {
            //Open the door
            SoundManager.Instance.PlayDoorOpeningSFX();
            float targetY = openDirection == DoorOpenDirection.Right ? openAngle : -openAngle;
            Quaternion targetRotation = Quaternion.Euler(0, initialRotation.eulerAngles.y + targetY, 0);
            transform.DORotateQuaternion(targetRotation, openDuration);
            isOpen = true;
        }
    }
}
