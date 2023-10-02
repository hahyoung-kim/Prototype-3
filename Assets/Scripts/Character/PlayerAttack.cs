using Enemy;
using UnityEngine;

namespace Character
{
    public class PlayerAttack : MonoBehaviour
    {
        public Transform fMeleeHitPos;
        public float fMeleeRange;
        public LayerMask lEnemyLayerMask;
        [SerializeField] public AudioSource rogue;

        public void OnPerformMeleeAttack()
        {
            rogue.Play();
            Collider2D[] detectiveEnemy =
                Physics2D.OverlapCircleAll(fMeleeHitPos.position, fMeleeRange, lEnemyLayerMask);
            foreach (var enemyCollider in detectiveEnemy)
            {
                //enemyCollider.GetComponent<EnemyBehavior>().OnHurt();
                enemyCollider.GetComponent<enemy_health>().TakeDamage(3);
            }
        }


        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(fMeleeHitPos.position, fMeleeRange);
        }
    }
}