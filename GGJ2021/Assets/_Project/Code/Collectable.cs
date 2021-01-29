using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int _valueAmount;
    public int ValueAmount { get { return _valueAmount; } private set { _valueAmount = value; } }
}