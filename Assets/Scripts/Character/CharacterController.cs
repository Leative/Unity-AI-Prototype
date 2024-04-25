using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform opponent;
        
    public Transform Transform{get => this.transform;}
    public Transform Opponent{get => opponent;}
    public NavMeshAgent Agent{get => _agent;}

    // Input variables
    public bool IsCoverTriggered{get; set;}

    private AbstractCharacterState _currentState;
    private CharacterStateProvider _stateProvider;
    private CharacterAnimationProvider _animProvider;
    private NavMeshAgent _agent;

    void Awake() {
        _animProvider = new CharacterAnimationProvider(animator);
        _stateProvider = new CharacterStateProvider(this, _animProvider);     
        _agent = GetComponent<NavMeshAgent>();   
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
        Debug.Log("Entering:" + _currentState.GetType().Name);
        _currentState.EnterState();
    }

    private void ParseInput() {
        IsCoverTriggered = Input.GetKeyDown(KeyCode.C);
    }        
}
