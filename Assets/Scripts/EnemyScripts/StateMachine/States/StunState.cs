using UnityEngine;

public class StunState : EnemyState
{
    private D_Stun stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Stun stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStopped = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }


    public override void Exit()
    {
        base.Exit();

        entity.ResetStunResistance();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && isMovementStopped && Time.time >= startTime + stateData.stunKnockbackTime)
        {
            isMovementStopped = true;
            entity.SetVelocity(0f);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckIfTouchingGround();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMaxAgroRange();
    }
}
