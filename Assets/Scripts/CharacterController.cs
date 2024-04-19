using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public bool IsCoverPressed{get; set;}

    private AbstractCharacterState _currentState;
    private CharacterStateProvider _stateProvider;
    private CharacterAnimationProvider _animProvider;    

    void Awake() {
        _animProvider = new CharacterAnimationProvider(animator);
        _stateProvider = new CharacterStateProvider(this, _animProvider);        
    }

    void Start()
    {
        _currentState = _stateProvider.Idle;
        _currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        ParseInput();
        _currentState.UpdateState();
    }

    public void SwitchState(AbstractCharacterState newState) {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    private void ParseInput() {
        IsCoverPressed = Input.GetKeyDown(KeyCode.C);
    }             
}
