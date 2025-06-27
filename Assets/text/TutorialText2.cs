using UnityEngine;

public class TutorialText2 : MonoBehaviour
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
                "ハンドガンを拾った",
                "カーソルで狙って左クリックで打てる",
                "これでゾンビを倒すことができる",
                "倒すと経験値が落ちて自身を強化できる",
                "万全だと思ったら矢印の方へいこう",
            };
            textBox.ShowMessages(lines);
        }
    }
}
