using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool isFacingLeft = true;

    [Header("Baddie Values")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpDistance = 10f;
    [SerializeField] private float hitPoints = 10f;
    [SerializeField] private float baseDamage = 5f;

    [Header("Utilities")]
    [SerializeField] private LayerMask playerContact;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        hitPoints = 10f;
        baseDamage = 5f;
    }


    void FixedUpdate()
    {
        // We want them to grab the players position, and use that to determine which direction to head, IF they're within a specific distance of the player
        // WHEN in distance, based on where the player is, turn the slime towards them
        // Then do the movemnt (pause then jump) towards the player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 contactPoint = contact.normal;

        //Debug.Log("\ncontact Normal" + contactPoint);
        //Debug.Log("Compare tage to player" + collision.gameObject.CompareTag("Player"));
        if (collision.gameObject.CompareTag("Player")) // If the slime comes in contact with the player...
        {
            if (contactPoint.y <= -0.2) // If it landed on enough of the top of the slime, then have the slime take damage
            {

            } 
            else // Have the player take damage from the slime
            {

            }
        }
    }
}
