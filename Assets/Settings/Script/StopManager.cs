using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> targetObjects = new List<GameObject>();

    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private List<MonoBehaviour> scripts = new List<MonoBehaviour>();
    private Dictionary<Rigidbody, Vector3> storedVelocities = new Dictionary<Rigidbody, Vector3>();
    private Dictionary<Rigidbody, Vector3> storedAngularVelocities = new Dictionary<Rigidbody, Vector3>();

    public static StopManager Instance { get; private set; }

    void Start()
    {
        foreach (var obj in targetObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rigidbodies.Add(rb);
            }

            var behaviours = obj.GetComponents<MonoBehaviour>();
            foreach (var b in behaviours)
            {
                if (b != this) 
                {
                    scripts.Add(b);
                }
            }
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    Pause();
        //}

        if (Input.GetKey(KeyCode.O))
        {
            Resume();
        }
    }

    private void Awake()
    {
        Instance = this;
    }


    public void Pause()
    {

        scripts.RemoveAll(script => script == null);

        foreach (var rb in rigidbodies)
        {
            storedVelocities[rb] = rb.linearVelocity;
            storedAngularVelocities[rb] = rb.angularVelocity;

            rb.isKinematic = true;
        }

        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }

    public void Resume()
    {

        scripts.RemoveAll(script => script == null);

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;

            if (storedVelocities.ContainsKey(rb))
                rb.linearVelocity = storedVelocities[rb];
            if (storedAngularVelocities.ContainsKey(rb))
                rb.angularVelocity = storedAngularVelocities[rb];
        }


        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }

}
