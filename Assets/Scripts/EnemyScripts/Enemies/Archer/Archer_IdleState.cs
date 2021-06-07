using UnityEngine;

public class Archer_IdleState : IdleState
{
    private Archer enemy;

    public Archer_IdleState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Idle stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        else if (isIdleTimeOver) // Move State
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
}
