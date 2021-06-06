using UnityEngine;

public class Dingus_PlayerDetectedState : PlayerDetectedState
{
    private Dingus enemy;

    public Dingus_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_PlayerDetected stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (performCloseRangeAction) // Melee Attack State
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction) // Charge State
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange) // Look For Player State
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        } 
        else if (isDetectingLedge) // Move State
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
