using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool dashInputStop;
    private float lastDashTime;
    private bool isHolding;
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePosition;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();
        isHolding = true;
        dashDirection = Vector2.right * Core.Movement.FacingDirection;
        Time.timeScale = playerData.holdTimeScale;
        StartTime = Time.unscaledTime; // This timer won't be affected in slow motion

        //player.DashDirectionIndicator.gameObject.SetActive(true); ///// Removing dash slowdown time and direction indicator /////
    } 


    public override void Exit()
    {
        base.Exit();

        if (Core.Movement.CurrentVelocity.y > 0)
        {
            Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.Anim.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));

            if (isHolding) // While choosing a dash direction
            {
                //dashDirectionInput = player.InputHandler.NormalizedDashDirectionInput;
                dashDirectionInput = player.InputHandler.RawDashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;

                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                // If the player touches the ground while in slow mo, stop him from sliding
                if (isTouchingGround)
                {
                    Core.Movement.SetVelocityToZero();
                }

                //if(dashInputStop || Time.unscaledTime >= StartTime + playerData.maxHoldTime) // the dash code ///// Removing dash slowdown time and direction indicator /////
                if (1 == 1)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    Core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.Rb.drag = playerData.drag;
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    Core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                    PlaceAfterImage();
                }
            }
            else // While dashing
            {
                Core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= StartTime + playerData.dashTime)
                {
                    player.Rb.drag = 0;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
            
        }
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }


    private void CheckIfShouldPlaceAfterImage()
    { 
        if(Vector2.Distance(player.transform.position, lastAfterImagePosition) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }


    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePosition = player.transform.position;
    }


    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }


    public void ResetCanDash() => CanDash = true;
}
