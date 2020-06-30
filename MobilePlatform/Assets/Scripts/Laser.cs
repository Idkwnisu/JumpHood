using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer line;
    private BoxCollider box;

    public Material warningMat;
    public Material laserMat;

    public Transform first;
    public Transform second;

    private bool growth = false;
    public float growthRate = 0.6f;

    public float delay = 0.0f;

    public float stopTime = 1.0f;
    public float warningTime = 0.4f;
    public float laserTime = 0.7f;

    public float warningWidth = 0.3f;

    public float laserWidth = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartLaser", delay);
        line = GetComponent<LineRenderer>();
        box = GetComponent<BoxCollider>();

        growth = false;
        line.startWidth = 0.0f;
        line.endWidth = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(growth)
        {
            float newWidth = Mathf.Lerp(line.startWidth, laserWidth, growthRate);
            line.startWidth = newWidth;
            line.endWidth = newWidth;
        }
    }

    IEnumerator LaserCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(stopTime);
            line.startWidth = warningWidth;
            line.endWidth = warningWidth;
            line.material = warningMat;

            yield return new WaitForSeconds(warningTime);
            line.material = laserMat;
            growth = true;

            box.isTrigger = false;

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            RaycastHit hit;
            Debug.DrawRay(first.position, (second.position - first.position).normalized * Vector3.Distance(first.position, second.position), Color.blue, 300.0f);
            if (Physics.Raycast(first.position, (second.position - first.position).normalized, out hit, Vector3.Distance(first.position, second.position), layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    CheckpointManager.Instance.resetPlayer();
                    
                }
            }

            yield return new WaitForSeconds(laserTime);
            growth = false;
            line.startWidth = 0.0f;
            line.endWidth = 0.0f;

            box.isTrigger = true;

            
        }
    }

    void StartLaser()
    {
        StartCoroutine("LaserCoroutine");
    }
}
