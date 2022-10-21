using UnityEngine;

namespace StateMachine.Player
{
    public class PlayerLocomotionState : PlayerBaseState
    {
        public PlayerLocomotionState(PlayerStateMachine stateMachine) : base(stateMachine)
        {}

        public override void Enter()
        {
        }

        public override void Tick()
        {
            Vector2 movementInputValue = StateMachine.InputReader.MovementValue;

            StateMachine.PlayerMover.MoveWithDefaultSpeed(new Vector3(
                movementInputValue.x,
                0f,
                movementInputValue.y)
            );
        }

        public override void Exit()
        {
        }
    }
}
