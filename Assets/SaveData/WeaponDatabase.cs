using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponDatabase Instance;

    [System.Serializable]
    public class WeaponEntry
    {
        public string id;
        public GameObject prefab;
    }

    public List<WeaponEntry> weapons;

    private Dictionary<string, GameObject> weaponDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
        foreach (var entry in weapons)
        {
            if (!weaponDict.ContainsKey(entry.id))
            {
                weaponDict.Add(entry.id, entry.prefab);
            }
        }
    }

    public GameObject GetWeaponPrefabByID(string id)
    {
        if (weaponDict.ContainsKey(id))
        {
            return weaponDict[id];
        }
        Debug.LogWarning($"ïêäÌID {id} Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩ");
        return null;
    }
}
