using UnityEngine;

namespace Locomotion.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float defaultSpeed = 5f;
        [SerializeField] private float jumpVelocity = 5f;
        [SerializeField] private float maxRaycastDistance = 40f;
        [SerializeField] private LayerMask searchingLayers;
        
        private CharacterController _characterController;
        private ForceReceiver _forceReceiver;

        private Vector3 _position;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _forceReceiver = GetComponent<ForceReceiver>();
        }

        public void FaceCharacterToPosition(Vector2 mousePosition)
        {
            bool hasHit = Physics.Raycast(GetMouseRay(mousePosition), out RaycastHit hit, maxRaycastDistance, searchingLayers);
            if (!hasHit) return;

            _position = hit.point;
            
            transform.LookAt(_position);
        }

        public void MoveWithDefaultSpeed(Vector3 direction) => Move(direction, defaultSpeed);
        
        private static Ray GetMouseRay(Vector2 mousePosition)
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
        
        public void ApplyMomentum()
        {
            Vector3 momentum = _characterController.velocity;
            momentum.y = 0f;
            
            UpdateVelocity(momentum + _forceReceiver.ForceDisplacement);
        }
        
        private void Move(Vector3 direction, float movementSpeed)
        {
            Vector3 movementDisplacement = direction * movementSpeed;
            UpdateVelocity(movementDisplacement + _forceReceiver.ForceDisplacement);
        }
        
        private void UpdateVelocity(Vector3 movement)
        {
            _characterController.Move(movement * Time.deltaTime);
        }
        
        // Animation event actions
        public void JumpWithDefaultVelocity()
        {
            _forceReceiver.Jump(jumpVelocity);
        }
    }
}
