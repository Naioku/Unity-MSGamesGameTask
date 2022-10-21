using UnityEngine;

namespace Locomotion.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float defaultSpeed = 5f;
        
        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MoveWithDefaultSpeed(Vector3 direction) => Move(direction, defaultSpeed);
        
        private void Move(Vector3 direction, float movementSpeed)
        {
            Vector3 movementDisplacement = direction * movementSpeed;
            _characterController.Move(movementDisplacement * Time.deltaTime);
        }
    }
}
