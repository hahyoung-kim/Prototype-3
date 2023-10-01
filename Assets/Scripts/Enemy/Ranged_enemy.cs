using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_enemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header ("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTime = Mathf.Infinity;

    [Header ("Ranged Attack Parameters")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] bullets;

    private enemy_Patrol enemyPatrol;

    private void Awake()
    {
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
                // shoot the bullet
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack()
    {
        cooldownTime = 0;
        bullets[FindBullets()].transform.position = firepoint.position;
        //bullets[FindBullets()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


        return (hit.collider != null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
