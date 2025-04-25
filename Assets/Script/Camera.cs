using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject target;

    [SerializeField]
    private Vector3 cameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraPos = target.transform.position;
        cameraPos.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos.x = target.transform.position.x;
        cameraPos.y = target.transform.position.y;
        cameraPos.z = -10;

        this.transform.position = cameraPos;


    }



}
