using Locomotion.AI;
using UnityEngine;

namespace StateMachine.AI
{
    public class AIChasingState : AIBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
        
        private Vector3 _lastSeenTargetPosition;

        public AIChasingState(AIStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
        }

        public override void Tick()
        {
            StateMachine.Animator.SetFloat(MovementSpeedHash, 1f, StateMachine.AnimatorDampTime, Time.deltaTime);
            StateMachine.CurrentTarget = GetClosestReachableTarget();
            
            if (StateMachine.CurrentTarget == null)
            {
                if (!StateMachine.AIMover.MoveToPosition(_lastSeenTargetPosition) ||
                    AIMover.IsDestinationReached(
                        StateMachine.transform.position,
                        _lastSeenTargetPosition,
                        StateMachine.ChasingWaypointTolerance))
                {
                    StateMachine.SwitchState(new AISuspicionState(StateMachine));
                    return;
                }

                return;
            }
            
            _lastSeenTargetPosition = StateMachine.CurrentTarget.position;
            
            if (StateMachine.AIFighter.IsInAttackRange(_lastSeenTargetPosition))
            {
                StateMachine.SwitchState(new AICombatState(StateMachine));
                return;
            }
            
            if (!StateMachine.AIMover.MoveToPosition(_lastSeenTargetPosition))
            {
                StateMachine.SwitchState(new AISuspicionState(StateMachine));
                return;
            }
        }

        public override void Exit()
        {
        }
    }
}
