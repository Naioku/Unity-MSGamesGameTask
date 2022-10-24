using UnityEngine;

namespace Locomotion.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float defaultSpeed = 5f;
        [SerializeField] private float jumpVelocity = 5f;

        private CharacterController _characterController;
        private ForceReceiver _forceReceiver;


        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _forceReceiver = GetComponent<ForceReceiver>();
        }

        public void FaceCharacterToPosition(Vector3 position)
        {
            transform.LookAt(position);
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
        
        // Animation event actions
        public void JumpWithDefaultVelocity()
        {
            _forceReceiver.Jump(jumpVelocity);
        }
    }
}
