using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            _currentState?.Tick();
        }

        public void SwitchState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}