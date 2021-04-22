using UnityEngine;

// All the player state machine is a variable that holds a reference to our current state, and a function to initialize and change what our current state is
public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; } // Any other script that has a reference to this may grab/read the value, but only this script set's the state

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
