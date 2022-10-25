using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.Player
{
    public class PlayerDeathState : PlayerBaseState
    {
        private static readonly int DeathStateHash = Animator.StringToHash("Death");

        public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(DeathStateHash, StateMachine.AnimationCrossFadeDuration);
            StateMachine.PlayerMover.DisableMovement();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            StateMachine.PlayerMover.EnableMovement();
        }
    }
}
