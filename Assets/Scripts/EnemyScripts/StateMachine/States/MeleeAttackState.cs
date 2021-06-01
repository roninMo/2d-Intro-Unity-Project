using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack stateData;

    protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, currentAnimation, attackPosition)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGameObject.transform.position;
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
    }


    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
