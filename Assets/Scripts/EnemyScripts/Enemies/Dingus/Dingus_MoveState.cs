using UnityEngine;

public class Dingus_MoveState : MoveState
{
    private Dingus enemy;

    public Dingus_MoveState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Move stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isPlayerInMinAgroRange) // Player Detected State
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge) // Idle State
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
