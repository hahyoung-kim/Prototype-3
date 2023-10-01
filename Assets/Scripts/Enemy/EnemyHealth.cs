using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(float dmg)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - dmg, 0, startingHealth);

        if (_currentHealth > 0)
        {
            //enemy take damage
            Debug.Log("hurt!");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("enemy is dead");
        }
    }
}