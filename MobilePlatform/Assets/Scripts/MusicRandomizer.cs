using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        RandomizeMusic();
    }

    public void RandomizeMusic()
    {
        int rnd = Random.Range(0, clips.Length);
        source.clip = clips[rnd];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
