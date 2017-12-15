using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public Text currencyIndicator;

    private void Start() {
        CurrencyManager.currencyManager.Load();
        currencyIndicator.text = "Currency: " + CurrencyManager.currencyManager.currency.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(3);
    }

    public void Shop()
    {
        //Store the scene the player entered the shop from
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(5);
    }

    public void PreviousScene()
    {
        //Get previous scene
        string lastScene = PlayerPrefs.GetString("lastLoadedScene");
        if (lastScene != null)
        {
            SceneManager.LoadScene(lastScene);
        }
        else {
            //Load main menu if there is no scene in lastScene variable
            SceneManager.LoadScene(0);
        }
        
    }
}
