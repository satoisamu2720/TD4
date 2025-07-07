using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StetusChange : MonoBehaviour
{

    [SerializeField] private string _LoadScene;

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerHp += 10;
            RsumeScene();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerSpeed -= 0.1f;
            RsumeScene();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.Bullet += 1;
            RsumeScene();
        }
    }

    public void OnAlpha1Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerHp += 2;
        RsumeScene();
    }

    public void OnAlpha2Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerSpeed -= 0.1f;
        RsumeScene();
    }

    public void OnAlpha3Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.Bullet += 1;
        RsumeScene();
    }

    public void RsumeScene()
    {
        //SceneTransitionManager.Instance.ReturnToStageScene();
        //Player.IsNotMove = false;

        StopManager.Instance.Resume();

    }


}
