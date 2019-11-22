using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState;
    private IState previousState;

    public void ChangeState(IState newState)
    {
        if (newState == null)
            return;

        if (currentState != null && newState.GetType() == currentState.GetType())
            return;

        if (currentState != null)
            currentState.Exit();

        previousState = currentState;
        currentState = newState;

        currentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        if (currentState != null)
            currentState.Execute();
    }

    public void SwitchToPreviousState()
    {
        ChangeState(previousState);
    }

}
