public interface IStateMachine
{
    void Enter<SType>();
    void Exit<SType>();
}