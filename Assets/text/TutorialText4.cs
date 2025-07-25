using UnityEngine;

public class TutorialText4 : MonoBehaviour
{
    public TextBoxController textBox;

    private bool TextFlag = false;

    void OnTriggerStay2D(Collider2D other)
    {
    
            if (GameManagement.Instance != null)
            {
                if (other.CompareTag("Player") && GameManagement.Instance.tutorialLeveUp && !TextFlag && !TextBoxController.Instance.IsShowing)
                {
                    TextFlag = true;
                    string[] lines = {
                    "カードがありそれぞれ効果が違う",
                    "カードの中身は毎回違うので自分の合うカードを選ぼう",
                    "左はプレイヤー関連",
                    "右は銃関連",
                    "だけどもっている武器によって変わるから注意がいる",
                    "真ん中はプレイヤーと銃が含まれてる"
                    };
                    
                    textBox.ShowMessages(lines);

                }
            }
        
    }
}
