using UnityEngine;

public class SGText : MonoBehaviour
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
        if (other.CompareTag("Player") && isWeaponEquipped && TextFlag)
        {
            TextFlag = false;

            string[] lines;

            switch (equippedWeaponID)
            {
                
                case "sg":
                    lines = new string[]
                    {
                        "ショットガンを拾った！",
                        "近距離で強力だ、まとめて吹き飛ばそう！",
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
