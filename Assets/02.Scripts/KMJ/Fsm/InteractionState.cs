using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : State<PlayerState>
{
    public InteractionState(Player player, StateMachine<PlayerState> state) : base(player, state)
    {
    }

}
