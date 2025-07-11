using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance { get; private set; }

    [SerializeField] List<EnemyCount> enemyCounts;

 

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // 生成された敵のリスト

    void Start()
    {
        //SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCounts.Count; i++)
        {
            EnemyGenerator(enemyCounts[i]);
        }
    }

    private void EnemyGenerator(EnemyCount data)
    {
        if (data.isSpawn)
        {
            foreach (Vector2 p in data.pos)
            {

                // EnemySpawnオブジェクトの位置を基準にした相対位置でスポーン
                Vector3 spawnPos = transform.position + (Vector3)p;
                GameObject enemyObj = Instantiate(data.enemyPrefab, spawnPos, data.rot);

                GameObject enemyObj = Instantiate(data.enemyPrefab, p, data.rot);
                enemyObj.tag = "Enemy";

                spawnedEnemies.Add(enemyObj);
                enemyObj.SetActive(true);
            }
        }
    }

    // 手動で呼び出して敵をスポーンさせる
    public void SpawnEnemiesManually()
    {
        SpawnEnemies();
    }

   
    public List<GameObject> GetSpawnedEnemies()
    {
        return spawnedEnemies;
    }
    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    [System.Serializable]
    public class EnemyCount
    {
        public enum EnemyType
        {
            Enemy0,
            Enemy1,
            Enemy2,
            Enemy3,
            Boss,
        }

        public EnemyType enemyType;            
        public List<Vector2> pos;              
        public Quaternion rot;                 
        public GameObject enemyPrefab;         
        public string ID;                      
        public bool isSpawn;                   
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
