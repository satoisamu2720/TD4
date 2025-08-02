using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") ||
            collision.CompareTag("Enemy") ||
            collision.CompareTag("BossEnemy") ||
            collision.CompareTag("MiniEnemy"))
        {
            if (!PlayerMove.Instance.isInvincible)
            {
                PlayerMove.Instance.PlayerDamage();
               
            }
        }
        
    }
}
