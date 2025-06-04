using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public float speed = 2f;         
    public int maxHP = 3;            
    private int currentHP;
    public GameObject bulletPrefab;  
    public float shootInterval = 2f; 
    private float shootTimer;

    public GameObject itemPrefab;    
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;
        shootTimer = shootInterval;
    }

    void Update()
    {
        if (player != null)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval; 
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;

            Destroy(bullet, 5f);
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Bullet")) 
    //    {
    //        TakeDamage(1);
    //        Destroy(collision.gameObject);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
