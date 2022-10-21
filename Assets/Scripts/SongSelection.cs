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
    #endregion

    #region Selection_variables
    // Need to implement this later (rn it's using a fixed constant to initialize the list)
    static int songListLen;
    static Dictionary<int, string> songList;

    public static int currSelected;
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
            clockStrikesButton.Select();
        }
        else if (songList[currSelected] == "Kingslayer")
        {
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

        // Set the songs' button variables
        clockStrikesButton = clockStrikesGO.GetComponent<Button>();
        kingslayerButton = kingslayerGO.GetComponent<Button>();

        // Set the current selected song to the top of the list
        currSelected = 0;
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
        }
    }
    #endregion
}
