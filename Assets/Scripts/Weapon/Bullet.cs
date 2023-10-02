using System;
using Enemy;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private Collider2D _coll;
        private Rigidbody2D _rb;

        public float fSpeed = 30f;


        // set the direction of the bullet
        public void SetDirection(int dir)
        {
            fSpeed *= dir;
            Vector3 scale = transform.localScale;
            scale.x = dir * Math.Abs(scale.x);
            transform.localScale = scale;
        }

        private void Update()
        {
            transform.Translate(fSpeed * Time.deltaTime, 0, 0, Space.World);
            Destroy(gameObject, 1.5f);
        }
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Debug.Log(other.gameObject.name);
            
            // platform
            if (other.gameObject.layer == 3)
            {
                Destroy(gameObject);
            }

            // enemy
            
            if (other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<enemy_health>().TakeDamage(1);
                //other.gameObject.GetComponent<EnemyBehavior>().OnHurt();
                Destroy(gameObject);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "enemy")
            {
                collision.GetComponent<enemy_health>().TakeDamage(1);
                //other.gameObject.GetComponent<EnemyBehavior>().OnHurt();
                Destroy(gameObject);
            }
        }
        
        private void OnEnable()
        {
            _coll = transform.GetComponent<Collider2D>();
            _rb = transform.GetComponent<Rigidbody2D>();
        }
    }
}