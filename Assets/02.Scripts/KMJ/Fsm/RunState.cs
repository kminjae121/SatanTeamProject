using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State<PlayerState>
{
    public RunState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
    }

    public override void Enter()
    {
       
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {
       
    }
}
