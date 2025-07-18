using UnityEngine;

public class HandGunText : MonoBehaviour
{
    public TextBoxController textBox;
    public string targetWeaponID; 

    private bool textShown = false;

    private void OnEnable()
    {
        Weapon.OnWeaponPickedUp += OnWeaponPickedUpHandler;
    }

    private void OnDisable()
    {
        Weapon.OnWeaponPickedUp -= OnWeaponPickedUpHandler;
    }

    private void OnWeaponPickedUpHandler(string pickedID)
    {
        if (textShown || pickedID != targetWeaponID) return;

        textShown = true;

        string[] lines;

        switch (pickedID)
        {
            case "handgun":
                lines = new string[]
                {
                        "ハンドガンを拾った",
                        "カーソルで狙って左クリックで撃てる",
                        "これでゾンビを倒すことができる",
                        "矢印の方へいこう",
                };
                TutorialStepController.Instance.ProgressToNextStep();
                break;

            case "ar":
                lines = new string[]
                {
                        "アサルトライフルを拾った！",
                        "長押しで連射できるぞ！",
                        "Qキーで武器の切り替えができる",
                };
                break;

            case "sg":
                lines = new string[]
                {
                        "ショットガンを拾った！",
                        "近距離でまとめて吹き飛ばそう！",
                };
                break;

            default:
                lines = new string[]
                {
                        "武器を拾った！",
                        "使い方は画面を見ながら試してみよう！"
                };
                break;
        }

        textBox.ShowMessages(lines);
        
    }
}
