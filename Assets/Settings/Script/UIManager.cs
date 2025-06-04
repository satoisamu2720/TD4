using TMPro;
using UnityEngine;

public class UIManag : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
        //hpText.text = "HP : " + playerMove.HP;
    }
}
