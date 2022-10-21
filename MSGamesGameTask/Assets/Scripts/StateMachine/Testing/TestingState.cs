using UnityEngine;

namespace StateMachine.Testing
{
    public class TestingState : TestingBaseState
    {
        private float _timer = 3f;

        public TestingState(TestingStateMachine stateMachine) : base(stateMachine)
        {}

        public override void Enter()
        {
            Debug.Log("TestingState: Enter");
        }

        public override void Tick()
        {
            if (_timer <= 0f)
            {
                StateMachine.SwitchState(new TestingState(StateMachine));
            }
            Debug.Log("TestingState: Tick: " + _timer);
            _timer -= Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("TestingState: Exit");
        }
    }
}
