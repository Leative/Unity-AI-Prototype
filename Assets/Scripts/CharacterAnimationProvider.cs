using UnityEngine;

public class CharacterAnimationProvider {
    
    private Animator _animator;
    private int isMovingHash;
    private int isRunningHash;


    public CharacterAnimationProvider(Animator animator) {
        _animator = animator;
        isMovingHash = Animator.StringToHash("isMoving");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    public void SetIdleAnimation() {
        _animator.SetBool(isMovingHash, false);
        _animator.SetBool(isRunningHash, false);
        //_animator.SetLayerWeight(1, 0f);
    }
}