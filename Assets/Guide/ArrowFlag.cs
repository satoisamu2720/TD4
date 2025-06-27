using UnityEngine;

public class ArrowFlag : MonoBehaviour
{
    
    public string playerTag = "Player"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(false);
            }
        }
    }
}
