using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;

    public UnityEvent OnEnter, OnExit;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    public void Enter()
    {
        this.agent.playerInput.OnMovement += HandleMovement;

        OnEnter?.Invoke();
        EnterState();
    }

    public void Exit()
    {
        this.agent.playerInput.OnMovement -= HandleMovement;

        OnExit?.Invoke();
        ExitState();
    }

    protected virtual void EnterState()
    {
    }

    protected virtual void ExitState()
    {
    }

    public virtual void StateUpdate()
    {
    }

    public virtual void StateFixedUpdate()
    {
    }

    protected virtual void HandleMovement(Vector3 input)
    {
    }
}
