using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State<PlayerState>
{
    private Rigidbody _rigid;

    public MoveState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
        
    }

    public override void Enter()
    {
        _player.isMove = true;
        _rigid = _player.GetComponent<Rigidbody>();
        Debug.Log("¿òÁ÷ÀÓ");
    }
    public override void Update()
    {
        if (_player._playerStat.moveDir.x == 0 && _player._playerStat.moveDir.z == 0)
            _stateMachine.ChangeState(PlayerState.Idle);
    }

    public override void PhysicsUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        _rigid.velocity = new Vector3(_player.transform.TransformDirection(_player._playerStat.moveDir).x * _player._playerStat.moveSpeed, 
            _rigid.velocity.y,
            _player.transform.TransformDirection(_player._playerStat.moveDir).z * _player._playerStat.moveSpeed);
    }

    public override void Exit()
    {
        _player.isMove = false;
    }

}
