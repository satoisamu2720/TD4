using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public Transform startGetTarget;
    private int PlayerHP = 0;
    private bool tutorialBoss = false;
    private bool stage1Boss = false;
    private bool stage2Boss = false;

    public float bossTime = 3f;

    private bool oneBoss = false;
    private AudioSource bgmSource;

    [SerializeField] private Vector3 playerSpawnTutorial = Vector3.zero;
    [SerializeField] private Vector3 playerSpawnStage1 = Vector3.zero;
    [SerializeField] private Vector3 playerSpawnStage2 = Vector3.zero;

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

        if (ShootEnemy.Instance != null)
        {
            if (ShootEnemy.Instance.currentHP <= 0 && !tutorialBoss)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
                if (player != null)
                {
                    MainCameraScript.Instance.SetDefaultTarget(player.transform);
                    MainCameraScript.Instance.FocusOn(bossEnemy.transform, bossTime); // 3秒間ボスにフォーカス
                }
                tutorialBoss = true;
            }
            if (!GameManagement.Instance.isPause && tutorialBoss)
            {
                ShootEnemy.Instance.Die();
                PlayerMove.Instance.transform.position = playerSpawnStage1;
                StetusScript.Instance.Save();

                EnemySpawn.Instance.DestroyAllEnemies();
            }
        }

        if (nWayBullet.Instance != null)
        {
            if (nWayBullet.Instance.currentHP <= 0 && !stage1Boss)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
                if (player != null)
                {
                    MainCameraScript.Instance.SetDefaultTarget(player.transform);
                    MainCameraScript.Instance.FocusOn(bossEnemy.transform, bossTime); // 3秒間ボスにフォーカス
                }
                stage1Boss = true;
            }
            if (!GameManagement.Instance.isPause && stage1Boss)
            {
                nWayBullet.Instance.Die();
                StetusScript.Instance.Save();
                PlayerMove.Instance.transform.position = playerSpawnStage2;
                EnemySpawn.Instance.DestroyAllEnemies();                    
            }
        }
        if (DivisionEnemy.Instance != null) 
        { 
            if (DivisionEnemy.Instance.currentHP <= 0 && !stage2Boss)
            { 
        
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
                if (player != null)
                {
                    MainCameraScript.Instance.SetDefaultTarget(player.transform);
                    MainCameraScript.Instance.FocusOn(bossEnemy.transform, bossTime); // 3秒間ボスにフォーカス
                }
                stage2Boss = true;
            }
            if (!GameManagement.Instance.isPause && stage2Boss)
            {
                DivisionEnemy.Instance.Die();
                EnemySpawn.Instance.DestroyAllEnemies();

                bgmSource.Stop();
                SceneManager.LoadScene("GameClear");
            }
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
    private IEnumerator TimeDie()
    {
        
        yield return new WaitForSeconds(3f);

    }
}
