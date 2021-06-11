using UnityEngine;

public class EnemyState
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    public float startTime { get; protected set; }
    protected string currentAnimation;

    public EnemyState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.currentAnimation = currentAnimation;
    }


    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(currentAnimation, true);
        Debug.Log("Current Animation: " + currentAnimation);
    }


    public virtual void Exit()
    {
        entity.anim.SetBool(currentAnimation, false);
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
}
