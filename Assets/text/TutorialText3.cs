using UnityEngine;

public class TutorialText3 : MonoBehaviour
{
    public TextBoxController textBox;
    private bool TextFlag = true;

    private bool isWeaponEquipped = false;
    private string equippedWeaponID = "";

    private void OnEnable()
    {
        Weapon.OnWeaponPickedUp += OnWeaponPickedUpHandler;
    }

    private void OnDisable()
    {
        Weapon.OnWeaponPickedUp -= OnWeaponPickedUpHandler;
    }

    private void OnWeaponPickedUpHandler(string weaponID)
    {
        isWeaponEquipped = true;
        equippedWeaponID = weaponID;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (ExpItem.Instance != null)
        {
            if (other.CompareTag("Player") && isWeaponEquipped && TextFlag)
            {
                TextFlag = false;

                string[] lines;

                switch (equippedWeaponID)
                {
                    case "handgun":
                        lines = new string[]
                        {
                        "経験値を拾うとレベルアップし",
                        "自身を強化することができる",
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
    }
}

           