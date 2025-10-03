using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f; // 1秒でフェードする

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeOut()
    {
        Color color = fadeImage.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }
}
