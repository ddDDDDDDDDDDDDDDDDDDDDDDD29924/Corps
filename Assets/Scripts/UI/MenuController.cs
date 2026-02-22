using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button buttonPlayGame;
    [SerializeField] private Button buttonQuitGame;

    private void Start()
    {
        buttonPlayGame.onClick.AddListener(() => GameManager.Instance.StartGame());
        buttonQuitGame.onClick.AddListener(() => Application.Quit());
    }
}
