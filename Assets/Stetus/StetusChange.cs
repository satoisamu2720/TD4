using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StetusChange : MonoBehaviour
{

    [SerializeField] private string _LoadScene;
    int randomChoice = Random.Range(0, 3); // 0, 1, 2 のどれか
    int randomChoice2 = Random.Range(0, 3);
    int randomChoice3 = Random.Range(0, 3);

    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerHp += 10;
            ChangeScene();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerSpeed -= 0.1f;
            ChangeScene();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.Bullet += 1;
            ChangeScene();
        }
    }

    public void OnAlpha1Button()
    {
        StetusScript.Instance.level += 1;
        

        switch (randomChoice)
        {
            case 0:
                StetusScript.Instance.PlayerHp += 1;
                Debug.Log("HP UP1!");
                break;
            case 1:
                StetusScript.Instance.PlayerHp += 3;
                Debug.Log("CoolTime!");
                break;
            case 2:
                StetusScript.Instance.PlayerHp += 5;
                Debug.Log("Speed!");
                break;
        }

        ChangeScene();
    }

    public void OnAlpha2Button()
    {
        StetusScript.Instance.level += 1;
       

        switch(randomChoice2)
        {
            case 0:
                StetusScript.Instance.PlayerSpeed -= 0.1f;
                Debug.Log("Cool Time -0.1!");
                break;
            case 1:
                StetusScript.Instance.PlayerSpeed -= 0.2f;
                Debug.Log("Cool Time -0.2!");
                break;
            case 2:
                StetusScript.Instance.PlayerSpeed -= 0.3f;
                Debug.Log("Cool Time -0.3!");
                break;

        }


        
        ChangeScene();
    }

    public void OnAlpha3Button()
    {
        StetusScript.Instance.level += 1;
        

        switch (randomChoice3)
        {
            case 0:
                StetusScript.Instance.PlayerSpeed -= 0.1f;
                Debug.Log("リロード速度　UP");
                break;
            case 1:
                StetusScript.Instance.PlayerSpeed -= 0.2f;
                Debug.Log("敵の貫通する数　UP");
                break;
            case 2:
                StetusScript.Instance.PlayerSpeed -= 0.3f;
                Debug.Log("弾の大きさ　UP");
                break;

        }

        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneTransitionManager.Instance.ReturnToStageScene();
        Player.IsNotMove = false;
    }


}
