using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Rigidbody rb;
    public IAgentInput playerInput;
    public StateFactory stateFactory;
    public CameraController camController;

    public State currentState = null, previousState = null;

    [Header("State Debug")]
    [SerializeField]
    private string stateName = "";

    private bool isInitialized = false;
    public void InitializeAgent()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponentInParent<IAgentInput>();
        stateFactory = GetComponentInChildren<StateFactory>();
        camController = GetComponentInChildren<CameraController>();

        stateFactory.InitializeStates(this);
        TransitionToState(stateFactory.GetState(StateType.idle));

        GameManager.Instance.playerAgent = this;

        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
            return;

        camController.SetCameraOrientation();
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        if (!isInitialized)
            return;

        currentState.StateFixedUpdate();
    }

    public void TransitionToState(State goaltState)
    {
        if (goaltState == null || goaltState == currentState)
            return;

        if (currentState != null)
            currentState.Exit();

        previousState = currentState;
        currentState = goaltState;
        currentState.Enter();

        DisplayState();
    }

    private void DisplayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }
}
