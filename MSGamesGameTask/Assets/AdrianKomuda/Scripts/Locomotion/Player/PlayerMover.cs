using UnityEngine;

namespace AdrianKomuda.Scripts.Locomotion.Player
{
    public class PlayerMover : MonoBehaviour
    {
        public bool IsMovementDisabled { get; set; }
        
        [SerializeField] private float defaultSpeed = 5f;
        [SerializeField] private float jumpVelocity = 5f;
        [SerializeField] private float maxRaycastDistance = 40f;
        [SerializeField] private LayerMask searchingLayers;
        
        private CharacterController _characterController;
        private ForceReceiver _forceReceiver;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _forceReceiver = GetComponent<ForceReceiver>();
        }
        
        public void DisableMovement()
        {
            _characterController.enabled = false;
            IsMovementDisabled = true;
        }
        
        public void EnableMovement()
        {
            _characterController.enabled = true;
            IsMovementDisabled = false;
        }

        public void FaceCharacterToPosition(Vector2 screenPosition)
        {
            if (IsMovementDisabled) return;
            transform.LookAt(GetMouseWorldPosition(screenPosition));
        }
        
        public void MoveWithDefaultSpeed(Vector3 direction) => Move(direction, defaultSpeed);

        public void ApplyMomentum()
        {
            Vector3 momentum = _characterController.velocity;
            momentum.y = 0f;
            
            UpdateVelocity(momentum + _forceReceiver.ForceDisplacement);
        }
        
        public void ApplyOnlyForces()
        {
            UpdateVelocity(_forceReceiver.ForceDisplacement);
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
        
        private Vector3 GetMouseWorldPosition(Vector2 mouseScreenPosition)
        {
            bool hasHit = Physics.Raycast(
                GetMouseRay(mouseScreenPosition),
                out RaycastHit hit,
                maxRaycastDistance,
                searchingLayers);
            
            return hasHit ? hit.point : Vector3.zero;
        }

        private static Ray GetMouseRay(Vector2 mousePosition)
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
        
        // Animation event actions
        public void JumpWithDefaultVelocity()
        {
            _forceReceiver.Jump(jumpVelocity);
        }
    }
}
