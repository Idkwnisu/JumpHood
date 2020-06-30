using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{

    public float time = 2.0f;

    public bool lowerMusic = false;

    public float ratio = 20.0f;

    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lowerMusic)
        {
            MusicManager.Instance.volume = MusicManager.Instance.volume - ratio * Time.deltaTime / time;
        }
    }

    void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Player"))
        {
            Invoke("GoBack", time);
            other.GetComponent<CharacterScript>().PlayNextLevelParticle();
            lowerMusic = true;

            string key = "Level" + level;
            PlayerPrefs.SetFloat(key, 1.0f);
            PlayerPrefs.SetFloat("LevelCompleted", 1.0f);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("LevelChoose");
    }
}
