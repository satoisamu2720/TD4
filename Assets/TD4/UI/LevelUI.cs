using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private int lastLevel = -1;

    // Update is called once per frame
    void Update()
    {
        int currentLevel = StetusScript.Instance.level;

        if(currentLevel != lastLevel)
        {
            levelText.text = "Lv: " + StetusScript.Instance.level.ToString();
            lastLevel = currentLevel;
        }
    }
}
