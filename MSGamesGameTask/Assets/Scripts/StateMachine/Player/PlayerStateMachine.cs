using Core;
using UnityEngine;

namespace StateMachine.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }

        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
        }

        private void Start()
        {
            SwitchState(new PlayerLocomotionState(this));
        }
    }
}
