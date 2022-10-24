using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Locomotion.AI
{
    public class AIPatroller : MonoBehaviour
    {
        [SerializeField] private float range;
        [Tooltip("If component can't find the position, increase that value. But FIRST try to increase the range.")]
        [SerializeField] private int searchingIterations = 30;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellingTime = 2f;

        [Header("Gizmos")]
        [SerializeField] private Color rangeColor = Color.green;
        [Tooltip("Waypoint tolerance is the radius of that gizmo.")]
        [SerializeField] private Color waypointColor = Color.yellow;
        
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private Vector3 _currentWaypointPosition;

        private void Start()
        {
            _currentWaypointPosition = transform.position;
        }

        private void OnDrawGizmos()
        {
            DrawRange();
            DrawWaypoint();
        }

        public bool AtWaypoint()
        {
            return AtWaypoint(_currentWaypointPosition);
        }
        
        public bool AtWaypoint(Vector3 position)
        {
            return AIMover.IsDestinationReached(transform.position, position, waypointTolerance);
        }

        public void ResetDwellingTimer()
        {
            _timeSinceArrivedAtWaypoint = 0f;
        }

        public void ReloadWaypoint()
        {
            CalculateRandomPointOnNavMesh();
        }

        public bool ShouldMoveToNextWaypoint()
        {
            return _timeSinceArrivedAtWaypoint > dwellingTime;
        }

        public Vector3 GetCurrentWaypointPosition()
        {
            return _currentWaypointPosition;
        }

        public void UpdateTimer()
        {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void DrawRange()
        {
            Gizmos.color = rangeColor;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void DrawWaypoint()
        {
            Gizmos.color = waypointColor;
            Gizmos.DrawSphere(_currentWaypointPosition, waypointTolerance);
        }

        private void CalculateRandomPointOnNavMesh()
        {
            for (int i = 0; i < searchingIterations; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    _currentWaypointPosition = hit.position;
                }
            }
        }
    }
}
