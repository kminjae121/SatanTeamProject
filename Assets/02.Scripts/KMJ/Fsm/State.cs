using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> where T : Enum
{
    protected Player _player;
    protected StateMachine<T> _stateMachine;
    public State(Player player, StateMachine<T> state)
    {
        _player = player;
        _stateMachine = state;
    }

    public virtual void Enter()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void LateUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
