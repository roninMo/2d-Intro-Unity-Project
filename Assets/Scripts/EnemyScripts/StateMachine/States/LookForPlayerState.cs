using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected D_LookForPlayer stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected bool turnImmediately;
    protected float lastTurnTime;
    protected int amountOfTurnsDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_LookForPlayer stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        entity.SetVelocity(0f);
        DoChecks();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnImmediately) // Immediate turn functionality
        {
            TurnLogic();
            turnImmediately = false;
        }
        else if (!isAllTurnsDone && Time.time >= lastTurnTime + RandomWaitTime()) // Random consecutive turns
        {
            TurnLogic();
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        // Time after waiting time
        if (Time.time >= lastTurnTime + RandomWaitTime())
        {
            isAllTurnsTimeDone = true;
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }


    #region Functions
    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }

    private void TurnLogic()
    {
        entity.Flip();
        lastTurnTime = Time.time;
        amountOfTurnsDone++;
    }

    private float RandomWaitTime()
    {
        return Random.Range(stateData.minTimeBetweenTurns, stateData.maxTimeBetweenTurns);
    }

    #endregion
}
