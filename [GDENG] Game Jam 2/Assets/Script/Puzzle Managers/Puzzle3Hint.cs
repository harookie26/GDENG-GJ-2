using Game.Interactable;
using UnityEngine;

public class Puzzle3Hint
    : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("Puzzle3: Player interacted with the Puzzle 3 Hint.");
    }

    public Transform GetTransform() => transform;

}
