using UnityEngine;

namespace Character
{
    public class PlayerAttack : MonoBehaviour
    {
        public Transform fMeleeHitPos;
        public float fMeleeRange;
        public LayerMask lEnemyLayerMask;

        public void OnPerformMeleeAttack()
        {
            Collider2D[] detectiveEnemy =
                Physics2D.OverlapCircleAll(fMeleeHitPos.position, fMeleeRange, lEnemyLayerMask);
            foreach (var enemyCollider in detectiveEnemy)
            {
                // Perform Attack Method
                Debug.Log(enemyCollider.gameObject.name);
            }
        }


        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(fMeleeHitPos.position, fMeleeRange);
        }
    }
}