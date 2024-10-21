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
        
    }

    public override void Update()
    {
        if (_player._playerStat.moveDir.x != 0 || _player._playerStat.moveDir.z != 0)
            _stateMachine.ChangeState(PlayerState.Walk);
    }
}
