using System;

public class StateMachine
{
    private IState CurrentState { get; set; }

    public IdleState idleState;
    public ScopeState scopeState;
    public AttackState attackState;
    public ResetState resetState;

    public StateMachine(Hero hero)
    {
        idleState = new IdleState(hero);
        scopeState = new ScopeState(hero);
        attackState = new AttackState(hero);
        resetState = new ResetState(hero);
    }

    public event Action<IState> StateChanged;

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
        StateChanged?.Invoke(state);
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        
        StateChanged?.Invoke(nextState);
    }

    public void Update()
    {
        if(CurrentState != null) CurrentState.Update();
    }
}
