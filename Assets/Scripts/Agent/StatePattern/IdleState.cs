using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected override void EnterState()
    {
        agent.rb.velocity = Vector3.zero;
    }

    protected override void HandleMovement(Vector3 input)
    {
        if(Mathf.Abs(input.x) > 0 || Mathf.Abs(input.z) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.movement));
        }
    }
}
