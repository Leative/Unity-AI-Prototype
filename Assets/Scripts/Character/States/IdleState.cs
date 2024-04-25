public class IdleState : AbstractCharacterState
{
    public IdleState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider)
        : base(controller, stateProvider, animProvider)
    {
    }

    public override void EnterState()
    {
        AnimationProvider.SetIdleAnimation();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (Controller.IsCoverTriggered)
        {
            Controller.SwitchState(StateProvider.Cover);            
        }
    }
}
