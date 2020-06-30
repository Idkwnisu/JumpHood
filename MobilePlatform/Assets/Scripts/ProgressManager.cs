using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager _instance;

    public static ProgressManager Instance { get { return _instance; } }

    private bool[] LevelUnlocked;

    public Button[] buttons;

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
        LevelUnlocked = new bool[buttons.Length];
        LevelUnlocked[0] = false;

        for(int i = 1; i < LevelUnlocked.Length; i++)
        {
            string key = "Level" + i;
            if (PlayerPrefs.HasKey(key))
            {
                LevelUnlocked[i] = true;
                buttons[i].interactable = true;
            }
            else
            {
                LevelUnlocked[i] = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
