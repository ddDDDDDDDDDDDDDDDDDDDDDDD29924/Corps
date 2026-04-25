using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI loadingText;
    [SerializeField] private TMPro.TextMeshProUGUI percentageText;

    private void Start()
    {
        StartCoroutine(LoadingTextAnimation());
    }

    private IEnumerator LoadingTextAnimation()
    {
        while (true)
        {
            loadingText.text = "Loading";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
