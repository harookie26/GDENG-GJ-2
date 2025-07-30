using Game.Interactable;
using UnityEngine;

public class TargetChair : MonoBehaviour, IInteractable
{
    private void Awake()
    {
    }

    public void Interact()
    {
    }

    public Transform GetTransform() => transform;
}