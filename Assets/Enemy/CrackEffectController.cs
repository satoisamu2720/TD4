using System.Collections;
using UnityEngine;

public class CrackEffectController : MonoBehaviour
{
    public SpriteRenderer crackRenderer;
    public ParticleSystem dustParticle;
    public ParticleSystem rockParticle;

    public void PlayCrackEffect()
    {
        

        StartCoroutine(FadeInCrack());

        Debug.Log("CrackEffect Played"); 

        if (dustParticle) dustParticle.Play();
        if (rockParticle) rockParticle.Play();
    }

    IEnumerator FadeInCrack()
    {
        float duration = 0.5f; // フェード時間（秒）
        float time = 0f;

        Color startColor = crackRenderer.color;
        startColor.a = 0f;
        crackRenderer.color = startColor;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            Color newColor = crackRenderer.color;
            newColor.a = alpha;
            crackRenderer.color = newColor;
            yield return null;
        }

        // 念のため完全表示に
        Color finalColor = crackRenderer.color;
        finalColor.a = 1f;
        crackRenderer.color = finalColor;
    }

}
