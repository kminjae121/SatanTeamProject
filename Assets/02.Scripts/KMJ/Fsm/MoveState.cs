using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State<PlayerState>, IMove
{
    private Rigidbody _rigid;

    public MoveState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
        
    }

    public override void Enter()
    { 
        _player.isMoving = true;
        _rigid = _player.GetComponent<Rigidbody>();
    }
    public override void Update()
    { 
        if (_player._playerStat.moveDir.x == 0 && _player._playerStat.moveDir.z == 0)
            _stateMachine.ChangeState(PlayerState.Idle);

        if (_player._inputReader._isJump &&_player.GetComponentInChildren<GroundChecker>()._isGround == true)
            _stateMachine.ChangeState(PlayerState.Jump);
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

    public override void Exit()
    {
        _player.isMoving = false;
    }

    public void Move()
    {
        _rigid.velocity = new Vector3(_player.transform.TransformDirection(_player._playerStat.moveDir.normalized).x * _player._playerStat.moveSpeed,
            _rigid.velocity.y,
            _player.transform.TransformDirection(_player._playerStat.moveDir.normalized).z * _player._playerStat.moveSpeed);
    }
}
