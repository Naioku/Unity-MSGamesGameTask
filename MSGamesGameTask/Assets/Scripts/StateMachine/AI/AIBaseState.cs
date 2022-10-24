using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.AI
{
    public abstract class AIBaseState : State
    {
        protected readonly AIStateMachine StateMachine;

        protected AIBaseState(AIStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        protected void HandleTargetDetection(List<Transform> detectedTargets)
        {
            if (!IsAnyTargetReachable()) return;

            StateMachine.SwitchState(new AIChasingState(StateMachine));
        }
        
        protected Transform GetClosestReachableTarget()
        {
            Vector3 aiPosition = StateMachine.transform.position;
            Transform closestTarget = null;
            float distanceToClosestTargetSquared = Mathf.Infinity;
            foreach (Transform detectedTarget in StateMachine.DetectedTargets)
            {
                if (!StateMachine.AIMover.IsPositionReachable(detectedTarget.position)) continue;

                float distanceToTargetSquared = Vector3.SqrMagnitude(detectedTarget.position - aiPosition);
                if (distanceToTargetSquared < distanceToClosestTargetSquared)
                {
                    distanceToClosestTargetSquared = distanceToTargetSquared;
                    closestTarget = detectedTarget;
                }
            }
        
            return closestTarget;
        }

        protected bool HasAnimationFinished(string tag)
        {
            return GetNormalizedAnimationTime(StateMachine.Animator, tag) >= 1f;
        }

        private bool IsAnyTargetReachable()
        {
            return GetClosestReachableTarget() != null;
        }
    }
}
