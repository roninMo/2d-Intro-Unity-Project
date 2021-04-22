//using UnityEngine;
//using UnityEngine.Events;

//public class PlayerController : MonoBehaviour
//{
//    private PlayerInput pi;
//    private Rigidbody2D rb;
//    private BoxCollider2D boxCollider;
//    private bool m_FacingLeft = false;
//    private bool isTouchingGround = false;
//    private bool isTouchingWall = false;
//    private bool isWallSliding = false;
//    private float wallCheckDistance = 0.5f;

//    [Header("Player Values")]
//    [SerializeField] private float runSpeed = 540f;
//    [SerializeField] private float jumpForce = 34f;
//    [SerializeField] private float wallSlidingSpeed = 4f;
//    [SerializeField] private float wallJumpForce = 100f;
//    //[SerializeField] private float crouchSpeedDampener = .5f;

//    [Header("Player Stats")]
//    [SerializeField] private float hitPoints = 40f;
//    [SerializeField] private float baseDamage = 5f;

//    [Header("Utilities")]
//    [SerializeField] private Animator animator;
//    [SerializeField] private Transform wallCheck;
//    [SerializeField] private LayerMask platformLayerMask;


//    void Awake()
//    {
//        pi = GetComponent<PlayerInput>();
//        rb = GetComponent<Rigidbody2D>();
//        boxCollider = GetComponent<BoxCollider2D>();
//        hitPoints = 40f;
//        baseDamage = 5f;
//    }


//    void Update()
//    {
//        Jump();
//        WallJump();
//        Debug.Log("velociy" + rb.velocity);
//    }


//    void FixedUpdate()
//    {
//        MoveCharacter();
//        WallSliding();

//        // Have it so if the player comes in contact with a slime, if it's not from above then knock them back and take damage
//    }


//    ////////////////////  Functions  ////////////////////
//    private void MoveCharacter()
//    {
//        float runSpeedCalculation = runSpeed * pi.movementInput * Time.fixedDeltaTime;
//        if (isTouchingGround)
//        {
//            rb.velocity = new Vector2(runSpeedCalculation, rb.velocity.y);
//            //Platforming();
//        } else
//        {
//            rb.AddForce(new Vector2(runSpeedCalculation * 10, rb.velocity.y), ForceMode2D.Force);
//            Vector2 clampVelocity = rb.velocity;
//            clampVelocity.x = Mathf.Clamp(clampVelocity.x, -8.6f, 8.6f);
//            rb.velocity = clampVelocity;
//        }
//        animator.SetFloat("MoveSpeed", Mathf.Abs(pi.movementInput));
//        turnAround();
//    }


//    private void turnAround()
//    {
//        if (pi.movementInput > 0 && m_FacingLeft)
//        {
//            Flip();
//        }
//        else if (pi.movementInput < 0 && !m_FacingLeft)
//        {
//            Flip();
//        }
//    }


//    private void Flip()
//    {
//        m_FacingLeft = !m_FacingLeft;
//        Vector3 theScale = transform.localScale; // Multiply the player's x local scale by -1.
//        theScale.x *= -1;
//        transform.localScale = theScale;
//    }


//    private void WallSliding()
//    {
//        CheckIfTouchingWall();
//        CheckIfWallSliding();

//        if (isWallSliding)
//        {
//            animator.SetBool("WallSliding", true);
//            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
//        }
//        else
//        {
//            animator.SetBool("WallSliding", false);
//        }
//    }


//    private void CheckIfTouchingWall()
//    {
//        if (m_FacingLeft)
//        {
//            isTouchingWall = Physics2D.Raycast(wallCheck.position, -transform.right, wallCheckDistance, platformLayerMask);
//        }
//        else
//        {
//            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, platformLayerMask);
//        }
//    }


//    private void CheckIfWallSliding()
//    {
//        if (isTouchingWall && !isTouchingGround && rb.velocity.y < 0)
//        {
//            isWallSliding = true;
//        }
//        else
//        {
//            isWallSliding = false;
//        }
//    }


//    private void WallJump()
//    { 
//        if (isWallSliding && pi.jump) // If they're wall sliding and jump, then wall jump
//        {
//            animator.SetBool("WallSliding", false);
//            float wjForceCalculation = wallJumpForce * Time.fixedDeltaTime;
//            rb.velocity = new Vector2(rb.velocity.x, 0);
//            rb.velocity = new Vector2(wjForceCalculation * -pi.movementInput, wjForceCalculation);
//        }
//    }


//    private void Jump()
//    {
//        // Set the y velocity for the animator to determine which jump animation to use:
//        isTouchingGround = IsTouchingGround();
//        animator.SetFloat("VerticalVelocity", rb.velocity.y);
//        if (isTouchingGround)
//        {
//            // Set the animator to no longer jumping, and check if the player inputs a jump
//            animator.SetBool("IsJumping", false);

//            if (pi.jump)
//            {
//            rb.velocity = new Vector2(rb.velocity.x, 0);
//            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//            // Think about recreating this to everytime they Jump while on the ground, create a timer, and for however long they Hold the button,
//            // push them up a static amount, and when they let go, reverse the velocity and ignore the gravity, keep it static. If they hold for the full duration,
//            // Stop pushing them up a static amount and let gravity drag them back down (This part will be interesting)
//            }
//        }
//        else
//        {
//            // Set the animator to the isJumping state
//            animator.SetBool("IsJumping", true);
//        }
//    }


//    private bool IsTouchingGround()
//    {
//        int numberOfGroundCollisions = boxCollider.Cast(Vector2.down, new RaycastHit2D[10], 0.04f, true);
//        return numberOfGroundCollisions != 0 ? true : false;
//    }


//    private void Platforming() // If player is moving up, ignore collisions between player and platforms
//    {
//        // All elegantly in one script baby
//        //if (rb.velocity.y > 0)
//        //{
//        //    Physics2D.IgnoreLayerCollision(8, 10, true);
//        //}
//        //else // the collision will not be ignored
//        //{
//        //    Physics2D.IgnoreLayerCollision(8, 10, false);
//        //}
//        // Hollow knight doesn't use platforms, and quite frankly I don't wanna either. Platform effectors are wack.
//        // Learn how collision detection works from one component to another layermask based on direction
//        // This is a solid way to do very basic platforming, at it's finest.
//        // I think we should just create a layer, and have it so that when they click crouch while walking on the platform, whilein contact with it keep it off
//        // Then reset it when they've phased through it. Just use a layer mask or whatever

//        // Also manually handle heading up a platform somehow. If you stop heading up 
//    }


//    //private void OnDrawGizmos()
//    //{
//    //    // To draw the wallcheck raycast
//    //    if (m_FacingLeft)
//    //    {
//    //        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
//    //    }
//    //    else
//    //    {
//    //        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
//    //    }
//    //}
//}
