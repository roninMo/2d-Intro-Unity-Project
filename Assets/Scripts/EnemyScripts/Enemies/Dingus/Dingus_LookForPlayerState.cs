using UnityEngine;

public class Dingus_LookForPlayerState : LookForPlayerState
{
    private Dingus enemy;

    public Dingus_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_LookForPlayer stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        else if (isAllTurnsTimeDone) // Move State
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
