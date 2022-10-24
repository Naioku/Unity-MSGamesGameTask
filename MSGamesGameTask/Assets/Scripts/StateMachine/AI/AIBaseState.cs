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
            StateMachine.SwitchState(new AIChasingState(StateMachine, detectedTargets));
        }
        
        protected bool HasAnimationFinished(string tag)
        {
            return GetNormalizedAnimationTime(StateMachine.Animator, tag) >= 1f;
        }
    }
}
