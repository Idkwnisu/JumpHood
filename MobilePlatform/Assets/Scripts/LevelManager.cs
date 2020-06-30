using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public GameObject tutorial;

    public float tutorialTime = 0.8f;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(string newLevel)
    {
        SceneManager.LoadScene(newLevel);
    }

    public void FirstLevel()
    {
        Invoke("GoToFirstLevel", tutorialTime);
        tutorial.SetActive(true);
    }

    public void GoToFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
