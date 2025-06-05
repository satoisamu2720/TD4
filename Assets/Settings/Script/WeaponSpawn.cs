using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] List<WeaponCount> weaponCounts ;


    private int weaponCount;

    private List<GameObject> spawnedItem = new List<GameObject>();   

   
    void Start()
    {
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        for (int i = 0; i < weaponCounts.Count; i++)
        {
            WeaponGenerator(weaponCounts[i]);
        }
    }

    private void WeaponGenerator(WeaponCount data)
    {
        if (data.isSpawn)
        {
            foreach (Vector2 p in data.pos)
            {
               
                GameObject weaponObj = Instantiate(data.weaponPrefabs, p, data.rot);
                spawnedItem.Add(weaponObj);
               
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
            Weapon0,
            Weapon1,
            Weapon2,
            Weapon3,
            Weapon4,
            Weapon5,
          
        }

        public Weapon weapon;
        public List<Vector2> pos;
        public Quaternion rot;
        public GameObject weaponPrefabs;
        public string ID;
        public bool isSpawn;
    }
}
