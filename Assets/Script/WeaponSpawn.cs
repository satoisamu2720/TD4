using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] List<ItemCount> enemyCounts;

    private List<GameObject> spawnedItem = new List<GameObject>();   // 生成済みの敵のリスト

    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {

        //this.weapon = FindObjectOfType<Weapon>(); // インスタンス化

        // 初期配置
        SpawnItems();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < 5; i++)
        {
            ItemGenerator(enemyCounts[i]);
        }
    }

    private void ItemGenerator(ItemCount data)
    {
        if (data.isSpawn)
        {
            foreach (Vector3 p in data.pos)
            {
                // 敵を生成してリストに追加する
                GameObject itemObj = Instantiate(data.itemPrefabs, p, data.rot);
                spawnedItem.Add(itemObj);
                this.weapon = GetComponent<Weapon>(); // インスタンス化
                weapon.SetID(data.ID);
                itemObj.SetActive(true);
            }
        }
    }

    public void SpawnItem()
    {
        SpawnItems();
    }

    public List<GameObject> GetSpawnedItem()
    {
        return spawnedItem;
    }

    [System.Serializable]
    public class ItemCount
    {
        public enum Item
        {
            Item0,
            Item1,
            Item2,
            Item3,
            Item4,
            Item5,
            // これがラベルになる
        }

        public Item item;
        public List<Vector3> pos;
        public Quaternion rot;
        public GameObject itemPrefabs;
        public string ID;
        public bool isSpawn;
    }
}
