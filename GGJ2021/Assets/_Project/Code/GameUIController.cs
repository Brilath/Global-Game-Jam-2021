using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _collectedText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _endCollectedText;
    [SerializeField] private TMP_Text _endTimeText;

    [SerializeField] private bool _hasStarted;
    [SerializeField] private float _gameTimeLapsed;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _pausePanel;

    private void Awake()
    {
        PlayerController.OnCollected += HandleOnCollected;
        GameController.OnGameStarted += HandleGameStarted;
        GameController.OnGameEnded += HandleGameEnded;
    }

    private void OnDestroy()
    {
        PlayerController.OnCollected -= HandleOnCollected;
        GameController.OnGameStarted -= HandleGameStarted;
        GameController.OnGameStarted -= HandleGameEnded;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (!_hasStarted) return;

        _gameTimeLapsed += Time.deltaTime;
        _timeText.SetText(_gameTimeLapsed.ToString("F2"));
        _endTimeText.SetText(_gameTimeLapsed.ToString("F2"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!_pausePanel.activeSelf);
        }
    }

    private void HandleOnCollected(int amount)
    {
        _collectedText.SetText(amount.ToString());
        _endCollectedText.SetText(amount.ToString());
    }

    private void HandleGameStarted()
    {
        _hasStarted = true;
    }

    private void HandleGameEnded()
    {
        _hasStarted = false;
        _endPanel.SetActive(true);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame(bool state)
    {
        Time.timeScale = state ? 0 : 1;
        _pausePanel.SetActive(!_pausePanel.activeSelf);
    }
}
