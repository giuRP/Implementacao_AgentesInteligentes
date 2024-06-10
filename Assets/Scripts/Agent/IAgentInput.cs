using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentInput
{
    Vector3 MovementDirection { get; }

    event Action<Vector3> OnMovement;
}
