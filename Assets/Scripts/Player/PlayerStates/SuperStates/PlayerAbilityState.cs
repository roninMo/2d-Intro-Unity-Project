using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isTouchingGround;
    protected Vector2 input;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandler.RawMovementInput;


        // State logic
        if (isAbilityDone)
        {
            if(isTouchingGround) // Ground States
            {
                if (input.x < 0.01f && input.x > -0.01f) // Idle State
                {
                    StateMachine.ChangeState(player.IdleState);
                }
                else // Move State
                {
                    StateMachine.ChangeState(player.MoveState);
                }
            }
            else // Air State
            {
                StateMachine.ChangeState(player.InAirState);
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingGround = Core.CollisionSenses.Ground(player.BoxCollider);
    }
}
