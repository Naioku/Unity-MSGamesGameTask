using Core;
using Locomotion.Player;
using UnityEngine;

namespace StateMachine.Player
{
    public class PlayerLocomotionState : PlayerBaseState
    {
        private readonly PlayerMover _playerMover;
        private readonly InputReader _inputReader;

        public PlayerLocomotionState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _playerMover = StateMachine.PlayerMover;
            _inputReader = StateMachine.InputReader;
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            Vector2 movementInputValue = _inputReader.MovementValue;

            _playerMover.MoveWithDefaultSpeed(new Vector3(
                movementInputValue.x,
                0f,
                movementInputValue.y)
            );
            
            _playerMover.FaceCharacterToPosition(_inputReader.MousePosition);
        }

        public override void Exit()
        {
        }
    }
}
