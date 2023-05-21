using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseCanvas;
    public bool isPaused;

    public void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseCanvas = GameObject.Find("Pause Canvas");
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape") && pauseCanvas.GetComponent<Canvas>().enabled == true)
        {
            Unpause();
        } else if (Input.GetKeyDown("escape") && pauseCanvas.GetComponent<Canvas>().enabled == false && !isPaused)
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
