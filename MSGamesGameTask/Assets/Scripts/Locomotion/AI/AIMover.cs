using System.Collections;
using UnityEngine;

namespace Locomotion.AI
{
    public class AIMover : MonoBehaviour
    {
        public bool IsRotationFinished { get; private set; } = true;
        
        [SerializeField] private float movementSpeed = 7f;
        [SerializeField] [Tooltip("In deg/sec.")] private float rotationSpeed = 1500f;
        
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        private CharacterController _characterController;
        private ForceReceiver _forceReceiver;
        private bool _isMovementDisabled;

        private void Awake()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _characterController = GetComponent<CharacterController>();
            _forceReceiver = GetComponent<ForceReceiver>();
        }

        private void Start()
        {
            _navMeshAgent.speed = movementSpeed;
        }
        
        private void OnValidate()
        {
            ClampRotationSpeedToBeGreaterThan0();
        }

        public void DisableMovement()
        {
            _navMeshAgent.enabled = false;
            _characterController.enabled = false;
            _isMovementDisabled = true;
        }
        
        public void EnableMovement()
        {
            _isMovementDisabled = false;
        }

        public bool MoveToPosition(Vector3 position)
        {
            SwitchMovementToNavmesh();
            if (!_navMeshAgent.isOnNavMesh) return false;
            if (!_navMeshAgent.SetDestination(position)) return false;
            if (!IsPathBuilt(_navMeshAgent.path)) return false;
        
            return true;
        }

        public bool IsPositionReachable(Vector3 position)
        {
            SwitchMovementToNavmesh();
            if (!_navMeshAgent.isOnNavMesh) return false;
            
            UnityEngine.AI.NavMeshPath navMeshPath = new();
            _navMeshAgent.CalculatePath(position, navMeshPath);

            return navMeshPath.status == UnityEngine.AI.NavMeshPathStatus.PathComplete;
        }

        public void StopMovement()
        {
            SwitchMovementToNavmesh();
            if (!_navMeshAgent.isOnNavMesh) return;
            
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;
        }

        public void FacePosition(Vector3 direction)
        {
            SwitchMovementToCharacterController();

            IsRotationFinished = false;
            direction.y = 0f;
           
            StartCoroutine(FacePositionCoroutine(direction, rotationSpeed));
        }

        public void ApplyForces()
        {
            SwitchMovementToCharacterController();

            _characterController.Move(_forceReceiver.ForceDisplacement * Time.deltaTime);
        }

        private void ClampRotationSpeedToBeGreaterThan0()
        {
            rotationSpeed = Mathf.Max(rotationSpeed, float.Epsilon);
        }

        private bool IsPathBuilt(UnityEngine.AI.NavMeshPath path)
        {
            return path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete;
        }

        private bool IsNavMeshAgentDisabled()
        {
            if (!_navMeshAgent.enabled) return true;
            
            print("AIMover: Nav mesh agent IS NOT disabled.");
            return false;
        }

        private void SwitchMovementToNavmesh()
        {
            if (_isMovementDisabled) return;
            _navMeshAgent.enabled = true;
        }

        private void SwitchMovementToCharacterController()
        {
            if (_isMovementDisabled) return;
            _navMeshAgent.enabled = false;
        }

        private IEnumerator FacePositionCoroutine(Vector3 desiredDirection, float speedOnSec)
        {
            Quaternion startingRotation = transform.rotation;
            float rotationAngle = Vector3.Angle(transform.forward, desiredDirection);
            float speedOnFrame = speedOnSec * Time.deltaTime;
            float step = speedOnFrame / rotationAngle;
            
            for (float currentRotationFraction = 0; currentRotationFraction <= 1f; currentRotationFraction += step)
            {
                if (!IsNavMeshAgentDisabled())
                {
                    yield break;
                }
                
                transform.rotation = Quaternion.Lerp(
                    startingRotation,
                    Quaternion.LookRotation(desiredDirection, Vector3.up),
                    currentRotationFraction
                );
                
                yield return new WaitForEndOfFrame();
            }
            
            IsRotationFinished = true;
        }
    }
}
