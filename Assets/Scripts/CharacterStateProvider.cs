using System.Data.Common;

public class CharacterStateProvider {
    
    public IdleState Idle {get;}

    public CharacterStateProvider(CharacterController controller, CharacterAnimationProvider animProvider) {
        Idle = new IdleState(controller, this, animProvider);
    }
}
