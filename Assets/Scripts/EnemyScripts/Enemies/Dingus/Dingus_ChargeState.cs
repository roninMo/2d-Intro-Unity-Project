using UnityEngine;

public class Dingus_ChargeState : ChargeState
{
    private Dingus enemy;

    public Dingus_ChargeState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Charge stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        else  if (!isDetectingLedge || isDetectingWall) // Look For Player State
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange) // Player Detected State
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else // If we're not detecting the player - Look For Player State
            {
                stateMachine.ChangeState(enemy.lookForPlayerState); 
            }
        }
    }
}
