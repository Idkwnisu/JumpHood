using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public GameObject gate;

    public Transform target;

    public bool active = false;

    public float lambda = 50.0f;

    private bool closed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(active && !closed)
        {
            gate.transform.position = Gate.DampVector(gate.transform.position, target.position, lambda, Time.deltaTime);
            if(Vector3.Distance(gate.transform.position, target.position) < 0.01f)
            {
                AudioManager.Instance.PlayAudioClue("Gate");
                closed = true;
            }
        }
    }

    public static Vector3 DampVector(Vector3 a, Vector3 b, float lambda, float dt)
    {
        Vector3 result = new Vector3(Gate.Damp(a.x, b.x, lambda, dt), Gate.Damp(a.y, b.y, lambda, dt), Gate.Damp(a.z, b.z, lambda, dt));

        return result;
    }

    public static float Damp(float a, float b, float lambda, float dt)
    {
        return Mathf.Lerp(a, b, 1 - Mathf.Exp(-lambda * dt));
}

    void OnTriggerEnter(Collider info)
    {
        if(info.CompareTag("Player"))
        {
            active = true;
        }
    }
}
