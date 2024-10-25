using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<PlayerState>
{
    public IdleState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
    }

    public override void Enter()
    {
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("가만히 있음");
    }

    public override void Update()
    {
        if (_player._playerStat.moveDir.x != 0 || _player._playerStat.moveDir.z != 0)
            _stateMachine.ChangeState(PlayerState.Walk);

        if (Input.GetKeyDown(KeyCode.Space))
            _stateMachine.ChangeState(PlayerState.Jump);
    }
}
