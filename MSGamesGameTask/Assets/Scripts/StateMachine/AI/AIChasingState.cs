using System.Collections.Generic;
using Locomotion.AI;
using UnityEngine;

namespace StateMachine.AI
{
    public class AIChasingState : AIBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
        
        private readonly List<Transform> _detectedTargets;
        private Transform _currentTarget;
        private Vector3 _lastSeenTargetPosition;

        public AIChasingState(AIStateMachine stateMachine, List<Transform> detectedTargets) : base(stateMachine)
        {
            _detectedTargets = detectedTargets;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
        }

        public override void Tick()
        {
            StateMachine.Animator.SetFloat(MovementSpeedHash, 1f, StateMachine.AnimatorDampTime, Time.deltaTime);
            
            _currentTarget = GetClosestReachableTarget();

            if (_currentTarget == null)
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
            
            _lastSeenTargetPosition = _currentTarget.position;
            
            if (StateMachine.AIFighter.IsInAttackRange(_lastSeenTargetPosition))
            {
                StateMachine.SwitchState(new AIAttackingState(StateMachine, StateMachine.AIFighter.GetRandomAttack()));
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
        
        private Transform GetClosestReachableTarget()
        {
            Vector3 aiPosition = StateMachine.transform.position;
            Transform closestTarget = null;
            float distanceToClosestTargetSquared = Mathf.Infinity;
            foreach (Transform detectedTarget in _detectedTargets)
            {
                if(!StateMachine.AIMover.IsPositionReachable(detectedTarget.position)) continue;
                
                float distanceToTargetSquared = Vector3.SqrMagnitude(detectedTarget.position - aiPosition);
                if (distanceToTargetSquared < distanceToClosestTargetSquared)
                {
                    distanceToClosestTargetSquared = distanceToTargetSquared;
                    closestTarget = detectedTarget;
                }
            }
        
            return closestTarget;
        }
    }
}
