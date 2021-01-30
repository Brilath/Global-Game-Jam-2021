using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _sadSounds;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform _metalDecetorHead;
    [SerializeField] private LayerMask _searchableLayerMask;
    [SerializeField] private float _searchDistance;

    [SerializeField] private int _collectionValue;
    [SerializeField] private string _collectableTag;

    private Vector2 _input;

    public static Action<int> OnCollected = delegate { };

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        _input.Normalize();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Dig in the sand!");
            Search();
        }
    }

    private void FixedUpdate()
    {
        _body.velocity = _input.normalized * _moveSpeed * Time.fixedDeltaTime;
    }

    private void Search()
    {
        Collider2D[] searchables = Physics2D.OverlapCircleAll(_metalDecetorHead.position, _searchDistance, _searchableLayerMask);

        foreach (Collider2D searchable in searchables)
        {
            Searchable s = searchable.GetComponent<Searchable>();
            if (s == null) return;
            s.Search();            
        }
    }

    public bool StealCollectables(int amount)
    {
        bool isHappy = _collectionValue >= amount ? true : false;

        _collectionValue -= amount;
        _collectionValue = Mathf.Max(_collectionValue, 0);
        OnCollected(_collectionValue);

        int rand = UnityRandom.Range(0, _sadSounds.Length);
        _audioSource.PlayOneShot(_sadSounds[rand]);

        return isHappy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable collectable = collision.GetComponent<Collectable>();
        if (collectable == null) return;

        _collectionValue += collectable.ValueAmount;
        _collectionValue = Mathf.Max(_collectionValue, 0);
        OnCollected(_collectionValue);
        collectable.Collect();
    }
}
