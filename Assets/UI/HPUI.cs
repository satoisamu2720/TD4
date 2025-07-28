using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HPUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab; // ハートのプレハブ
    [SerializeField] private Transform heartContainer; // ハートを並べる親オブジェクト
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> hearts = new List<Image>();

    void Start()
    {
        int maxHP = StetusScript.Instance.PlayerHp;

        // ハートをプレハブから生成
        for (int i = 0; i < maxHP / 2; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, heartContainer);
            Image heartImage = heartObj.GetComponent<Image>();
            hearts.Add(heartImage);
        }
    }

    void Update()
    {
        int currentHP = StetusScript.Instance.PlayerHp;

        for (int i = 0; i < hearts.Count; i++)
        {
            int hpLeft = currentHP - (i * 2);

            if (hpLeft >= 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (hpLeft == 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
