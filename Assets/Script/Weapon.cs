using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    public string ID;

    public void SetID(string id)
    {
        ID = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponLogger.Add(ID);
            Destroy(this.gameObject);
        }
    }
}
