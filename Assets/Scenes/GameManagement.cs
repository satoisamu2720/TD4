using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public Transform startGetTarget;
    private int PlayerHP = 0;
    public bool tutorialBoss = false;
    public bool stage1Boss = false;
    public bool stage2Boss = false;

    public float bossTime = 3f;

    private AudioSource bgmSource;

    [SerializeField] private Vector3 playerSpawnTutorial = Vector3.zero;
    [SerializeField] private Vector3 playerSpawnStage1 = Vector3.zero;
    [SerializeField] private Vector3 playerSpawnStage2 = Vector3.zero;

    public bool isPause = false;

    public GameObject levelUpPanel;
    public bool isLevelUp = false;
    public bool levelUpRandom = true;
    public bool StartTutorialLeveUp = true;
    public bool tutorialLeveUp = false;
    public bool tutorialLeveUpPlate = false;


    private GameObject lastDeadEnemy;

    private int enemyKillCount = 0;
    public int totalEnemyCount = 3;
    void Start()
    {
        
        PlayerHP = StetusScript.Instance.PlayerHp;

        if (UIFollowWorldObject.Instance != null)
        {
            UIFollowWorldObject.Instance.ShowUI(false);
        }

        bgmSource = GetComponent<AudioSource>();
        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
        if (PlayerPrefs.HasKey("StartLoad") && PlayerPrefs.GetInt("StartLoad") == 1)
        {
            
            //TutorialStepController.Instance.ProgressToNextStep();
            StetusScript.Instance.EnemySpeed = 8;
            StetusScript.Instance.EnemyHp = 3;
            PlayerMove.Instance.transform.position = playerSpawnStage1;
            PlayerPrefs.DeleteKey("StartLoad");
            StartTutorialLeveUp = false;
        }
        else
        {

            StartTutorialLeveUp = true;
        }

        levelUpPanel.SetActive(true);
        levelUpPanel.SetActive(false);
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
                StartCoroutine(TutorialBossClear());
            }
        }

        if (nWayBullet.Instance != null)
        {
            if (stage1Boss)
            {               
                StartCoroutine(Stage1BossClear());
            }
            
        }
        if (!stage2Boss)
        {
            GameObject[] miniEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");

            bool allDead = true;
            foreach (GameObject enemy in miniEnemies)
            {
                SplitEnemy split = enemy.GetComponent<SplitEnemy>();
                if (split != null && split.currentHP > 0)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead && miniEnemies.Length > 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null && lastDeadEnemy != null && MainCameraScript.Instance != null)
                {
                    // 最後に死んだ敵にフォーカス
                    MainCameraScript.Instance.SetDefaultTarget(player.transform);
                    MainCameraScript.Instance.FocusOn(lastDeadEnemy.transform, bossTime);
                }

                stage2Boss = true;
                StartCoroutine(Stage2BossClear());
            }
        }


        if (isLevelUp)
        {
            if (levelUpPanel != null)
                levelUpPanel.SetActive(true);
            
            

            if (levelUpRandom)
            {
                if (StetusChange.Instance != null)
                    StetusChange.Instance.PrepareLevelUpOptions();
                    
                levelUpRandom = false;
            }

            if (StartTutorialLeveUp)
            {
                

                tutorialLeveUpPlate = false;
                tutorialLeveUp = true;
                StartTutorialLeveUp = false;
            }
        }
        else
        {
            if (levelUpPanel != null)
                levelUpPanel.SetActive(false);
            levelUpRandom = true;
            tutorialLeveUp = false;
        }

    }
    public void SetLastDeadEnemy(GameObject enemy)
    {
        lastDeadEnemy = enemy;
    }
    private IEnumerator TutorialBossClear()
    {
        ShootEnemy.Instance.Die();
        EnemySpawn.Instance.DestroyAllEnemies();

        // カメラフォーカス時間待機
        yield return new WaitForSeconds(3f);
        if (FadeController.Instance != null)
        {
            yield return StartCoroutine(FadeController.Instance.FadeOut());
        }

        bgmSource.Stop();
        //シーン切り替え
        SceneManager.LoadScene("Title");
        
    }
    private IEnumerator Stage1BossClear()
    {
        nWayBullet.Instance.Die();

        EnemySpawn.Instance.DestroyAllEnemies();
        // カメラフォーカス時間待機
        yield return new WaitForSeconds(3f);
        if (FadeController.Instance != null)
        {
            yield return StartCoroutine(FadeController.Instance.FadeOut());
        }

        PlayerMove.Instance.transform.position = playerSpawnStage2;
        StetusScript.Instance.Save();
        yield return new WaitForSeconds(2f);
        if (FadeController.Instance != null)
        {
            yield return StartCoroutine(FadeController.Instance.FadeIn());
            if (UIFollowWorldObject.Instance != null)
            {
                UIFollowWorldObject.Instance.ShowUI(true);
            }
        }
    }

    public void OnEnemyKilled(nWayBullet enemy)
    {
        enemyKillCount++;

        if (enemyKillCount == totalEnemyCount)
        {
            // 最後の敵 → カメラフォーカス → 遅延削除
            enemy.StartCoroutine(DelayedDestroyWithFocus(enemy));
        }
        else
        {
            // 即時削除（先に倒された敵）
            Destroy(enemy.gameObject);
        }
    }

    private IEnumerator DelayedDestroyWithFocus(nWayBullet enemy)
    {
        MainCameraScript.Instance.FocusOn(enemy.transform, 3f);
        stage1Boss = true;
        yield return new WaitForSeconds(3f); // フォーカス時間に合わせる
        Destroy(enemy.gameObject);
    }
    private IEnumerator Stage2BossClear()
    {

        
        // カメラフォーカス時間待機
        yield return new WaitForSeconds(3f);
        if (FadeController.Instance != null)
        {
            yield return StartCoroutine(FadeController.Instance.FadeOut());
        }

        
        EnemySpawn.Instance.DestroyAllEnemies();
        GameObject[] miniEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");
        foreach (GameObject enemy in miniEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        bgmSource.Stop();
        //シーン切り替え
        SceneManager.LoadScene("GameClear");

        

    }
    public void SetBgmVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = Mathf.Clamp01(volume);
        }
    }
    private IEnumerator TimeDie()
    {
        
        yield return new WaitForSeconds(3f);

    }
    void Awake()
    {
        Instance = this;

    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
