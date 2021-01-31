using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _boxCollider;

    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runAwaySpeed;
    [SerializeField] private float _waitTime;
    [SerializeField] private bool _isWaiting;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform[] _patrolPath;
    [SerializeField] private Transform _runAwayPoint;
    [SerializeField] private Transform _currentMovePoint;
    [SerializeField] private int _currentPatrolIndex;

    [SerializeField] private Steal _stealer;
    [SerializeField] private int _stealAmount;
    public int StealAmount { get { return _stealAmount; } private set { _stealAmount = value; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stealer.OnSteal += HandleOnSteal;
    }

    private void OnDestroy()
    {
        _stealer.OnSteal -= HandleOnSteal;
    }

    private void Start()
    {
        StartCoroutine(GetNextPatrolPosition());
        _moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
    }

    private void Update()
    {
        if (_body == null) return;
        if (_isWaiting) return;

        _body.position = Vector2.MoveTowards(_body.position, _currentMovePoint.position, _moveSpeed * Time.deltaTime);

        if(Vector3.Distance(_body.position, _patrolPath[_currentPatrolIndex].position) <= 0.1f)
        {
            StartCoroutine(GetNextPatrolPosition());
        }
    }

    private void HandleOnSteal(PlayerController player)
    {
        Debug.Log("Trying to steal stuff");
        if (player.StealCollectables(StealAmount))
        {
            _boxCollider.enabled = false;
            _currentMovePoint = _runAwayPoint;
            _moveSpeed = _runAwaySpeed;
            Destroy(gameObject,1.5f);
        }
    }

    private IEnumerator GetNextPatrolPosition()
    {
        _isWaiting = true;
        if (_currentPatrolIndex + 1 < _patrolPath.Length)
        {
            _currentPatrolIndex++;
        }
        else
        {
            _currentPatrolIndex = 0;
        }
        _currentMovePoint = _patrolPath[_currentPatrolIndex];
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
    }
}
