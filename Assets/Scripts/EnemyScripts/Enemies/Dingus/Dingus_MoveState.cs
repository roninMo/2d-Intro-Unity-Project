using UnityEngine;

public class Dingus_MoveState : MoveState
{
    private Dingus enemy;

    public Dingus_MoveState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_MoveState stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
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
        if (isDetectingWall || !isDetectingLedge) // Idle State
        {
            enemy.idleState.SetFlipAfterIdle(true);
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
