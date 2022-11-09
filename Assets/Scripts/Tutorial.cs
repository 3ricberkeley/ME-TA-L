using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    #region Audio_vars
    public AudioSource audioSource;
    #endregion

    #region Pause_vars
    private bool isPaused;
    #endregion

    #region Pause_funcs
    void PauseGame(float p)
    {
        if (Time.timeSinceLevelLoad == p)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            AudioListener.pause = true;
        }
    }
    #endregion

    #region Unity_funcs
    void Start()
    {
        isPaused = false;
    }

    // Pause the tutorial at certain timestamps
    void FixedUpdate()
    {
        // Debug.Log(Time.timeSinceLevelLoad.ToString());
        PauseGame(2.8f);
        PauseGame(20.8f);
        PauseGame(27.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && (Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("k") || Input.GetKeyDown("l")))
        {
            // Debug.Log("KEY was pressed!");
            isPaused = false;
            Time.timeScale = 1.0f;
            AudioListener.pause = false;
        }
    }
    #endregion
}
