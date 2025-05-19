using UnityEngine;

public class Weapon : MonoBehaviour
{
    //[SerializeField]
    private string ID;

    public void SetID(string id)
    {
        ID = id;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                WeaponLogger.Add(ID);
                Debug.Log("ç~ÇÍÇΩ");
                Destroy(this.gameObject);
            }
        }
    }
}
