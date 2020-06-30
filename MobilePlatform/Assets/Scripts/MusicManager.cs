using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    public static MusicManager Instance { get { return _instance; } }

    public AudioMixer mixer;

    public float volume;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       mixer.GetFloat("Volume", out volume);
    }

    // Update is called once per frame
    void Update()
    {
        mixer.SetFloat("Volume", volume);
    }

    public void ResetVolume()
    {
        volume = 0.0f;
    }
}
