using System;
using System.Collections;
using UnityEngine;
using Weapon;


[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    public bool bUnLockAbility;
    [Header("Movement")] public float fJumpSpeed;
    public float fDashSpeed;
    public float fTargetSpeed = 8;
    public float fLinearDrag = 1;

    [Header("Weapon")] public GameObject wMelee;
    private Animator _meleeAnimator;
    private bool _bMelee; // enable melee attack (cool down bool variable)
    public GameObject wGun;
    public GameObject wBullet;
    public Transform wBulletPos;
    private bool _bGun;

    // Movement Component
    private bool _bOnGround;
    private Collider2D _coll;
    private Rigidbody2D _rb;

    // Animation Component
    private Animator _animator;
    private static readonly int Walk = Animator.StringToHash("walk");
    private static readonly int Ground = Animator.StringToHash("ground");

    // Audio Component
    [SerializeField] public AudioSource shooting_gun;
    [SerializeField] public AudioSource flash;


    private float _fDashMaxTime = 0.2f;
    private float _fDashTime;
    private bool _bDash;


    public static bool bMelee;
    public static bool bShoot;
    public static bool bDash;

    private void Awake()
    {
        _animator = transform.GetComponent<Animator>();
        _coll = transform.GetComponent<Collider2D>();
        _rb = transform.GetComponent<Rigidbody2D>();

        _meleeAnimator = wMelee.GetComponent<Animator>();
        wGun.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        if (bUnLockAbility)
        {
            bDash = true;
            bMelee = true;
            bShoot = true;
        }
    }

    // Update is called once per frame

    private void Update()
    {
        Jump();
        Movement();
        if (bDash) Dash();
        if (bMelee) Melee();
        if (bShoot) Shoot();
    }


    void Dash()
    {
        if (!_bDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                _bDash = true;
                flash.Play();
                _animator.Play("Dash");
                _rb.velocity = new Vector2(Math.Sign(transform.localScale.x) * fDashSpeed, 0);
                _fDashTime = 0f;
                _rb.gravityScale = 0;
            }
        }
        else
        {
            _fDashTime += Time.deltaTime;
            if (_fDashTime > _fDashMaxTime)
            {
                _bDash = false;
                _rb.gravityScale = 6;
            }
        }
    }

    void Melee()
    {
        if (Input.GetKeyDown(KeyCode.J) && _bOnGround && !_bMelee)
        {
            _animator.Play("MeleeAttack");
            _bMelee = true;
            StartCoroutine(PlayMeleeAnimation());
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.K) && !_bGun)
        {
            _animator.Play("ShootAttack");
            shooting_gun.Play();
            _bGun = true;
            StartCoroutine(PlayGunAnimation());
        }
    }

    IEnumerator PlayGunAnimation()
    {
        yield return new WaitForSeconds(0.12f);
        wGun.SetActive(true);
        GameObject bullet = Instantiate(wBullet);
        bullet.transform.position = wBulletPos.position;
        bullet.GetComponent<Bullet>().SetDirection(Math.Sign(transform.localScale.x));
        yield return new WaitForSeconds(0.04f);
        wGun.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _bGun = false;
    }

    // Delay melee animation playback time, in order to make the attack effect natural
    IEnumerator PlayMeleeAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        _meleeAnimator.Play("Attack");
        yield return new WaitForSeconds(0.2f);
        _bMelee = false;
    }


    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            _animator.SetBool(Walk, true);
        }
        else
        {
            _animator.SetBool(Walk, false);
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
        if (!_bDash)
        {
            if (Math.Abs(_rb.velocity.x) > fTargetSpeed)
            {
                _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * fTargetSpeed, _rb.velocity.y);
            }
        }

        if (Math.Abs(horizontalInput) >= 0 && Mathf.Abs(horizontalInput) < 0.4f)
        {
            _rb.drag = fLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) &&
            _bOnGround)
        {
            //_rb.AddForce(new Vector2(0, fJumpSpeed), ForceMode2D.Impulse);
            _rb.velocity = new Vector2(_rb.velocity.x, fJumpSpeed);
            _animator.Play("Jump");
            _bOnGround = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _bOnGround = true;
            _animator.SetBool(Ground, true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _bOnGround = false;
            _animator.SetBool(Ground, false);
        }
    }
}