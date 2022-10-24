using UnityEngine;

namespace StateMachine.AI
{
    public class AICombatState : AIBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
        
        public AICombatState(AIStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
            StateMachine.AIMover.StopMovement();
        }

        public override void Tick()
        {
            Debug.Log("AICombatState: CurrentTarget: " + StateMachine.CurrentTarget);
            StateMachine.Animator.SetFloat(MovementSpeedHash, 0f, StateMachine.AnimatorDampTime, Time.deltaTime);

            if (StateMachine.CurrentTarget == null)
            {
                StateMachine.SwitchState(new AISuspicionState(StateMachine));
                return;
            }

            if (StateMachine.AIFighter.ReadyForNextAttack())
            {
                PerformAttack();
                return;
            }
        }

        public override void Exit()
        {
        }

        private void PerformAttack()
        {
            Vector3 directionTowardsTarget = StateMachine.CurrentTarget.position - StateMachine.transform.position;
            StateMachine.SwitchState
            (
                new AIRotationState
                (
                    StateMachine,
                    directionTowardsTarget,
                    new AIAttackingState(StateMachine, StateMachine.AIFighter.GetRandomAttack())
                )
            );

            StateMachine.AIFighter.StartTimer();
        }
    }
}
