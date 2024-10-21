using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State<PlayerState>
{
    public JumpState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
    }

}
