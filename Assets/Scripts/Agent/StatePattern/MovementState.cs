using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField]
    private float speed = 1f, maxSpeed = 1f;

    [SerializeField]
    private float accel = 1f, deaccel = 1f;

    protected Vector3 flatDirection = Vector3.zero;
    protected Vector3 currentVelocity = Vector3.zero;

    public override void StateUpdate()
    {
        CalculateVelocity();

        if(speed < 0.05f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.idle));
        }
    }

    public override void StateFixedUpdate()
    {
        SetPlayerVelocity();
    }

    protected virtual void CalculateSpeed(Vector3 movementDir)
    {
        if(Mathf.Abs(movementDir.x) > 0 || Mathf.Abs(movementDir.z) > 0)
        {
            speed += accel * Time.deltaTime;
        }
        else
        {
            speed -= deaccel * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    protected virtual void CalculateDirection(Vector3 movementDir)
    {
        flatDirection = movementDir;
    }

    protected virtual Vector3 CalculateVelocity()
    {
        CalculateSpeed(agent.playerInput.MovementDirection);
        CalculateDirection(agent.playerInput.MovementDirection);

        currentVelocity = speed * new Vector3(flatDirection.x, agent.rb.velocity.y, flatDirection.z);

        return currentVelocity;
    }

    protected virtual void SetPlayerVelocity()
    {
        agent.rb.velocity = CalculateVelocity();
    }
}
