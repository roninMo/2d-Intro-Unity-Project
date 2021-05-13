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
        if(!isDetectingLedge || isDetectingWall) // Look For Player State
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange) // Player Detected State
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
        }
    }
}
