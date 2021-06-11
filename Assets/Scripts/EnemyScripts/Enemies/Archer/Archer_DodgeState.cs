using UnityEngine;

public class Archer_DodgeState : DodgeState
{
    private Archer enemy;

    public Archer_DodgeState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Dodge stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isDodgeOver)
        {
            if (isPlayerInMaxAgroRange) // Melee Attack State
            {
                if (performCloseRangeAction)
                {
                    stateMachine.ChangeState(enemy.MeleeAttackState);
                }
            }
            else // Look For Player State
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
            // TODO: ranged attack state
        }
    }
}
