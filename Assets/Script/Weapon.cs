using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string ID;

    private void Start()
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
    }

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
                PlayerMove playerMove = other.GetComponent<PlayerMove>();
                playerMove.WeaponObj(this.gameObject);
                Debug.Log("ç~ÇÍÇΩ");
                Destroy(this.gameObject);
            }
        }
    }
}
