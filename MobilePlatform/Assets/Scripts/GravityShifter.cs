using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShifter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            CharacterScript cs = collider.gameObject.GetComponent<CharacterScript>();
            if (cs.verticalMultiplier == 1.0f)
            {
                cs.verticalMultiplier = -1.0f;
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().flipY = true;
                AudioManager.Instance.PlayAudioClue("Gravity");
            }
        }
    }

}
