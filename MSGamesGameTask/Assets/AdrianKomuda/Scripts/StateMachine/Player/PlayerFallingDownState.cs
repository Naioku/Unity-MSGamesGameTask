using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.Player
{
    public class PlayerFallingDownState : PlayerBaseState
    {
        private static readonly int FallDownStateHash = Animator.StringToHash("FallDown");
        
        public PlayerFallingDownState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(FallDownStateHash, StateMachine.AnimationCrossFadeDuration);
        }

        public override void Tick()
        {
            StateMachine.PlayerMover.ApplyMomentum();

            if (StateMachine.ForceReceiver.IsGrounded)
            {
                StateMachine.SwitchState(new PlayerGroundingState(StateMachine));
            }
        }

        public override void Exit()
        {
        }
    }
}
