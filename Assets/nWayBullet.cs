using UnityEngine;

public class nWayBullet : MonoBehaviour
{
    public GameObject Bullet;
    public float _Velocity_0 = 5f;      // 弾の速度
    public float Degree = 60f;          // 弾の扇状角度（度）
    public int Angle_Sprite = 5;        // 弾の本数（例：5-way）

    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += 0.1f * Input.GetAxisRaw("Horizontal");
        transform.position = pos;


        if (Input.GetKeyDown(KeyCode.Space)) // スペース押したときに発射
        {
            float baseAngle = -Degree * 0.5f;  // 左端の角度から開始
            for (int i = 0; i < Angle_Sprite; i++)
            {
                float angle = baseAngle + Degree * i / (Angle_Sprite - 1); // 等間隔角度
                float rad = angle * Mathf.Deg2Rad;

                GameObject bulletObj = Instantiate(Bullet, transform.position, Quaternion.identity);
                BulletCs bulletCs = bulletObj.GetComponent<BulletCs>();
                bulletCs.theta = rad;
                bulletCs.Velocity_0 = _Velocity_0;

                // 5秒後に自動破壊（時間で）
                Destroy(bulletObj, 5f);
            }
        }
    }
}
