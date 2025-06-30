using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
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
                GameObject enemyObj = Instantiate(data.enemyPrefab, p, data.rot);
                spawnedEnemies.Add(enemyObj);
                enemyObj.SetActive(true);
            }
        }
    }

    public void SpawnEnemiesManually()
    {
        SpawnEnemies();
    }

    public List<GameObject> GetSpawnedEnemies()
    {
        return spawnedEnemies;
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

        public EnemyType enemyType;            // ラベル的な用途
        public List<Vector2> pos;              // 配置位置のリスト
        public Quaternion rot;                 // 回転
        public GameObject enemyPrefab;         // 敵のプレハブ
        public string ID;                      // 任意のID
        public bool isSpawn;                   // この敵を生成するかどうか
    }

   
}
