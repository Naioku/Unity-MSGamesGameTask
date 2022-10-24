namespace StateMachine.AI
{
    public abstract class AIBaseState : State
    {
        protected readonly AIStateMachine StateMachine;

        protected AIBaseState(AIStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
