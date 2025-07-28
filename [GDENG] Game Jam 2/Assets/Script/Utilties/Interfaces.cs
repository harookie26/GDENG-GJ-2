using UnityEngine;

namespace Game.InputSystem
{
    public interface IInputReceiver
    {
        void OnInputSubmitted(string input);
    }
}

namespace Game.Interactable
{
    public interface IInteractable
    {
        void Interact();
        Transform GetTransform();
    }
}