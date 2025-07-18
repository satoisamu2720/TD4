using System.Collections;
using UnityEngine;
using TMPro;

public class TextBoxController : MonoBehaviour
{
    public TextMeshProUGUI messageText; // 表示するメッセージテキスト
    public GameObject textBoxPanel;     // テキストボックス背景
    public GameObject nextIcon;         // 次へ進むアイコン（▼など）
    public float textSpeed = 0.05f;     // 文字送り速度

    private string[] messages;          // 表示するメッセージ配列
    private int currentMessageIndex;    // 現在表示中のインデックス
    private bool isTyping;              // タイピング中かどうか
    private bool canProceed;            // 次に進めるか
    void Start()
    {
        var followUI = UIFollowWorldObject.GetInstance();
        if (followUI != null)
        {
            followUI.ShowUI(false);
        }
    }

    // 会話スタート
    public void ShowMessages(string[] lines)
    {
        GameManagement.Instance.isPause  = true; // 会話開始
        messages = lines;
        currentMessageIndex = 0;
        textBoxPanel.SetActive(true);
        StartCoroutine(TypeText(messages[currentMessageIndex]));
    }

    void Update()
    {
        if (!textBoxPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.F)|| Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // スキップして全文表示
                StopAllCoroutines();
                messageText.text = messages[currentMessageIndex];
                isTyping = false;
                nextIcon.SetActive(true);
                canProceed = true;
            }
            else if (canProceed)
            {
                NextMessage();
            }
        }
    }

    void NextMessage()
    {
        nextIcon.SetActive(false);
        currentMessageIndex++;

        if (currentMessageIndex < messages.Length)
        {
            StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
        else
        {
            // 最後まで表示し終えたら非表示に
            HideTextBox();
        }
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        canProceed = false;
        messageText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        nextIcon.SetActive(true);
        canProceed = true;
    }

    // テキストボックスを非表示にする処理
    void HideTextBox()
    {
        textBoxPanel.SetActive(false);
        messageText.text = "";
        nextIcon.SetActive(false);
        messages = null;
        currentMessageIndex = 0;
        isTyping = false;
        canProceed = false;
        GameManagement.Instance.isPause  = false; // 会話終了
        var followUI = UIFollowWorldObject.GetInstance();
        if (followUI != null)
        {
            followUI.ShowUI(true);
        }
    }
}
