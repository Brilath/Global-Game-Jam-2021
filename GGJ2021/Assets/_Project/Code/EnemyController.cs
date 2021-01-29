using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _waitTime;
    [SerializeField] private bool _isWaiting;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform[] _patrolPath;
    [SerializeField] private int _currentPatrolIndex;

    private void Start()
    {
        StartCoroutine(GetNextPatrolPosition());
    }

    private void Update()
    {
        if (_body == null) return;
        if (_isWaiting) return;

        _body.position = Vector2.MoveTowards(_body.position, _patrolPath[_currentPatrolIndex].position, _moveSpeed * Time.deltaTime);
        if(Vector3.Distance(_body.position, _patrolPath[_currentPatrolIndex].position) <= 0.1f)
        {
            StartCoroutine(GetNextPatrolPosition());
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
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
    }
}
