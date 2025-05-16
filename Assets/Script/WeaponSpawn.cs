using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] List<WeaponCount> weaponCounts;

    private List<GameObject> spawnedItem = new List<GameObject>();   // 生成済みの敵のリスト

    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {

        //this.weapon = FindObjectOfType<Weapon>(); // インスタンス化

        // 初期配置
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        for (int i = 0; i < 5; i++)
        {
            ItemGenerator(weaponCounts[i]);
        }
    }

    private void ItemGenerator(WeaponCount data)
    {
        if (data.isSpawn)
        {
            foreach (Vector2 p in data.pos)
            {
                // 敵を生成してリストに追加する
                GameObject weaponObj = Instantiate(data.itemPrefabs, p, data.rot);
                spawnedItem.Add(weaponObj);
                this.weapon = GetComponent<Weapon>(); // インスタンス化
                weapon.SetID(data.ID);
                weaponObj.SetActive(true);
            }
        }
    }

    public void SpawnItem()
    {
        SpawnWeapon();
    }

    public List<GameObject> GetSpawnedItem()
    {
        return spawnedItem;
    }

    [System.Serializable]
    public class WeaponCount
    {
        public enum Weapon
        {
            Item0,
            Item1,
            Item2,
            Item3,
            Item4,
            Item5,
            // これがラベルになる
        }

        public Weapon item;
        public List<Vector2> pos;
        public Quaternion rot;
        public GameObject itemPrefabs;
        public string ID;
        public bool isSpawn;
    }
}
