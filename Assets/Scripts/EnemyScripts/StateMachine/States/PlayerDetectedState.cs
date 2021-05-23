using UnityEngine;

public class PlayerDetectedState : EnemyState
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performCloseRangeAction;
    protected bool performLongRangeAction;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_PlayerDetected stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        entity.SetVelocity(0f);
        DoChecks();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
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
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

}
