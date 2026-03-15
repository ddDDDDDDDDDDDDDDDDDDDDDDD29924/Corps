using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BootstrapManager : MonoBehaviour
{
    // Защита от повторной инициализации, если Bootstrap загрузится повторно
    private static bool _initialized;

    private void Awake()
    {
        if (_initialized)
        {
            Destroy(gameObject);
            return;
        }

        _initialized = true;
        DontDestroyOnLoad(gameObject);

        // Создаём/поднимаем основные менеджеры
        CreateGameManager();
        CreateSceneLoader();
        CreateEventBus();
        CreateInputManager();
        CreateScreenManager();

        // Переходим в главное меню
        SceneLoader.Instance.Load(SceneNames.Menu);
    }

    private static void CreateGameManager()
    {
        GameManager existing = FindFirstObjectByType<GameManager>();
        if (existing != null)
        {
            DontDestroyOnLoad(existing.gameObject);
            return;
        }

        GameObject go = new GameObject("GameManager");
        go.AddComponent<GameManager>();
        DontDestroyOnLoad(go);
    }

    private static void CreateSceneLoader()
    {
        SceneLoader existing = FindFirstObjectByType<SceneLoader>();
        if (existing != null)
        {
            DontDestroyOnLoad(existing.gameObject);
            return;
        }

        GameObject go = new GameObject("SceneLoader");
        go.AddComponent<SceneLoader>();
        DontDestroyOnLoad(go);
    }

    private static void CreateEventBus()
    {
        EventBus existing = FindFirstObjectByType<EventBus>();
        if (existing != null)
        {
            DontDestroyOnLoad(existing.gameObject);
            return;
        }

        GameObject go = new GameObject("EventBus");
        go.AddComponent<EventBus>();
        DontDestroyOnLoad(go);
    }

    private void CreateInputManager()
    {
        InputManager existing = FindFirstObjectByType<InputManager>();
        if (existing != null)
        {
            DontDestroyOnLoad(existing.gameObject);
            return;
        }

        GameObject go = new GameObject("InputManager");
        InputManager inputManager = go.AddComponent<InputManager>();

        inputManager.inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");

        if (inputManager.inputActions == null)
        {
            Debug.LogError("Failed to load InputActionAsset from Resources/InputSystem_Actions");
        }

        DontDestroyOnLoad(go);
    }

    private void CreateScreenManager()
    {
        ScreenManager existing = FindFirstObjectByType<ScreenManager>();
        if (existing != null)
        {
            DontDestroyOnLoad(existing.gameObject);
            return;
        }

        GameObject go = new GameObject("ScreenManager");
        ScreenManager screenManager = go.AddComponent<ScreenManager>();

        DontDestroyOnLoad(go);
    }
}