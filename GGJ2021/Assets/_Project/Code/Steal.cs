using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Steal : MonoBehaviour
{
    public Action<PlayerController> OnSteal = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        OnSteal?.Invoke(player);
    }
}
