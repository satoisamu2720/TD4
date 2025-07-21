using UnityEngine;

public class ArrowFlag : MonoBehaviour
{
    
    public string playerTag = "Player"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (UIFollowWorldObject.Instance != null)
            {
                UIFollowWorldObject.Instance.ShowUI(false);
            }
        }
    }
}
