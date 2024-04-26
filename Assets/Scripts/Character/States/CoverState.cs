public class CoverState : AbstractCharacterState
{
    public CoverState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider) : base(controller, stateProvider, animProvider)
    {
    }

    public override void EnterState()
    {
        AnimationProvider.SetCoverAnimation();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}