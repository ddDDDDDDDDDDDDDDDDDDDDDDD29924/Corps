using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    Paused,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Текущее состояние игры (меню / игра / пауза).
    /// </summary>
    public GameState CurrentGameState { get; private set; } = GameState.Menu;

    /// <summary>
    /// Инициализация Singleton и закрепление объекта между сценами.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Запускает игру из меню: переключает состояние, сбрасывает время, загружает игровую сцену и включает ввод игрока.
    /// </summary>
    public void StartGame()
    {
        CurrentGameState = GameState.Playing;
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadWithLoading(SceneNames.GameScene);
        Debug.Log("Game started");
        if (InputManager.Instance != null)
            InputManager.Instance.EnablePlayerInput();
    }

    /// <summary>
    /// Возврат в главное меню: переключает состояние, сбрасывает скорость времени, загружает сцену меню и включает UI‑ввод.
    /// </summary>
    public void GoToMenu()
    {
        CurrentGameState = GameState.Menu;
        Time.timeScale = 1f;
        SceneLoader.Instance.Load(SceneNames.Menu);
        Debug.Log("Go to Main Menu");
        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();
    }

    /// <summary>
    /// Ставит игру на паузу из состояния Playing:
    /// останавливает время через Time.timeScale и оповещает слушателей через EventBus.
    /// </summary>
    public void Pause()
    {
        if (CurrentGameState != GameState.Playing)
            return;

        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f; // простой вариант паузы
        EventBus.Instance.RaiseGamePaused();
        Debug.Log("Game paused");
    }

    /// <summary>
    /// Снимает паузу из состояния Paused:
    /// возвращает Time.timeScale к 1 и оповещает слушателей через EventBus.
    /// </summary>
    public void Resume()
    {
        if (CurrentGameState != GameState.Paused)
            return;

        CurrentGameState = GameState.Playing;
        Time.timeScale = 1f;
        EventBus.Instance.RaiseGameResumed();
        Debug.Log("Game resumed");
    }
}