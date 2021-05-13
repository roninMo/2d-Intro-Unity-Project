using UnityEngine;

public class Dingus_PlayerDetectedState : PlayerDetectedState
{
    private Dingus enemy;

    public Dingus_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_PlayerDetected stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isPlayerInMaxAgroRange) // Idle State
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }
}
