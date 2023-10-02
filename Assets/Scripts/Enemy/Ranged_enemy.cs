using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_enemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosi;
    private GameObject _player;
    public float attack_range;

    [SerializeField] private Animator anim;
    private float _timer;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if(distance < attack_range)
        {
            _timer += Time.deltaTime;

            if (_timer > 2)
            {
                _timer = 0;
                anim.SetTrigger("RangedAttack");
            }
        }

        
    }

    public void shoot()
    {
        Instantiate(bullet, bulletPosi.position, Quaternion.identity);
    }

}