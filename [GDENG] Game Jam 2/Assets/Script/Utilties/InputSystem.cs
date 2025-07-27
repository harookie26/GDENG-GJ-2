using UnityEngine;

namespace Game.InputSystem
{
    public interface IInputReceiver
    {
        void OnInputSubmitted(string input);
    }
}
