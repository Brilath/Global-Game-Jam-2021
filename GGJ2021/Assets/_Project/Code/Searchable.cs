using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour
{
    [SerializeField] private bool _hasValue;
    public bool HasValue { get { return _hasValue; } private set { _hasValue = value; } }

    [SerializeField] private int _valueAmount;
    public int ValueAmount { get { return _valueAmount; } private set { _valueAmount = value; } }
}
