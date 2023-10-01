using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Patrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;

    [Header ("Move Parameters")]
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;
    [SerializeField] private float Idleduration;
    private float idelTimer;

    [Header ("Animation")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveDirection(-1);
            } else {
                ChangeDirection();
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveDirection(1);
            } else {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        idelTimer += Time.deltaTime;
        if (idelTimer > Idleduration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void MoveDirection(int direct)
    {
        anim.SetBool("moving", true);
        idelTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x)* direct, initialScale.y, initialScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direct * speed, enemy.position.y, enemy.position.z);
    }
}
