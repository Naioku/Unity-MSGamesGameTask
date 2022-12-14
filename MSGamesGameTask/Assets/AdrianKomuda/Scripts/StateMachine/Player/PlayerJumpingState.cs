using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.Player
{
    public class PlayerJumpingState : PlayerBaseState
    {
        private static readonly int JumpingStateHash = Animator.StringToHash("Jump");
        
        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(JumpingStateHash, StateMachine.AnimationCrossFadeDuration);
            // Jump action is invoked by animation.
        }

        public override void Tick()
        {
            StateMachine.PlayerMover.ApplyMomentum();
            
            if (HasAnimationFinished("Jump") && StateMachine.ForceReceiver.IsFallingDown)
            {
                StateMachine.SwitchState(new PlayerFallingDownState(StateMachine));
                return;
            }
        }

        public override void Exit()
        {
        }
    }
}
