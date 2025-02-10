using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateRunner<T> : MonoBehaviour where T : MonoBehaviour //allows us to pass down states to the state list
{
    [SerializeField]
    private List<State<T>> _states;
    private readonly Dictionary<Type, State<T>> _stateByType = new();
    public State<T> _activeState;

    protected virtual void Awake() //makes it so we dont create new states everytime we use them, converts list to dictionary
    {
        _states.ForEach(s => _stateByType.Add(s.GetType(), s));
        SetState(_states[0].GetType()); //set first state to be activestate

    }

    public void SetState(Type newStateType)
    {
        if (_activeState != null) //if active state already there, trigger state exit
        {
            _activeState.Exit();
        }

        _activeState = _stateByType[newStateType]; //set new state from dictionary
        _activeState.Init(parent: GetComponent<T>()); //initialize the new state
    }

    private void Update() //calls to capptured input, update, and change state if needed
    {
        _activeState.CaptureInput();
        _activeState.Update();
        _activeState.ChangeState();


    }

    private void FixedUpdate()
    {
        _activeState.FixedUpdate();
    }















}

