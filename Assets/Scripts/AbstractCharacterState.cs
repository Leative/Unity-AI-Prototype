using UnityEngine;

public abstract class AbstractCharacterState
{
    protected CharacterController Controller {get;}
    protected CharacterStateProvider StateProvider {get;}
    protected CharacterAnimationProvider AnimationProvider {get;}

      // public HumanoidState NextState { get; protected set; }
    public bool SwitchState { get; protected set; }

    public AbstractCharacterState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider)
    {
        Controller = controller;
        StateProvider = stateProvider;
        AnimationProvider = animProvider;
    }

    // No-ops meant to be implemented by states as needed
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
