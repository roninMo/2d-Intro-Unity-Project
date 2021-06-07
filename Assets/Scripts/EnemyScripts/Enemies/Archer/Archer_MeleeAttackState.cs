using UnityEngine;

public class Archer_MeleeAttackState : MeleeAttackState
{
    private Archer enemy;

    public Archer_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, Transform attackPosition, D_MeleeAttack stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, attackPosition, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange) // Player Detected State
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (!isPlayerInMinAgroRange) // Look For Player State
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}
