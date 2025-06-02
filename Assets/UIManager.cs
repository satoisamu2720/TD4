using TMPro;
using UnityEngine;

public class UIManag : MonoBehaviour
{

    public TextMeshProUGUI uiText;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
        uiText.text = "HP " + playerMove.HP;, 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
