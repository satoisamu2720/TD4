using UnityEngine;

public class TutorialText1 : MonoBehaviour
{
    public TextBoxController textBox;
    private bool TextFlag = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !TextFlag)
        {
            TextFlag = true;
            string[] lines = {
                "FƒL[‚Å˜b‚â•¨‚ğ’²‚×‚ç‚ê‚é",
                "WSAD‚ÅˆÚ“®‚Å‚«‚é",
                "–îˆó‚Ì•ûŒü‚É‚¢‚Á‚Ä‚İ‚Ä",
                "K‰^‚ğ‹F‚Á‚Ä‚é",
            };
            textBox.ShowMessages(lines);
        }
    }
}
