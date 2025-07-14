using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    private int PlayerHP = 0;
    private bool oneBoss = false;

    private AudioSource bgmSource;

    [SerializeField]
    private Vector3 playerSpawnTutorial = Vector3.zero;
    [SerializeField]
    private Vector3 playerSpawnStage1= Vector3.zero;
    [SerializeField]
    private Vector3 playerSpawnStage2= Vector3.zero;

    public bool isPause = false;


    void Start()
    {
        PlayerHP = StetusScript.Instance.PlayerHp;

        bgmSource = GetComponent<AudioSource>();

        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    
    void Update()
    {
       
        if (StetusScript.Instance != null && StetusScript.Instance.PlayerHp <= 0)
        {
            bgmSource.Stop();
            SceneManager.LoadScene("GameOver");
        }
        if (ShootEnemy.Instance != null && ShootEnemy.Instance.currentHP <= 0 && !oneBoss)
        {
            PlayerMove.Instance.transform.position = playerSpawnStage1;

            StetusScript.Instance.Save();
            oneBoss = true;
            ShootEnemy.Instance.Die();
            EnemySpawn.Instance.DestroyAllEnemies();
            TutorialStepController.Instance.ProgressToNextStep();
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(true);
            }

        }
    }

    public void SetBgmVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = Mathf.Clamp01(volume);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}
