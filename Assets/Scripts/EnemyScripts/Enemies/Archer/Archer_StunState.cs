using UnityEngine;

public class Archer_StunState : StunState
{
    private Archer enemy;

    public Archer_StunState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Stun stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isStunTimeOver)
        {
            if (isPlayerInMinAgroRange) // Player Detected State
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else // Look For Player State
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}
