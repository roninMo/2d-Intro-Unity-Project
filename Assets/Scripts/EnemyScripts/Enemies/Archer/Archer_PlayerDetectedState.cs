using UnityEngine;

public class Archer_PlayerDetectedState : PlayerDetectedState
{
    private Archer enemy;

    public Archer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_PlayerDetected stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (performCloseRangeAction) // Melee Attack State
        {
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (!isPlayerInMaxAgroRange) // Look For Player State
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
    }
}
