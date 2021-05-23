using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer = 0.2f;
    private bool gotInput, isAttacking, isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity;
    private AttackDetails attackDetails;

    private Animator anim;
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private float attack1Radius = 0.8f;
    [SerializeField] private float attack1Damage = 10f;
    [SerializeField] private float stunDamageAmount = 1f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);   
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }


    private void CheckCombatInput()
    {
        // Grab the input if combat is enabled
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                // Attack
                gotInput = true;
                lastInputTime = Time.time;
            }
        }

        // Reset the attack input if it's past the duration that we save the input
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }


    private void CheckAttacks()
    {
        if (gotInput)
        {
            // Perform attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isAttacking = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
    }

    private void CheckAttackHitBox()
    {
        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);
            // Instantiate hit particle -- do this in the damage function forunique particles for each enemy 
        }
    }


    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
