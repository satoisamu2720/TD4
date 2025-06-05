using UnityEngine;

public class BulletCs : MonoBehaviour
{
    public float Velocity_0, theta;

    Rigidbody2D rid2d;

    void Start()
    {
        rid2d = GetComponent<Rigidbody2D>();

        Vector2 bulletV;
        bulletV.x = Velocity_0 * Mathf.Cos(theta);
        bulletV.y = Velocity_0 * Mathf.Sin(theta);
        rid2d.linearVelocity = bulletV; 
    }
}
