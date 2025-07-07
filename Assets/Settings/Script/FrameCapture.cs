using UnityEngine;
using System.IO;

public class FrameCapture : MonoBehaviour
{
    [Header("フレーム設定")]
    [SerializeField]
    private int frameRate = 30;
    // 何フレームまでキャプチャするか
    [SerializeField]
    private int maxCaptureFrames = 100;
    // 保存先フォルダ名
    [SerializeField]
    private string folderName = "CaptureFrames"; // 保存先フォルダ名

    private int frameCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 固定フレームレート
        Time.captureFramerate = frameRate;

        // 保存フォルダ作成
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount >= maxCaptureFrames)
        {
            Debug.Log("[FrameCapture] 終了");
            enabled = false;
            return;
        }

        // ファイル名（例：image_0001.png）
        string filename = Path.Combine(folderName, $"image_{frameCount:D4}.png");

        // スクリーンショット保存
        ScreenCapture.CaptureScreenshot(filename);

        Debug.Log($"[FrameCapture] Saved: {filename}");

        frameCount++;

    }
}
