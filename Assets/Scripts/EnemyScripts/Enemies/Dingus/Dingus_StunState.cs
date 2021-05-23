using UnityEngine;

public class Dingus_StunState : StunState
{
    private Dingus enemy;

    public Dingus_StunState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Stun stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isStunTimeOver)
        {
            if (performCloseRangeAction) // Melee Attack State
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange) // Charge State
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
