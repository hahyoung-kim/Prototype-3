using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private float maxHp;
    private float _currentHp;

    private void Start()
    {
        _currentHp = maxHp;
    }

    private void Update()
    {
        Vector3 hpPos = hpBar.transform.position;
        hpPos.x = transform.position.x;
        hpBar.transform.parent.position = hpPos;
    }


    public void TakeDamage(float dmg)
    {
        _currentHp -= dmg;

        if (_currentHp > 0)
        {
            Vector3 scale = hpBar.transform.localScale;
            scale.x = _currentHp / maxHp;
            hpBar.transform.localScale = scale;
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}