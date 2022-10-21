using System;
using UnityEngine;

namespace Locomotion.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float defaultSpeed = 5f;
        [SerializeField] private float maxRaycastDistance = 100f;
        [SerializeField] private LayerMask searchingLayers;
        
        private CharacterController _characterController;

        private Vector3 _position;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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
        
        private void Move(Vector3 direction, float movementSpeed)
        {
            Vector3 movementDisplacement = direction * movementSpeed;
            _characterController.Move(movementDisplacement * Time.deltaTime);
        }
    }
}
