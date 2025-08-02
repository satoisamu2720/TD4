using UnityEngine;

public class TutorialText1 : MonoBehaviour
{
    public TextBoxController textBox;
    private bool TextFlag = true;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& TextFlag)
        {
            TextFlag = false;

            string[] lines = {
                "Fキーで話や物を調べられる",
                "WSADで移動ができて",
                "Spaceでダッシュができる",
                "矢印の方向にいってみて",
                "幸運を祈ってる",
            };
            GameManagement.Instance.StartTutorial = true;
            textBox.ShowMessages(lines);
            
        }
    }
}
