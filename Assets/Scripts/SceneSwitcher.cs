using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    #region Menu_vars
    // Menu game object
    public GameObject menu;
    // Bool indicating whether or not the menu is open
    private bool menuIsOpen;
    #endregion

    #region Song_vars
    // Song game object
    public AudioSource songAS;
    // Song name
    private string songName;
    #endregion

    #region Pause_funcs
    // Pause the game
    void PauseGame()
    {
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }

    // Unpause the game
    void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
    #endregion

    #region Scene_funcs
    // Transition to the song selection scene
    public void LoadSelection()
    {
        Debug.Log("loading select screen");
        SceneManager.LoadScene("Selection");
    }

    // Transition to the how to play screen
    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    // Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Unity_funcs
    // Initialize variables
    void Start()
    {
        menuIsOpen = false;
        songName = songAS.name.Replace("AS", "");
        Debug.Log(songName);
    }

    // After the song ends, wait 5~ seconds and return to the selection scene
    void FixedUpdate()
    {
        //Debug.Log(Time.timeSinceLevelLoad.ToString());
        if (songName == "ClockStrikes" && (Time.timeSinceLevelLoad >= 70.0f && Time.timeSinceLevelLoad < 70.015f))
        {
            Debug.Log("Switch scene on clock strikes");
            LoadSelection();
        }
        if (songName == "Kingslayer" && (Time.timeSinceLevelLoad >= 165.0f && Time.timeSinceLevelLoad < 165.25f))
        {
            Debug.Log("Switch scene on kingslayer");
            LoadSelection();
        }
        if (songName == "Mozaik Role" && (Time.timeSinceLevelLoad >= 108.0f && Time.timeSinceLevelLoad < 108.5f))
        {
            Debug.Log("Switch scene on mozaik role");
            LoadSelection();
        }
    }

    // Whenever ESCAPE is pressed, open/close the menu
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (menuIsOpen)
            {
                menu.gameObject.SetActive(false);
                menuIsOpen = false;
                UnpauseGame();
            } else
            {
                menu.gameObject.SetActive(true);
                menuIsOpen = true;
                PauseGame();
            }
        }
    }

    // Before the scene is switched, unpause the game
    // (This is in the case that the player uses the menu)
    void OnDestroy()
    {
        UnpauseGame();
    }
    #endregion
}
