using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] List<WeaponCount> weaponCounts ;


    private int weaponCount;

    private List<GameObject> spawnedWeapon = new List<GameObject>();   // ê∂ê¨çœÇ›ÇÃïêäÌÇÃÉäÉXÉg

   
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
               
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                //weapon.SetID(data.ID);
                weaponObj.SetActive(true);
                // ï`âÊÇÃèáî‘
                SpriteRenderer sr = weaponObj.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingOrder = data.sortingOrder;
                }
            }
        }
    }

    public void SpawnItem()
    {
        SpawnWeapon();
    }

    public List<GameObject> GetSpawnedItem()
    {
        return spawnedWeapon;
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
        public int sortingOrder = 0;
        public bool isSpawn;
    }
}
