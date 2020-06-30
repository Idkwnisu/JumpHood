using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject toSpawn;

    public Vector3 direction;
    public float speed;

    public float time = 1.5f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            animator.SetBool("Shoot", true);
           
            yield return new WaitForSeconds(time);
        }

    }

    void Spawn()
    {
        GameObject spawned = Instantiate(toSpawn, transform);
        spawned.GetComponent<Projectile>().direction = direction;
        spawned.GetComponent<Projectile>().Speed = speed;
        animator.SetBool("Shoot", false);
    }
}
