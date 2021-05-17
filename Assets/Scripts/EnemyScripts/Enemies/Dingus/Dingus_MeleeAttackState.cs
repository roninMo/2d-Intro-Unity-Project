using UnityEngine;

public class Dingus_MeleeAttackState : MeleeAttackState
{
    private Dingus enemy;

    public Dingus_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, Transform attackPosition, D_MeleeAttack stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, attackPosition, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange) // Player Detected State
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else // Look For Player State
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }


    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
