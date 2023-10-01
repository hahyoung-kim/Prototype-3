using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_enemy_attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTime = Mathf.Infinity;

    //reference to player
    private Health playerHealth;
    //private Animator anim;
    private enemy_Patrol enemyPatrol;

    private void Awake()
    {
        //animation here
        //anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<enemy_Patrol>();
    }

    private void Update()
    {
        cooldownTime += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTime >= attackCooldown)
            {
                cooldownTime = 0;
                DamagePlayer();
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x, 
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return (hit.collider != null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range*transform.localScale.x, 
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
