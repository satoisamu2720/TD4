using UnityEngine;

public class TutorialText3 : MonoBehaviour
{
    public TextBoxController textBox;
    private bool TextFlag = true;

    private bool isWeaponEquipped = false;

    private void OnEnable()
    {
        Weapon.OnWeaponPickedUp += OnWeaponPickedUpHandler;
    }

    private void OnDisable()
    {
        Weapon.OnWeaponPickedUp -= OnWeaponPickedUpHandler;
    }

    private void OnWeaponPickedUpHandler()
    {
        isWeaponEquipped = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isWeaponEquipped && TextFlag)
        {
            TextFlag = false;
            string[] lines = {
                "倒すと経験値が落ちる",
                "経験値を拾うとレベルアップし",
                "自身を強化することができる",
                "敵を倒し切らなくてもボスとは戦える"
            };
            textBox.ShowMessages(lines);
        }
    }
}
