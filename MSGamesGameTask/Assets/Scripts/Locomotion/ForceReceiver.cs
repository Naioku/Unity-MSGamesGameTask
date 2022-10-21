using UnityEngine;

namespace Locomotion
{
    public class ForceReceiver : MonoBehaviour
    {
        public Vector3 ForceDisplacement => Vector3.up * _verticalVelocity;
        public bool IsFallingDown => !IsGrounded && _characterController.velocity.y < UnitaryAcceleration;
        public bool IsGrounded => _characterController.isGrounded;
        
        [SerializeField] private float gravityAmplifier = 4f;

        private CharacterController _characterController;
        private float _verticalVelocity;
        
        private float UnitaryAcceleration => Physics.gravity.y * gravityAmplifier * Time.deltaTime;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (_verticalVelocity < 0f && IsGrounded)
            {
                _verticalVelocity = UnitaryAcceleration;
            }
            else
            {
                _verticalVelocity += UnitaryAcceleration;
            }
        }
        
        internal void Jump(float jumpVelocity)
        {
            _verticalVelocity += jumpVelocity;
        }
    }
}
