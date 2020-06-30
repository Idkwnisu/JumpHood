using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overrider : MonoBehaviour
{
    public Vector3 overrideSpeed;
    public float time;
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
            CharacterScript script = collider.GetComponent<CharacterScript>();

            if (!script.overridedMove)
            {
                collider.GetComponent<CharacterScript>().MoveTo(transform.position);
            }
            collider.GetComponent<CharacterScript>().OverrideSpeed(overrideSpeed, time);
            AudioManager.Instance.PlayAudioClue("Boost");
        }
    }
}
