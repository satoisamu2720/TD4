using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture;       // Inspector‚ÅŠ„‚è“–‚Ä
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Vector2 center = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, center, CursorMode.Auto);
    }
}
