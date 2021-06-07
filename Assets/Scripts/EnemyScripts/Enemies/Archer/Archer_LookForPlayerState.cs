using UnityEngine;

public class Archer_LookForPlayerState : LookForPlayerState
{
    private Archer enemy;

    public Archer_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_LookForPlayer stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        else if (isAllTurnsTimeDone) // Move State
        {
            stateMachine.ChangeState(enemy.MoveState); 
        }
    }
}
