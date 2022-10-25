using Combat;
using Combat.AI;
using Locomotion.AI;
using UnityEngine;

namespace StateMachine.AI
{
    public class AIStateMachine : StateMachine
    {
        [field: SerializeField] public float AnimationCrossFadeDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float AnimatorDampTime { get; private set; } = 0.05f;
        [field: SerializeField] public float ChasingWaypointTolerance { get; private set; } = 1.5f;
        [field: SerializeField] public float SuspicionTime { get; set; } = 1f;
        
        public Vector3 GuardingPosition { get; set; }
        public Transform CurrentTarget { get; set; }
        public Animator Animator { get; private set; }
        public AIMover AIMover { get; private set; }
        public AIPatroller AIPatroller { get; private set; }
        public AIFighter AIFighter { get; private set; }
        public AISensor AISensor { get; private set; }

        private Health _health;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            AIMover = GetComponent<AIMover>();
            AIPatroller = GetComponent<AIPatroller>();
            AIFighter = GetComponent<AIFighter>();
            AISensor = GetComponent<AISensor>();
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            GuardingPosition = transform.position;
            SwitchState(new AILocomotionState(this));
        }

        private void OnEnable()
        {
            AISensor.SensorUpdateEvent += HandleSensorUpdate;
            _health.TakeDamageEvent += HandleTakeDamage;
            _health.DeathEvent += HandleDeath;
        }

        private void OnDisable()
        {
            AISensor.SensorUpdateEvent -= HandleSensorUpdate;
            _health.TakeDamageEvent -= HandleTakeDamage;
            _health.DeathEvent -= HandleDeath;
        }

        private void HandleSensorUpdate()
        {
            if (!AISensor.DetectedObjects.Contains(CurrentTarget))
            {
                CurrentTarget = null;
            }
        }

        private void HandleTakeDamage(Vector3 hitDirection)
        {
            SwitchState(new AIImpactState(this, hitDirection));
        }

        private void HandleDeath()
        {
            SwitchState(new AIDeathState(this));
        }
    }
}
