using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public TextBoxController textBox;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyUp(KeyCode.Space))
        {
            string[] lines = {
                "こんにちは、旅人さん。",
                "この村には不思議な噂があるんだ。",
                "気をつけていくんだよ。",                
            };
            textBox.ShowMessages(lines);
        }
    }
}
