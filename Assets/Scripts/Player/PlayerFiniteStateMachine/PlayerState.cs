using UnityEngine;

public class PlayerState
{
    protected Player player; // protected means private but shared between components that inherit the class
    protected PlayerStateMachine StateMachine;
    protected PlayerData playerData;
    private string currentAnimation;
    protected float StartTime; // Start time gets set everytime we're in a state, that way we have a reference for how long we've been in any state (good for mechanics)
    protected bool isAnimationFinished;
    protected bool isExitingState; // Very useful, if you run through if/else conditionals in a superState, the substate may still run, and both end up calling a change state. This will stop this from happening

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation)
    {
        this.player = player;
        this.StateMachine = stateMachine;
        this.playerData = playerData;
        this.currentAnimation = currentAnimation;
    }


        // So now we code out the functions for each state
    // Every state must have an enter and exit function, as well as an update and fixedUpdate function
    // We're naming the update function as "LogicUpdate", and fixedUpate as "PhysicsUpdate"

    public virtual void Enter() // virtual means this function may be overriden from classes that inherit this class
    {
        DoChecks();
        player.Anim.SetBool(currentAnimation, true);
        StartTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        Debug.Log("Current Animation: " + currentAnimation);
    }


    public virtual void Exit()
    {
        player.Anim.SetBool(currentAnimation, false);
        isExitingState = true;
    }


    public virtual void LogicUpdate()
    {

    }


    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }


    public virtual void DoChecks()
    {
        // DoCheck is a function we're going to call from physics update and enter. It will check for things like if we're touching the ground or look for walls,
        // things like that. That way we're not declaring them twice in every state
    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
