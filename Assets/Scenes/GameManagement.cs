using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public Transform startGetTarget;
    private int PlayerHP = 0;
    private bool tutorialBoss = false;
    private bool stage1Boss = false;
    private bool stage2Boss = false;

    private AudioSource bgmSource;

    [SerializeField]
    private Vector3 playerSpawnTutorial = Vector3.zero;
    [SerializeField]
    private Vector3 playerSpawnStage1= Vector3.zero;
    [SerializeField]
    private Vector3 playerSpawnStage2= Vector3.zero;

    public bool isPause = false;

    public GameObject levelUpPanel;
    public bool isLevelUp = false;
    void Start()
    {
        
        PlayerHP = StetusScript.Instance.PlayerHp;

        bgmSource = GetComponent<AudioSource>();

        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
        if (PlayerPrefs.HasKey("StartLoad") && PlayerPrefs.GetInt("StartLoad") == 1)
        {
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(true);
            }
            TutorialStepController.Instance.ProgressToNextStep();
            TutorialStepController.Instance.ProgressToNextStep();
            
            PlayerMove.Instance.transform.position = playerSpawnStage1;
            PlayerPrefs.DeleteKey("StartLoad");
        }
        
    }

    
    void Update()
    {
       
        if (StetusScript.Instance != null && StetusScript.Instance.PlayerHp <= 0)
        {
            bgmSource.Stop();
            SceneManager.LoadScene("GameOver");
        }
        if (ShootEnemy.Instance != null && ShootEnemy.Instance.currentHP <= 0 && !tutorialBoss)
        {
            PlayerMove.Instance.transform.position = playerSpawnStage1;

            StetusScript.Instance.Save();
            tutorialBoss = true;
            ShootEnemy.Instance.Die();
            EnemySpawn.Instance.DestroyAllEnemies();
            TutorialStepController.Instance.ProgressToNextStep();
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(true);
            }

        }
        if (nWayBullet.Instance != null && nWayBullet.Instance.currentHP <= 0 && !stage1Boss)
        {
            PlayerMove.Instance.transform.position = playerSpawnStage2;

            StetusScript.Instance.Save();
            stage1Boss = true;
            nWayBullet.Instance.Die();
            EnemySpawn.Instance.DestroyAllEnemies();
            TutorialStepController.Instance.ProgressToNextStep();
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(true);
            }

        }
        if (DivisionEnemy.Instance != null && DivisionEnemy.Instance.currentHP <= 0 && !stage2Boss)
        {
            stage2Boss = true;
            DivisionEnemy.Instance.Die();
            EnemySpawn.Instance.DestroyAllEnemies();

            bgmSource.Stop();
            SceneManager.LoadScene("GameClear");
        }

        if (isLevelUp)
        {
            levelUpPanel.SetActive(true);
        }
        else
        {
            levelUpPanel.SetActive(false);
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
