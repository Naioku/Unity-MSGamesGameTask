using Combat;

namespace StateMachine.AI
{
    public class AIAttackingState : AIBaseState
    {
        private readonly Attack _attack;
        private bool _forceAlreadyApplied;

        public AIAttackingState(AIStateMachine stateMachine, Attack attack) : base(stateMachine)
        {
            _attack = attack;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        }

        public override void Tick()
        {
            StateMachine.AIMover.ApplyForces();
            
            if (GetNormalizedAnimationTime(StateMachine.Animator, "Attack") >= _attack.DisplacementApplicationNormalizedTime)
            {
                TryForceApplication();
            }
            
            if (HasAnimationFinished("Attack"))
            {
                StateMachine.SwitchState(new AISuspicionState(StateMachine));
                return;
            }
        }

        public override void Exit()
        {
        }
        
        private void TryForceApplication()
        {
            if (_forceAlreadyApplied) return;
            
            StateMachine.ForceReceiver.AddForce(
                StateMachine.transform.forward * _attack.AttackerDisplacement,
                _attack.AttackerDisplacementSmoothingTime
            );
            
            _forceAlreadyApplied = true;
        }
    }
}
