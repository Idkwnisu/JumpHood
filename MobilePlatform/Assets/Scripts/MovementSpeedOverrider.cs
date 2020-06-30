using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedOverrider : MonoBehaviour
{
    public Vector3 overrideSpeed;

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
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<CharacterScript>().movementDirection = overrideSpeed;
        }
    }
}
