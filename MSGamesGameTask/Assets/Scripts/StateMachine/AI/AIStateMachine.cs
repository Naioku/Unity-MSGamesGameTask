using System.Collections.Generic;
using Combat.AI;
using Locomotion;
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
        public List<Transform> DetectedTargets { get; private set; }
        public Animator Animator { get; private set; }
        public AIMover AIMover { get; private set; }
        public AIPatroller AIPatroller { get; private set; }
        public AIFighter AIFighter { get; private set; }
        public AISensor AISensor { get; private set; }
        public ForceReceiver ForceReceiver { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            AIMover = GetComponent<AIMover>();
            AIPatroller = GetComponent<AIPatroller>();
            AIFighter = GetComponent<AIFighter>();
            AISensor = GetComponent<AISensor>();
            ForceReceiver = GetComponent<ForceReceiver>();
        }

        private void Start()
        {
            GuardingPosition = transform.position;
            SwitchState(new AILocomotionState(this));
        }

        private void OnEnable()
        {
            AISensor.TargetDetectedEvent += HandleTargetDetection;
        }

        private void OnDisable()
        {
            AISensor.TargetDetectedEvent -= HandleTargetDetection;
        }

        private new void Update()
        {
            base.Update();
            print("CurrentTarget: " + CurrentTarget);
        }
        
        private void HandleTargetDetection(List<Transform> detectedTargets)
        {
            DetectedTargets = detectedTargets;
            
            if (!DetectedTargets.Contains(CurrentTarget))
            {
                CurrentTarget = null;
            }
        }
    }
}
