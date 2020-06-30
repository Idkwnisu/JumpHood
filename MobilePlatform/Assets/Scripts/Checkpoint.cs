using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;
    public bool overrideDirection = false;
    public Vector3 newDirection;

    public GameObject toActivate;
    public GameObject toDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            CheckpointManager.Instance.setNewCheckpoint(this, overrideDirection, newDirection);
            toDeactivate.SetActive(false);
            toActivate.SetActive(true);
        }
    }
}
