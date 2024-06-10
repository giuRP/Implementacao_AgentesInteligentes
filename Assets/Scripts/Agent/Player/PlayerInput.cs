using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IAgentInput
{
    [SerializeField]
    public Vector3 MovementDirection { get; private set; }

    public event Action<Vector3> OnMovement;

    private void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        MovementDirection = GetMovementDirection();
        OnMovement?.Invoke(MovementDirection);
    }

    protected Vector3 GetMovementDirection()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
