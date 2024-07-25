public class ResetState : IState
{
    private Hero _hero;

    public ResetState(Hero hero)
    {
        _hero = hero;
    }

    public void Enter()
    {
        _hero.Reload();
    }

    public void Update()
    {
        _hero.BackToInitialPosition();
        if (!_hero.HeroEuler.IsRotating)
        {
            _hero.HeroStateMachine.TransitionTo(_hero.HeroStateMachine.idleState);
        }
    }

    public void Exit()
    {
        
    }
}
