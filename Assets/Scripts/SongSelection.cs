using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SongSelection : MonoBehaviour
{
    #region Canvas_variables
    public GameObject canvas;
    public GameObject coverImgCanvas;

    public GameObject clockStrikesGO;
    public GameObject kingslayerGO;
    Button clockStrikesButton;
    Button kingslayerButton;

    public AudioSource clockStrikesAS;
    public AudioSource kingslayerAS;

    public SceneSwitcher scSw;
    #endregion

    #region Selection_variables
    // Need to implement this later (rn it's using a fixed constant to initialize the list)
    static int songListLen;
    static Dictionary<int, string> songList;

    public static int currSelected;
    static string difficulty;
    #endregion

    #region Selection_functions
    // Select the song that's above the current one
    void SelectAbove()
    {
        // Update the currSelected counter
        if (currSelected >= 1)
        {
            // Selects the button of the current tab, deselecting the previous
            currSelected -= 1;
            SelectTab(songList[currSelected]);
            // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
    }

    // Select the song that's below the current one
    void SelectBelow()
    {
        if (currSelected < songListLen - 1)
        {
            // Selects the button of the current tab, deselecting the previous
            currSelected += 1;
            SelectTab(songList[currSelected]);
            // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
    }

    // Switch to easy mode
    void DecreaseDifficulty()
    {
        // Update the difficulty if it's currently in hard mode
        if (difficulty == "Hard")
        {
            // Switch the difficulty to easy mode
            difficulty = "Easy";
            Song.difficulty = difficulty;

            // Switch the sprite of the tabs
            Sprite spClock = Resources.Load<Sprite>("Sprites/" + "select_clock");
            clockStrikesGO.GetComponent<Image>().sprite = spClock;

            Sprite spKing = Resources.Load<Sprite>("Sprites/" + "select_kingslayer");
            kingslayerGO.GetComponent<Image>().sprite = spKing;
        }
    }

    // Switch to hard mode
    void IncreaseDifficulty()
    {
        // Update the difficulty if it's currently in easy mode
        if (difficulty == "Easy")
        {
            // Switch the difficulty to hard mode
            difficulty = "Hard";
            Song.difficulty = difficulty;

            // Switch the sprite of the tabs
            Sprite spClock = Resources.Load<Sprite>("Sprites/" + "select_clock_hard");
            clockStrikesGO.GetComponent<Image>().sprite = spClock;

            Sprite spKing = Resources.Load<Sprite>("Sprites/" + "select_kingslayer_hard");
            kingslayerGO.GetComponent<Image>().sprite = spKing;
        }
    }

    // Inner workings to select a tab
    void SelectTab(string songName)
    {
        // Deselect the button of the previously selected song
        SelectButton(songName);
        
        // Display the cover of the currently selected song
        string songBgName = songName.ToLower().Replace(" ", "") + "cover";
        Sprite sp = Resources.Load<Sprite>("Sprites/" + songBgName);
        coverImgCanvas.GetComponent<Image>().sprite = sp;
    }

    // Select a button
    void SelectButton(string songName)
    {
        // Select the button of the currently selected song
        if (songList[currSelected] == "Clock Strikes")
        {
            scSw.audioSource.Stop();
            scSw.audioSource = clockStrikesAS;
            scSw.audioSource.Play();
            clockStrikesButton.Select();
        }
        else if (songList[currSelected] == "Kingslayer")
        {
            scSw.audioSource.Stop();
            scSw.audioSource = kingslayerAS;
            scSw.audioSource.Play();
            kingslayerButton.Select();
        }
    }

    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        // Set the song list length
        songListLen = 2;

        // Generate the song list dictionary
        songList = new Dictionary<int, string>();
        songList.Add(0, "Clock Strikes");
        songList.Add(1, "Kingslayer");

        // Set the current difficulty
        difficulty = "Easy";
        Song.difficulty = difficulty;

        // Set the songs' button variables
        clockStrikesButton = clockStrikesGO.GetComponent<Button>();
        kingslayerButton = kingslayerGO.GetComponent<Button>();

        // Set the current selected song to the top of the list
        currSelected = 0;
        scSw.audioSource = clockStrikesAS;
        SelectTab(songList[currSelected]);
    }

    // Update is called once per frame
    void Update()
    {
        // If the up and down arrow keys are pressed, switch songs that the player is selecting
        if (Input.GetKeyDown("up"))
        {
            SelectAbove();
        } else if (Input.GetKeyDown("down"))
        {
            SelectBelow();
        } else if (Input.GetKeyDown("left"))
        {
            DecreaseDifficulty();
        } else if (Input.GetKeyDown("right"))
        {
            IncreaseDifficulty();
        }
    }
    #endregion
}
