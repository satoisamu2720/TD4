using UnityEngine;

public class Weapon : MonoBehaviour
{
    //[SerializeField]
    private string ID;

    public void SetID(string id)
    {
        ID = id;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponLogger.Add(ID);
            Debug.Log("ç~ÇÍÇΩ");
            Destroy(this.gameObject);
        }
    }
}
