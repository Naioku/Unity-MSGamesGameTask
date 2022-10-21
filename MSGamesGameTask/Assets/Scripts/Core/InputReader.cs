using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Vector2 MovementValue { get; private set; }
        
        private Controls _controls;

        private void Start()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);

            _controls.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }
    }
}
