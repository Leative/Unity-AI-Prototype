using System.Data.Common;

public class CharacterStateProvider {
    
    public IdleState Idle {get;}
    public RunState Run {get;}
    public GetCoverState GetCover {get;}
    public CoverState Cover {get;}
    public FightState Fight {get;}

    public CharacterStateProvider(CharacterController controller, CharacterAnimationProvider animProvider) {
        Idle = new IdleState(controller, this, animProvider);
        Run = new RunState(controller, this, animProvider);
        GetCover = new GetCoverState(controller, this, animProvider);
        Cover = new CoverState(controller, this, animProvider);
        Fight = new FightState(controller, this, animProvider);
    }
}
