using Core;
using Locomotion.Player;

namespace StateMachine.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public InputReader InputReader { get; private set; }
        public PlayerMover PlayerMover { get; private set; }

        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            PlayerMover = GetComponent<PlayerMover>();
        }

        private void Start()
        {
            SwitchState(new PlayerLocomotionState(this));
        }
    }
}
