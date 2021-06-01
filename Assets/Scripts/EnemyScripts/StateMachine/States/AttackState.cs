using UnityEngine;

public class AttackState : EnemyState
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, Transform attackPosition) : base(entity, stateMachine, currentAnimation)
    {
        this.attackPosition = attackPosition;
    }


    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        entity.atsm.attackState = this;
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


    public virtual void TriggerAttack() {}
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
