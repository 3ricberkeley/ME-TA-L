using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Transition to the song selection screen
    public void LoadSelection()
    {
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
}
