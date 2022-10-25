using System;
using AdrianKomuda.Scripts.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdrianKomuda.Scripts.Core
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action JumpEvent; 
        public event Action<AttackSlotType> AttackEvent; 
        public event Action PauseEvent; 
        
        public Vector2 MovementValue { get; private set; }
        public Vector2 MouseScreenPosition { get; private set; }
        
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
            MouseScreenPosition = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpEvent?.Invoke();
        }

        public void OnAttackLeft(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke(AttackSlotType.LeftHand);
        }

        public void OnAttackRight(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke(AttackSlotType.RightHand);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            PauseEvent?.Invoke();
        }
    }
}
