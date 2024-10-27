using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State<PlayerState>
{
    private Rigidbody _rigid;
    public JumpState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
    }

    public override void Enter()
    {
        _rigid = _player.GetComponent<Rigidbody>();

        Debug.Log("나 들어감 ㅋ");

        Jump();
    }

    public override void Update()
    {
        if(!_player._inputReader._isJump && _player.GetComponentInChildren<GroundChecker>()._isGround == true)
            _player.stateMachine.ChangeState(PlayerState.Idle);
    }

    public override void Exit()
    {
        
    }

    private void Jump()
    {
        _rigid.AddForce(Vector3.up * _player._playerStat.jumpSpeed, ForceMode.Impulse);
    }

}
