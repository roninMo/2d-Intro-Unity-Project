using UnityEngine;

public class IdleState : EnemyState
{
    protected D_Idle stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected float idleTime;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Idle stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }

    
    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        setSetRandomIdleTime();
    }


    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }


    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }


    private void setSetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
