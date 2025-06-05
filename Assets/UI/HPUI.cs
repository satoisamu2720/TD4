using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public TextMeshProUGUI HpText;

    private int lastHp = -1;

    // Update is called once per frame
    void Update()
    {
        int currentHP = StetusScript.Instance.PlayerHp;

        if (currentHP != lastHp)
        {
            HpText.text = "PlayerHP: " + StetusScript.Instance.PlayerHp;
            lastHp = currentHP;
        }
    }
}