using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; } // We add the getters and setters so our states have access to the animator


    private void Awake() => StateMachine = new PlayerStateMachine();


    private void Start()
    {
        // TODO: Initialize the state machine upon starting the gameObject
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Instead of running the logic for each and every mechanic, we run the LogicUpdate Function, and the state machine handle the individual mechanic within this call
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
