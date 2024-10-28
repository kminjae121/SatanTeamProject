using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Idle,
    Jump,
    Interaction
}
public class Player : MonoBehaviour
{
    [field :SerializeField] public InputReader _inputReader { get;  set; }
    [field : SerializeField] public PlayerStat _playerStat { get; set; }


    public Action OnJump;

    public PlayerState playerState { get; private set; }

    public State<PlayerState> currentState { get; private set; }

    public StateMachine<PlayerState> stateMachine { get; private set; }

    public bool isMove { get; set; }

    private void Awake()
    {
        isMove = false;
        stateMachine = new StateMachine<PlayerState>();

        stateMachine.AddState(PlayerState.Walk, new MoveState(this, stateMachine));
        stateMachine.AddState(PlayerState.Idle, new IdleState(this, stateMachine));
        stateMachine.AddState(PlayerState.Jump, new JumpState(this, stateMachine));
        stateMachine.AddState(PlayerState.Interaction, new InteractionState(this, stateMachine));

        stateMachine.InitIntialize(PlayerState.Idle, this);
    }

    private void Update()
    {
        SetMove(_inputReader.InputVec);
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate(); 
    }

    public void SetMove(Vector3 input)
    {
        _playerStat.moveDir.x = input.x;
        _playerStat.moveDir.z = input.z;
    }

    private void OnDisable()
    {
       
    }

}
