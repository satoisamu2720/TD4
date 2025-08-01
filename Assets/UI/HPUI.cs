using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HPUI : MonoBehaviour
{
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> hearts = new List<Image>();
    private int lastMaxHeartCount = 0;
    private int lastHP = -1;

    void Update()
    {
        if (StetusScript.Instance == null) return;

        int currentHP = StetusScript.Instance.PlayerHp;
        int maxHP = Mathf.Max(currentHP, StetusScript.Instance.PlayerHp); 
        int heartCountNeeded = Mathf.CeilToInt(maxHP / 2f);

        
        if (heartCountNeeded != lastMaxHeartCount)
        {
            AdjustHeartCount(heartCountNeeded);
            lastMaxHeartCount = heartCountNeeded;
        }

        
        if (currentHP == lastHP) return;
        lastHP = currentHP;

        UpdateHeartSprites(currentHP);
    }

    void AdjustHeartCount(int requiredCount)
    {
        
        while (hearts.Count > requiredCount)
        {
            Destroy(hearts[hearts.Count - 1].gameObject);
            hearts.RemoveAt(hearts.Count - 1);
        }

        
        while (hearts.Count < requiredCount)
        {
            Image newHeart = CreateHeartImage();
            hearts.Add(newHeart);
        }
    }

    void UpdateHeartSprites(int currentHP)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            int hpLeft = currentHP - (i * 2);

            if (hpLeft >= 2)
                hearts[i].sprite = fullHeart;
            else if (hpLeft == 1)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    private Image CreateHeartImage()
    {
        GameObject heartGO = new GameObject("Heart", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        heartGO.transform.SetParent(heartContainer, false);

        Image image = heartGO.GetComponent<Image>();
        image.sprite = emptyHeart;
        image.SetNativeSize();

        return image;
    }
}
