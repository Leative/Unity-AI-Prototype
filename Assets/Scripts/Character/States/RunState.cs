public class RunState : AbstractCharacterState
{
    public RunState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider)
        : base(controller, stateProvider, animProvider)
    {
    }

    public override void EnterState()
    {
        AnimationProvider.SetRunAnimation();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (Controller.IsCoverTriggered) {
            Controller.SwitchState(StateProvider.Cover);
        }
    }
}