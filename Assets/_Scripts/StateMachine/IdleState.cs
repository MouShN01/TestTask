public class IdleState : IState
{
    private Hero _hero;

    public IdleState(Hero hero)
    {
        _hero = hero;
    }
    public void Enter()
    {
        _hero.SetAnimationTrigger("Idle");
    }

    public void Update()
    {
        if (_hero.IsTargetActiveAndInRange())
        {
            _hero.HeroStateMachine.TransitionTo(_hero.HeroStateMachine.scopeState);
        }
    }

    public void Exit()
    {
    }
}
