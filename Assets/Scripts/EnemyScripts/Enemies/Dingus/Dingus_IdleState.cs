using UnityEngine;

public class Dingus_IdleState : IdleState
{
    private Dingus enemy;

    public Dingus_IdleState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Idle stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        else if (isIdleTimeOver) // Move State
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
