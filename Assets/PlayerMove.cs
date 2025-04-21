using UnityEngine;

public class PlayerMove
{

    private Rigidbody rb;

    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            position.y -= speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            position.x += speed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            position.y -= speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            position.x += speed;
        }


    }


}
