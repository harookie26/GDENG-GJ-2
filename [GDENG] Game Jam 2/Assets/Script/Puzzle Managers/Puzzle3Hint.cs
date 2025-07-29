using Game.Interactable;
using UnityEngine;

public class Puzzle3Hint
    : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("Puzzle3: Player interacted with the Puzzle 3 Hint.");
        HintUI.Instance.ShowHint("GDENG01");
    }

    public Transform GetTransform() => transform;

    private void CloseHint()
    {

    }

}
