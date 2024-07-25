public class AttackState : IState
{
    private Hero _hero;

    public AttackState(Hero hero)
    {
        _hero = hero;
    }

    public void Enter()
    {
        _hero.SetAnimationTrigger("Attack");
    }

    public void Update()
    {
        _hero.Attack();
        if (!_hero.IsTargetActiveAndInRange())
        {
            _hero.HeroStateMachine.TransitionTo(_hero.HeroStateMachine.resetState);
        }
    }

    public void Exit()
    {
    }
}
