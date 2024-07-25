public class ScopeState : IState
{
    private Hero _hero;

    public ScopeState(Hero hero)
    {
        _hero = hero;
    }

    public void Enter()
    {
        _hero.PrepareForAttack();
        _hero.SetAnimationTrigger("Scope");
    }

    public void Update()
    {
        _hero.ScopeTarget();
        if (!_hero.HeroEuler.IsRotating)
        {
            _hero.HeroStateMachine.TransitionTo(_hero.HeroStateMachine.attackState);
        }
    }
    public void Exit()
    {}
}
