﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour {


    public Transform canvas;
    public Transform mainButtons;
    public Transform settingButtons;
    private bool paused = false;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
        else
        {
            // If the player leaves the pause menu when in settings, next time the pause menu is entered
            // it will go main pause menu instead of where the player leaved the pause menu
            mainButtons.gameObject.SetActive(true);
            settingButtons.gameObject.SetActive(false);

            canvas.gameObject.SetActive(false);

            paused = false;
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        // Must activate time scale before entering main menu, else the player wont be able
        // to move when entering play again from main menu
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        //Hide main buttons and show settings buttons
        mainButtons.gameObject.SetActive(false);
        settingButtons.gameObject.SetActive(true);

    }

    public void BackToPauseMenu()
    {
        //Hide setting buttons and show main buttons
        mainButtons.gameObject.SetActive(true);
        settingButtons.gameObject.SetActive(false);
    }

    public bool IsPaused()
    {
        return paused;
    }
}