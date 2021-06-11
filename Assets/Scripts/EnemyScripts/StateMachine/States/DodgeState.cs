using UnityEngine;

public class DodgeState : EnemyState
{
    private D_Dodge stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isTouchingGround;
    protected bool isDodgeOver;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Dodge stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isTouchingGround)
        {

            if (Time.time >= startTime + stateData.dodgeTime)
            {
                isDodgeOver = true;

                if (entity.rb.velocity.x != 0)
                {
                    entity.SetVelocity(0f);
                }
            }
        }

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isTouchingGround = entity.CheckIfTouchingGround();
    }
}
