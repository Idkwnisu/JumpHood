using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public AudioClip[] clips;

    public string[] nameOfClips;

    private Dictionary<string, AudioClip> audioClips;

    private AudioSource source;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
        {
        audioClips = new Dictionary<string, AudioClip>();
            for(int i = 0; i < nameOfClips.Length; i++)
            {
                audioClips.Add(nameOfClips[i], clips[i]);
            }
        source = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

    public void PlayAudioClue(string clue)
    {
        source.PlayOneShot(audioClips[clue]);
    }
   
}
