using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth = 20;
    public float currentHealth = 20;
    private bool dead;

    [Header("IFrames")] [SerializeField] private float IframeDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriteRender;

    [Header("Audio")] [SerializeField] public AudioSource hurt;
    [SerializeField] public AudioSource Death;

    private void Awake()
    {
        currentHealth = startingHealth;
        //dead = false;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startingHealth);

        if (currentHealth > 0)
        {
            hurt.Play();
            HealthBar.Instance.OnHpChange();
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                PlayerDead();
            }
        }
    }

    private void PlayerDead()
    {
        Death.Play();
        LevelManager.instance.GameOver();
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        for (int i = 0; i < numberofFlashes; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IframeDuration / (numberofFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(IframeDuration / (numberofFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 9, false);
    }

    public void reborn()
    {
        currentHealth = startingHealth;
    }
}