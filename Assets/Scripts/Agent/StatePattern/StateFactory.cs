using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State idle, movement;

    public State GetState(StateType stateType) => stateType switch
    {
        StateType.idle => idle,
        StateType.movement => movement,
        _ => throw new System.Exception("State not defined" + stateType.ToString())
    };

    public void InitializeStates(Agent agent)
    {
        State[] states = GetComponents<State>();
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}

public enum StateType
{
    idle,
    movement
}
