using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    #region Audio_vars
    // Audio source
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
    // Welcome mask
    public GameObject welcome;
    // Feel mask
    public GameObject feel;
    // Lanes mask
    public GameObject lanes;
    #endregion

    #region Pause_funcs
    // IEnumerator for the welcome msg
    IEnumerator WelcomeCoroutine()
    {
        ToggleMask("welcome", true);
        yield return new WaitForSeconds(1);
        ToggleMask("welcome", false);
        ToggleMask("feel", true);
        yield return new WaitForSeconds(2);
        ToggleMask("feel", false);
        yield return new WaitForSeconds(2);
        ToggleMask("lanes", true);
        yield return new WaitForSeconds(2);
        ToggleMask("lanes", false);
    }

    // Change the visibility of the type UI mask
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
        } else if (type == "welcome")
        {
            welcome.gameObject.SetActive(state);
        } else if (type == "feel")
        {
            feel.gameObject.SetActive(state);
        } else if (type == "lanes")
        {
            lanes.gameObject.SetActive(state);
        }
    }

    // Pause the game within 0.015f of timestamp t
    void PauseGame(float t, string type)
    {
        if (!isPaused && (Time.timeSinceLevelLoad >= t && Time.timeSinceLevelLoad < t + 0.015f))
        {
            isPaused = true;
            noteType = type;
            ToggleMask(type, true);
            audioSource.Pause();
            Time.timeScale = 0.0f;
        }
    }

    // IEnumerator for the completed message and transition to the song selection scene
    IEnumerator CompletedTutorial()
    {
        if (Time.timeSinceLevelLoad >= 35.0f && Time.timeSinceLevelLoad < 35.025f)
        {
            completed.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("Selection");
        }
    }
    #endregion

    #region Unity_funcs
    // Initialize pause variables
    void Start()
    {
        isPaused = false;
        noteType = "normal";
        StartCoroutine(WelcomeCoroutine());
    }

    // Pause the tutorial at certain timestamps
    void FixedUpdate()
    {
        //Debug.Log((Time.timeSinceLevelLoad).ToString());
        PauseGame(2.76f + 7.0f, "normal");
        PauseGame(12.66f + 7.0f, "hold");
        PauseGame(30.15f, "text");
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
            audioSource.UnPause();
            Time.timeScale = 1.0f;
        }
    }
    #endregion
}
