using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        //dead = false;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player take damage
            Debug.Log("take damage");
        } else {
            if (!dead)
            {
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                Debug.Log("dead!");
            }
            Debug.Log("dead already");
        }
    }
}
