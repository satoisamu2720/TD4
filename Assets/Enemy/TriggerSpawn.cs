using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public EnemySpawn enemySpawner; // Inspector ‚ÅŽw’è
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            enemySpawner.SpawnEnemiesManually();
            hasTriggered = true;
        }
    }
}
