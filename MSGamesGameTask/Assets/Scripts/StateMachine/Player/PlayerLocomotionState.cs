using Combat;
using Core;
using Locomotion.Player;
using UnityEngine;

namespace StateMachine.Player
{
    public class PlayerLocomotionState : PlayerBaseState
    {
        private static readonly int LocomotionStateHash = Animator.StringToHash("Locomotion");
        private static readonly int ForwardMovementSpeedHash = Animator.StringToHash("ForwardMovementSpeed");
        private static readonly int RightMovementSpeedHash = Animator.StringToHash("RightMovementSpeed");
        
        private readonly PlayerMover _playerMover;
        private readonly InputReader _inputReader;

        public PlayerLocomotionState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _playerMover = StateMachine.PlayerMover;
            _inputReader = StateMachine.InputReader;
        }

        public override void Enter()
        {
            StateMachine.InputReader.JumpEvent += HandleJump;
            StateMachine.InputReader.AttackEvent += HandleAttack;
            
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionStateHash, StateMachine.AnimationCrossFadeDuration);
        }

        public override void Tick()
        {
            Vector2 movementInputValue = _inputReader.MovementValue;

            Vector3 movementDirection = new Vector3(
                movementInputValue.x,
                0f,
                movementInputValue.y);
            _playerMover.MoveWithDefaultSpeed(movementDirection);
            UpdateAnimator(movementDirection);
            
            _playerMover.FaceCharacterToPosition(_inputReader.MouseWorldPosition);
        }

        public override void Exit()
        {
            StateMachine.InputReader.JumpEvent -= HandleJump;
            StateMachine.InputReader.AttackEvent -= HandleAttack;
        }

        private void UpdateAnimator(Vector3 movementGlobalDirection)
        {
            Vector3 movementLocalDirection = StateMachine.transform.InverseTransformDirection(movementGlobalDirection);
            
            float movementForwardValue = movementLocalDirection.z;
            float movementRightValue = movementLocalDirection.x;

            StateMachine.Animator.SetFloat(ForwardMovementSpeedHash, movementForwardValue, StateMachine.AnimatorDampTime, Time.deltaTime);
            StateMachine.Animator.SetFloat(RightMovementSpeedHash, movementRightValue, StateMachine.AnimatorDampTime, Time.deltaTime);
        }

        private void HandleJump()
        {
            StateMachine.SwitchState(new PlayerJumpingState(StateMachine));
        }

        private void HandleAttack(AttackType attackType)
        {
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, StateMachine.Fighter.GetAttack(attackType)));
        }
    }
}
