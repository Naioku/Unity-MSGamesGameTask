using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.AI
{
    public class AIFrozenState : AIBaseState
    {
        private static readonly int FrozenStateHash = Animator.StringToHash("Frozen");
        
        public AIFrozenState(AIStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(FrozenStateHash, StateMachine.AnimationCrossFadeDuration);
            StateMachine.AIMover.DisableMovement(false);
        }

        public override void Tick() {}

        public override void Exit()
        {
            StateMachine.AIMover.EnableMovement();
        }
    }
}
