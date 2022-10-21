namespace StateMachine.Testing
{
    public abstract class TestingBaseState : State
    {
        protected readonly TestingStateMachine StateMachine;

        protected TestingBaseState(TestingStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}