using System;
using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Shoot,
    Melee
}


namespace Enemy
{
    [RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class EnemyBehavior : MonoBehaviour
    {
        [Header("Patrol Behavior")] [SerializeField]
        private Transform leftEdge;

        [SerializeField] private Transform rightEdge;
        [SerializeField] private float fPatrolSpeed;
        private int _moveDir;


        [Header("Attack Range")] [SerializeField]
        private float fNotificationRange;

        [SerializeField] private float fAttackRange;

        [Header("Player Perception")] [SerializeField]
        private LayerMask lPlayerLayerMask;


        private Animator _animator;
        private Collider2D _coll;
        private Transform _selfTransform;
        private bool _bPause;
        private Vector3 _playerPosition;

        public EnemyState State
        {
            get => _state;
            set
            {
                switch (value)
                {
                    case EnemyState.Idle:
                        _animator.SetBool("walk", false);
                        break;
                    case EnemyState.Melee:
                        _animator.Play("Attack");
                        break;
                    case EnemyState.Patrol:
                        _animator.SetBool("walk", true);
                        break;
                    case EnemyState.Shoot:
                        break;
                    case EnemyState.Chase:
                        _animator.SetBool("walk", true);
                        break;
                }

                _state = value;
            }
        }

        private EnemyState _state;


        private void Awake()
        {
            _selfTransform = transform;
            _animator = transform.GetComponent<Animator>();
            _coll = transform.GetComponent<Collider2D>();
            _moveDir = Math.Sign(_selfTransform.localScale.x);
        }


        private void Start()
        {
            State = EnemyState.Patrol;
            StartCoroutine(PerceivePlayer());
        }


        private void FixedUpdate()
        {
            if (!_bPause)
            {
                Movement();
            }
        }


        private void Movement()
        {
            if (State == EnemyState.Patrol)
            {
                PatrolMovement();
            }
            else if (State == EnemyState.Chase)
            {
                ChaseMovement();
            }
        }


        #region PatrolFunction

        private void PatrolMovement()
        {
            // turn around condition
            if (_selfTransform.position.x >= rightEdge.position.x)
            {
                _moveDir = -1;
                Vector3 scale = _selfTransform.localScale;
                scale.x = _moveDir * Math.Abs(scale.x);
                _selfTransform.localScale = scale;
                StartCoroutine(IdleTime(2f));
            }
            else if (_selfTransform.position.x <= leftEdge.position.x)
            {
                _moveDir = 1;
                Vector3 scale = _selfTransform.localScale;
                scale.x = _moveDir * Math.Abs(scale.x);
                _selfTransform.localScale = scale;
                StartCoroutine(IdleTime(2f));
            }


            // basic movement
            var position = _selfTransform.position;
            position.x += Time.deltaTime * _moveDir * fPatrolSpeed;
            _selfTransform.position = position;
        }

        private void ChaseMovement()
        {
            if (Math.Abs(_playerPosition.x - _selfTransform.position.x) < 0.3)
            {
                return;
            }

            _moveDir = Math.Sign(_playerPosition.x - _selfTransform.position.x);
            Vector3 scale = _selfTransform.localScale;
            scale.x = _moveDir * Math.Abs(scale.x);
            _selfTransform.localScale = scale;

            if (_selfTransform.position.x > leftEdge.position.x && _selfTransform.position.x < rightEdge.position.x)
            {
                var position = _selfTransform.position;
                position.x += Time.deltaTime * _moveDir * fPatrolSpeed;
                _selfTransform.position = position;
            }
        }

        #endregion


        #region Perceive

        IEnumerator PerceivePlayer()
        {
            while (gameObject.activeSelf)
            {
                OnPerceivePlayer();
                yield return new WaitForSeconds(1f);
            }
        }

        private void OnPerceivePlayer()
        {
            var position = transform.position;
            Collider2D[] playerNotification =
                Physics2D.OverlapCircleAll(position, fNotificationRange, lPlayerLayerMask);
            Collider2D[] playerAttack =
                Physics2D.OverlapCircleAll(position, fAttackRange, lPlayerLayerMask);
            if (playerAttack.Length == 0)
            {
                if (playerNotification.Length != 0)
                {
                    _playerPosition = playerNotification[0].transform.position;
                    State = EnemyState.Chase;
                }
                else
                {
                    State = EnemyState.Patrol;
                }
            }
            else
            {
                State = EnemyState.Melee;
            }
        }


        IEnumerator IdleTime(float time)
        {
            State = EnemyState.Idle;
            yield return new WaitForSeconds(time);
            State = EnemyState.Patrol;
        }

        #endregion


        public void OnHurt()
        {
            _animator.Play("Hurt");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, fNotificationRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fAttackRange);
        }
    }
}