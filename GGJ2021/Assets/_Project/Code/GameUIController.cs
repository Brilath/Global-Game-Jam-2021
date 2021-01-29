using System;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _collectedText;


    private void Awake()
    {
        PlayerController.OnCollected += HandleOnCollected;
    }

    private void OnDestroy()
    {
        PlayerController.OnCollected -= HandleOnCollected;
    }

    private void HandleOnCollected(int amount)
    {
        _collectedText.SetText(amount.ToString());
    }
}
