using UnityEngine;

namespace Locomotion
{
    public class ForceReceiver : MonoBehaviour
    {
        public Vector3 ForceDisplacement => Vector3.up * _verticalVelocity;
        public bool IsFallingDown => !IsGrounded && _characterController.velocity.y < UnitaryAcceleration;
        public bool IsGrounded => _characterController.isGrounded;
        
        [SerializeField] private float gravityAmplifier = 4f;
        [SerializeField] private float defaultImpactSmoothingTime = 0.1f;

        private CharacterController _characterController;
        private float _verticalVelocity;
        private float _impactSmoothingTime;
        private Vector3 _impact;
        private Vector3 _dampingVelocity;
        
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
            
            _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _impactSmoothingTime);

            if (_impact.sqrMagnitude < 0.2f * 0.2f)
            {
                _impact = Vector3.zero;
            }
        }
        
        internal void Jump(float jumpVelocity)
        {
            _verticalVelocity += jumpVelocity;
        }

        public void AddForce(Vector3 force)
        {
            AddForce(force, defaultImpactSmoothingTime);
        }
        
        public void AddForce(Vector3 force, float impactSmoothingTime)
        {
            _impactSmoothingTime = impactSmoothingTime;
            _impact += force;
        }
    }
}
