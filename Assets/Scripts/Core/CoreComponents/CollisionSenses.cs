using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float ceilingCheckDistance;
    [SerializeField] LayerMask whatIsGround;


    public bool Ground(BoxCollider2D boxCollider)
    {
        int numberOfGroundCollisions = boxCollider.Cast(Vector2.down, new RaycastHit2D[10], groundCheckRadius, true);
        return numberOfGroundCollisions != 0 ? true : false;
        // The other way to do this with layermasks (Though cast is awesome, it creates a rectangle not a circle), is this:
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); // groundChekc is the transform of a gameobject attached to the player
    }

    public bool Ceiling(BoxCollider2D boxCollider)
    {
        int numberOfCeilingCollisions = boxCollider.Cast(Vector2.up, new RaycastHit2D[10], ceilingCheckDistance, true);
        return numberOfCeilingCollisions != 0 ? true : false;
        // The other way to do this with layermasks (Though cast is awesome, it creates a rectangle not a circle), is this:
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); // groundChekc is the transform of a gameobject attached to the player
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
}










// Deprecated ledge climb mechanics
//public Vector2 DetermineCornerPosition()
//{
//    // This function takes our wall and ledge check positions, and creates a raycast in the y direction between them to calculate where the ledge is exactly
//    // So - The wallCheck finds the distance from the player to the side of the wall, then stores it
//    // The raycast we create goes out that distance from from the ledgeCheck, and then goes down to find the distance to the top of the ledge and stores it
//    // this is how it determines the corner position and where our player should climb up to

//    RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right, playerData.wallCheckDistance, playerData.whatIsGround);
//    float xDistance = xHit.distance;
//    workspace.Set(xDistance * Core.Movement.FacingDirection, 0f); // the distance the player is from the wall
//    RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
//    float yDistance = yHit.distance;

//    workspace.Set(wallCheck.position.x + (xDistance * Core.Movement.FacingDirection), ledgeCheck.position.y - yDistance);
//    return workspace;
//}
