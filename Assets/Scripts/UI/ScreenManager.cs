using UnityEngine;


public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

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

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            SetCursorLockState(CursorLockMode.Locked);
            SetCursorVisibility(false);
        }
        else
        {
            SetCursorLockState(CursorLockMode.None);
            SetCursorVisibility(true);
        }
    }

    // Cursor settings
    public void SetCursorLockState(CursorLockMode lockMode)
    {
        if (Cursor.lockState == lockMode)
            return;
        Cursor.lockState = lockMode;
    }

    public void SetCursorVisibility(bool visible)
    {
        if (Cursor.visible == visible)
            return;
        Cursor.visible = visible;
    }
}
