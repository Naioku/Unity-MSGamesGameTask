﻿using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.AI
{
    public class AISuspicionState : AIBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
        
        private float _suspicionTimer;
        private bool _canMoveToDestination;
        
        public AISuspicionState(AIStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
            StateMachine.AISensor.TargetDetectedEvent += HandleTargetDetection;
            _suspicionTimer = StateMachine.SuspicionTime;
        }

        public override void Tick()
        {
            StateMachine.Animator.SetFloat(MovementSpeedHash, 0f, StateMachine.AnimatorDampTime, Time.deltaTime);
            _suspicionTimer -= Time.deltaTime;
            
            if (_suspicionTimer <= 0f)
            {
                StateMachine.SwitchToDefaultState();
                return;
            }
        }

        public override void Exit()
        {
            StateMachine.AISensor.TargetDetectedEvent -= HandleTargetDetection;
        }
    }
}
