using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBooster : MonoBehaviour
{
    public float boost = 100.0f;

    public bool block = false;

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
            collider.GetComponent<CharacterScript>().yOffset = boost;
        }
    }
}
