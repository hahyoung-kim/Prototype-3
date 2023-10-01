using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startingHealth);

        if (currentHealth > 0)
        {
            //enemy take damage
            Debug.Log("hurt!");
        } else {
            Destroy(gameObject);
            Debug.Log("enemy is dead");
        }
    }
}
