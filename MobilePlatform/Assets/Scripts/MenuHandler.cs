using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject startLevelPanel;
    public GameObject credits;
    public GameObject menuPane;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("LevelCompleted"))
        {
            PlayerPrefs.DeleteKey("LevelCompleted");
            changePanel("Start");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePanel(string panel)
    {
        if(panel == "Start")
        {
            startLevelPanel.SetActive(true);
            menuPane.SetActive(false);
        }
        else if(panel == "Credits")
        {
            menuPane.SetActive(false);
            credits.SetActive(true);
        }
        else if(panel == "Menu")
        {
            credits.SetActive(false);
            startLevelPanel.SetActive(false);
            menuPane.SetActive(true);
        }
    }
}
