using UnityEngine;

public class Archer_MoveState : MoveState
{
    private Archer enemy;

    public Archer_MoveState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Move stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isPlayerInMinAgroRange) // Player Detected State
        {
            stateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge) // Idle State
        {
            enemy.IdleState.SetFlipAfterIdle(true); 
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
