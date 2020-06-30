using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] transforms;

    public int currentPlatformTarget;

    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = (transforms[currentPlatformTarget].position - transform.position).normalized;

        transform.position += movement * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, transforms[currentPlatformTarget].position) < 0.5f)
        {
            currentPlatformTarget++;
            if(currentPlatformTarget >= transforms.Length)
            {
                currentPlatformTarget = 0;
            }
        }
    }
}
