using UnityEngine;

public class PlayerDetectedState : EnemyState
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_PlayerDetected stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();
        DoChecks();

        entity.SetVelocity(0f);
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxRange();
    }

}
