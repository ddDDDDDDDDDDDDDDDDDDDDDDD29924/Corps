using UnityEngine;
using System;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public event Action OnGamePaused;
    public event Action OnGameResumed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void RaiseGamePaused()
    {
        OnGamePaused?.Invoke();
        Debug.Log("EventBus: Game Paused event raised.");
    }
    public void RaiseGameResumed()
    {
        OnGameResumed?.Invoke();
        Debug.Log("EventBus: Game Paused event raised.");
    }
}

public class PauseUI : MonoBehaviour
{
    public GameManager gameManager;

    void OnButtonClick()
    {
        gameManager.Pause();
    }
}
