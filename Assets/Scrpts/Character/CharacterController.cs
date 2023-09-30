using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterController : MonoBehaviour
{
    public float fJumpSpeed;
    public float fTargetSpeed = 8;
    public float fLinearDrag = 1;


    public Animator _eff_animator1;
    public Animator _eff_animator2;
    private bool _bOnGround;
    private Collider2D _coll;
    private Animator _animator;
    private Rigidbody2D _rb;

    void Awake()
    {
        _animator = transform.GetComponent<Animator>();
        _coll = transform.GetComponent<Collider2D>();
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            _animator.SetBool("walk", true);
        }
        else
        {
            _animator.SetBool("walk", false);
        }


        if (horizontalInput > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Math.Abs(scale.x);
            transform.localScale = scale;
        }

        if (horizontalInput < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1 * Math.Abs(scale.x);
            transform.localScale = scale;
        }

        _rb.AddForce(new Vector2(horizontalInput, 0), ForceMode2D.Impulse);
        if (Math.Abs(_rb.velocity.x) > fTargetSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * fTargetSpeed, _rb.velocity.y);
        }

        if (Mathf.Abs(horizontalInput) < 0.4f)
        {
            _rb.drag = fLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }
}