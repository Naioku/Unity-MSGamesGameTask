using UnityEngine;

namespace Locomotion
{
    public class ForceReceiver : MonoBehaviour
    {
        public Vector3 ForceDisplacement => Vector3.up * _verticalVelocity;

        [SerializeField] private float gravityAmplifier = 4f;

        private CharacterController _characterController;
        private float _verticalVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (_verticalVelocity < 0f && _characterController.isGrounded)
            {
                _verticalVelocity = Physics.gravity.y * gravityAmplifier * Time.deltaTime;
            }
            else
            {
                _verticalVelocity += Physics.gravity.y * gravityAmplifier * Time.deltaTime;
            }
        }
        
        internal void Jump(float jumpVelocity)
        {
            _verticalVelocity += jumpVelocity;
        }
    }
}
