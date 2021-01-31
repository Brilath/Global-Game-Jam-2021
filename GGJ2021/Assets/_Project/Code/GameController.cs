using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action OnGameStarted = delegate { };
    public static Action OnGameEnded = delegate { };


    private void Start()
    {
        OnGameStarted?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        OnGameEnded?.Invoke();
    }
}
