using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware; // ForceSoftware�ɕύX

    void Start()
    {
        Vector2 center = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, center, CursorMode.ForceSoftware);
    }
}
