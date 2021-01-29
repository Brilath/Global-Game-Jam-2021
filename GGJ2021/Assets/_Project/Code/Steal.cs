using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    [SerializeField] private int _stealAmount;
    public int StealAmount { get { return _stealAmount; } private set { _stealAmount = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        if (player.StealCollectables(StealAmount))
        {
            Destroy(gameObject);
        }
    }
}
