﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _metalDecetorHead;
    [SerializeField] private LayerMask _searchableLayerMask;
    [SerializeField] private int _collectionValue;

    private Vector2 _input;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        _input.Normalize();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Metal Decetor gogogogo");
            MetalDecetorSearch();
        }
    }

    private void FixedUpdate()
    {
        _body.velocity = _input * _moveSpeed * Time.fixedDeltaTime;
    }

    // Discover the searchable item
    // Check if it has value
    // Collect it
    private void MetalDecetorSearch()
    {
        Collider2D[] searchables = Physics2D.OverlapCircleAll(_metalDecetorHead.transform.position, 0.5f, _searchableLayerMask);

        Debug.Log($"Number of searchables found {searchables.Length}");

        foreach (Collider2D searchable in searchables)
        {
            Searchable s = searchable.GetComponent<Searchable>();
            bool hasValue = s.HasValue;
            if(hasValue)
            {
                _collectionValue += s.ValueAmount;
                _collectionValue = Mathf.Max(_collectionValue, 0);  
            }
        }
    }
}
