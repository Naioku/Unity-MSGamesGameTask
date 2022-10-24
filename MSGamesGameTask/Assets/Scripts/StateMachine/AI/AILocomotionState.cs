using Locomotion.AI;
using UnityEngine;

namespace StateMachine.AI
{
    public class AILocomotionState : AIBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");

        private readonly AIPatroller _aiPatroller;
        private readonly AIMover _aiMover;

        public AILocomotionState(AIStateMachine stateMachine) : base(stateMachine)
        {
            _aiPatroller = StateMachine.AIPatroller;
            _aiMover = StateMachine.AIMover;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);

            StateMachine.AISensor.TargetDetectedEvent += HandleTargetDetection;
        }

        public override void Tick()
        {
            if (_aiPatroller != null)
            {
                if (_aiPatroller.AtWaypoint())
                {
                    _aiMover.StopMovement();
                    _aiPatroller.ResetDwellingTimer();
                    _aiPatroller.ReloadWaypoint();
                }

                if (_aiPatroller.ShouldMoveToNextWaypoint())
                {
                    StateMachine.Animator.SetFloat(MovementSpeedHash, 1f, StateMachine.AnimatorDampTime, Time.deltaTime);

                    if (!_aiMover.MoveToPosition(_aiPatroller.GetCurrentWaypointPosition()))
                    {
                        _aiPatroller.ReloadWaypoint();
                    }
                }
                else
                {
                    StateMachine.Animator.SetFloat(MovementSpeedHash, 0f, StateMachine.AnimatorDampTime, Time.deltaTime);
                }
            
                _aiPatroller.UpdateTimer();
            }
            else
            {
                if (!_aiPatroller.AtWaypoint(StateMachine.GuardingPosition))
                {
                    if (!_aiMover.MoveToPosition(_aiPatroller.GetCurrentWaypointPosition()))
                    {
                        StateMachine.GuardingPosition = StateMachine.transform.position;
                    }
                }
            }
        }

        public override void Exit()
        {
            StateMachine.AISensor.TargetDetectedEvent -= HandleTargetDetection;
        }
    }
}
