using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        protected void Update()
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
