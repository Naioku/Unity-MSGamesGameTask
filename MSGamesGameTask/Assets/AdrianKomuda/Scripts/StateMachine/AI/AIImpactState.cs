﻿using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.AI
{
    public class AIImpactState : AIBaseState
    {
        private static readonly int ImpactStateHash = Animator.StringToHash("Impact");

        private readonly Vector3 _hitDirection;

        public AIImpactState(AIStateMachine stateMachine, Vector3 hitDirection) : base(stateMachine)
        {
            _hitDirection = hitDirection;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(ImpactStateHash, StateMachine.AnimationCrossFadeDuration);
        }

        public override void Tick()
        {
            StateMachine.AIMover.ApplyForces();

            if (HasAnimationFinished("Impact"))
            {
                Vector3 directionFromReceiver = -_hitDirection;
                StateMachine.SwitchState(new AIRotationState(
                    StateMachine,
                    directionFromReceiver,
                    new AISuspicionState(StateMachine)
                ));
                return;
            }
        }

        public override void Exit()
        {
        }
    }
}
