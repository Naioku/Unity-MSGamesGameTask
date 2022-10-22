using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action JumpEvent; 
        public event Action AttackEvent; 
        
        public Vector2 MovementValue { get; private set; }
        public Vector2 MousePosition { get; private set; }
        
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

        public void OnMoveMouse(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpEvent?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke();
        }
    }
}
