using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    #region Audio_vars
    public AudioSource audioSource;
    #endregion

    #region Pause_vars
    // Boolean indicating whether or not the game is paused
    private bool isPaused;
    // String for the type of note the game is paused for
    private string noteType;
    // Normal note mask
    public GameObject normalMask;
    // Hold note mask
    public GameObject holdMask;
    // Text note mask
    public GameObject textMask;
    // Completed GameObject
    public GameObject completed;
    #endregion

    #region Pause_funcs
    void ToggleMask(string type, bool state)
    {
        if (type == "normal")
        {
            normalMask.gameObject.SetActive(state);
        }
        else if (type == "hold")
        {
            holdMask.gameObject.SetActive(state);
        }
        else if (type == "text")
        {
            textMask.gameObject.SetActive(state);
        }
    }

    void PauseGame(float t, string type)
    {
        if (Time.timeSinceLevelLoad == t)
        {
            isPaused = true;
            noteType = type;
            ToggleMask(type, true);
            Time.timeScale = 0.0f;
            AudioListener.pause = true;
        }
    }

    IEnumerator CompletedTutorial()
    {
        if (Time.timeSinceLevelLoad == 33.0f)
        {
            completed.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("Selection");
        }
    }
    #endregion

    #region Unity_funcs
    void Start()
    {
        isPaused = false;
        noteType = "normal";
    }

    // Pause the tutorial at certain timestamps
    void FixedUpdate()
    {
        // Debug.Log(Time.timeSinceLevelLoad.ToString());
        PauseGame(2.8f, "normal");
        PauseGame(14.7f, "hold");
        PauseGame(30.3f, "text");
        StartCoroutine(CompletedTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && (Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("k") || Input.GetKeyDown("l")))
        {
            // Debug.Log("KEY was pressed!");
            isPaused = false;
            ToggleMask(noteType, false);
            Time.timeScale = 1.0f;
            AudioListener.pause = false;
        }
    }
    #endregion
}
