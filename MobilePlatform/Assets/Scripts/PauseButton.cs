using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public CharacterScript player;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Debug.Log("PAUSE");
        player.Pause();
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("PAUSE");
        player.Resume();
        pausePanel.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("LevelChoose");
    }
}
