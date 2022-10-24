using System;
using Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action JumpEvent; 
        public event Action<AttackSlotType> AttackEvent; 
        
        public Vector2 MovementValue { get; private set; }
        public Vector3 MouseWorldPosition { get; private set; }
        
        [SerializeField] private float maxRaycastDistance = 40f;
        [SerializeField] private LayerMask searchingLayers;
        
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
            Vector2 mouseScreenPosition = context.ReadValue<Vector2>();

            UpdateMouseWorldPosition(mouseScreenPosition);
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

        private void UpdateMouseWorldPosition(Vector2 mouseScreenPosition)
        {
            bool hasHit = Physics.Raycast(
                GetMouseRay(mouseScreenPosition),
                out RaycastHit hit,
                maxRaycastDistance,
                searchingLayers);
            
            if (!hasHit) return;

            MouseWorldPosition = hit.point;
        }

        private static Ray GetMouseRay(Vector2 mousePosition)
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
    }
}
