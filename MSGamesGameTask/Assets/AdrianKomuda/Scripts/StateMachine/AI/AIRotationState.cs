using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.AI
{
    public class AIRotationState : AIBaseState
    {
        private readonly AIBaseState _nextState;
        private readonly Vector3 _direction;
        
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");

        public AIRotationState(AIStateMachine stateMachine, Vector3 direction, AIBaseState nextState) : base(stateMachine)
        {
            _direction = direction;
            _nextState = nextState;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
            StateMachine.AIMover.FacePosition(_direction);
        }

        public override void Tick()
        {
            StateMachine.Animator.SetFloat(MovementSpeedHash, 0f, StateMachine.AnimatorDampTime, Time.deltaTime);

            if (StateMachine.AIMover.IsRotationFinished)
            {
                StateMachine.SwitchState(_nextState);
            }
        }

        public override void Exit()
        {
        }
    }
}
