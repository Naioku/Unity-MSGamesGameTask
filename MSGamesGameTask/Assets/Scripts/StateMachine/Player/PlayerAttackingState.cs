using Combat;

namespace StateMachine.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private readonly Attack _attack;
        
        private bool _isComboBroken;

        public PlayerAttackingState(PlayerStateMachine stateMachine, Attack attack) : base(stateMachine)
        {
            _attack = attack;
        }
        
        public override void Enter()
        {
            StateMachine.InputReader.AttackEvent += HandleAttack;

            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        }

        public override void Tick()
        {
            if (HasAnimationFinished("Attack"))
            {
                StateMachine.SwitchState(new PlayerLocomotionState(StateMachine));
                return;
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.AttackEvent -= HandleAttack;
        }
        
        private void HandleAttack(AttackSlotType attackSlotType)
        {
            if (!ReadyForNextAttack(GetNormalizedAnimationTime(StateMachine.Animator, "Attack")))
            {
                _isComboBroken = true;
                return;
            }
            
            if (_isComboBroken) return;
            
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, StateMachine.Fighter.GetAttack(attackSlotType)));
        }

        private bool ReadyForNextAttack(float normalizedAnimationTime)
        {
            return normalizedAnimationTime >= _attack.NextComboAttackNormalizedTime;
        }
    }
}
