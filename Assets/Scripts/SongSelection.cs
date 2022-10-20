using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SongSelection : MonoBehaviour
{
    #region Canvas_variables
    public GameObject canvas;
    public GameObject coverImgCanvas;
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
        if (currSelected >= 1) {
            currSelected -= 1;
        }

        // TODO: Enlarge the tab of the currently selected song

        // Display the cover of the selected song
        DisplayCover(songList[currSelected]);
    }

    // Select the song that's below the current one
    void SelectBelow()
    {
        if (currSelected < songListLen - 1) {
            currSelected += 1;
        }

        // TODO: Enlarge the tab of the currently selected song

        // Display the cover of the selected song
        DisplayCover(songList[currSelected]);
    }

    // Display the selected song's cover
    void DisplayCover(string songName)
    {
        string songBgName = songName.ToLower().Replace(" ", "") + "cover";
        Sprite sp = Resources.Load<Sprite>("Sprites/" + songBgName);
        coverImgCanvas.GetComponent<Image>().sprite = sp;
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

        // Set the current selected song to the top of the list
        currSelected = 0;
        DisplayCover(songList[currSelected]);
    }

    // Update is called once per frame
    void Update()
    {
        // If the up and down arrow keys are pressed, switch songs that the player is selecting
        if (Input.GetKeyDown("up")) {
            SelectAbove();
        } else if (Input.GetKeyDown("down")) {
            SelectBelow();
        }
    }
    #endregion
}
