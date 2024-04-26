using UnityEngine;

public class CharacterAnimationProvider {
    
    private Animator _animator;
    private int isMovingHash;
    private int isRunningHash;
    private int isKneelingHash;


    public CharacterAnimationProvider(Animator animator) {
        _animator = animator;
        isMovingHash = Animator.StringToHash("isMoving");
        isRunningHash = Animator.StringToHash("isRunning");
        isKneelingHash = Animator.StringToHash("isKneeling");
    }

    public void SetIdleAnimation() {
        _animator.SetBool(isMovingHash, false);
        _animator.SetBool(isRunningHash, false);
        _animator.SetBool(isKneelingHash, false);
        
        //_animator.SetLayerWeight(1, 0f);
    }

    public void SetRunAnimation() {
        _animator.SetBool(isMovingHash, true);
        _animator.SetBool(isRunningHash, true);
        _animator.SetBool(isKneelingHash, false);
        //_animator.SetLayerWeight(1, 0f);
    }

    public void SetCoverAnimation() {
        _animator.SetBool(isKneelingHash, true);
        //_animator.SetLayerWeight(1, 0f);
    }
}