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
                // EnemySpawnオブジェクトの位置を基準にした相対位置でスポーン
                Vector3 spawnPos = transform.position + (Vector3)p;
                GameObject enemyObj = Instantiate(data.enemyPrefab, spawnPos, data.rot);
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

    // 生成された敵のリストを取得
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
        public List<Vector2> pos;              // EnemySpawn位置からの相対配置位置のリスト
        public Quaternion rot;                 // 回転
        public GameObject enemyPrefab;         // 敵のプレハブ
        public string ID;                      // 任意のID
        public bool isSpawn;                   // この敵を生成するかどうか
    }
}
