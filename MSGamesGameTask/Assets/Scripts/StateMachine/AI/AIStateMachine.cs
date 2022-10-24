using Locomotion.AI;
using UnityEngine;

namespace StateMachine.AI
{
    public class AIStateMachine : StateMachine
    {
        [field: SerializeField] public float AnimationCrossFadeDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float AnimatorDampTime { get; private set; } = 0.05f;
        
        public Vector3 GuardingPosition { get; set; }
        public Animator Animator { get; private set; }
        public AIMover AIMover { get; private set; }
        public AIPatroller AIPatroller { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            AIMover = GetComponent<AIMover>();
            AIPatroller = GetComponent<AIPatroller>();
        }

        private void Start()
        {
            GuardingPosition = transform.position;
            SwitchState(new AILocomotionState(this));
        }
    }
}
