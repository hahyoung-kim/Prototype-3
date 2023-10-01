using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_enemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosi;
    private GameObject player;
    public float attack_range;

    [SerializeField] private Animator anim;

    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < attack_range)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                anim.SetTrigger("RangedAttack");
            }
        }

        
    }

    public void shoot()
    {
        Instantiate(bullet, bulletPosi.position, Quaternion.identity);
    }

}