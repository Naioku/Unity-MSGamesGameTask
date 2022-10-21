namespace StateMachine.Testing
{
    public class TestingStateMachine : StateMachine
    {
        private void Start()
        {
            SwitchState(new TestingState(this));
        }
    }
}
