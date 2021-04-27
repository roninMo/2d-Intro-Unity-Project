using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Vector2 input;
    private bool isGrounded;
    private bool isJumping;
    private bool isTouchingWall;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool jumpSaver;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        jumpSaver = true;
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        input = player.InputHandler.RawMovementInput;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        // When they press and hold jump, jumpInput is true, and junpInputStop is false
        // When they release jump, jumpInputStop is true

        // when they press jump we want to stop the function from being called again until they release the button (this has to be done by handling a variable in multiple states

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01)
        {
            StateMachine.ChangeState(player.LandState);
        }
        else if(jumpSaver && jumpInput && player.JumpState.CanJump())
        {
            StopCoyoteTime();
            StateMachine.ChangeState(player.JumpState);
        }
        else
        {
            player.CheckIfShouldFlip(input.x);
            player.SetAirVelocityX(playerData.airMovementVelocity * input.x);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        }
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
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
        isGrounded = player.CheckIfTouchingGround();
    }


    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time >= StartTime + playerData.coyoteTime)
        {
            Debug.Log("Coyote limit reached");
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void StopCoyoteTime() => coyoteTime = false;
    public void SetIsJumping() => isJumping = true;
}
