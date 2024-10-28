using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : Enum
{
    public Dictionary<T, State<T>> stateDic = new Dictionary<T, State<T>>();

    public State<T> currentState { get; private set; }

    private Player _player;

    public void InitIntialize(T state, Player player)
    {
        _player = player;
        currentState = stateDic[state];
        currentState.Enter();
    }

    public void ChangeState(T changeState)
    {
        currentState.Exit();
        currentState = stateDic[changeState];
        currentState.Enter();
    }

    public void AddState(T stateEnum, State<T> state)
    {
        stateDic.Add(stateEnum, state);
    }
}
