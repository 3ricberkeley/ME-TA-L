using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelection : MonoBehaviour
{
    #region Canvas_variables
    public GameObject canvas;
    public GameObject clockStrikesCover;
    public GameObject kingslayerCover;
    #endregion

    #region Selection_variables
    // Need to implement this later (rn it's using a fixed constant to initialize the list)
    public static int songListLen;
    static string[] songList;

    public static int currSelected;
    #endregion

    #region Selection_functions
    // Select the song that's above the current one
    void SelectAbove()
    {
        if (currSelected >= 1) {
            currSelected -= 1;
        }
    }

    // Select the song that's below the current one
    void SelectBelow()
    {
        if (currSelected < songListLen - 1) {
            currSelected += 1;
        }
    }

    // Display the selected song's cover
    void DisplayCover()
    {
        Instantiate(clockStrikesCover, canvas.transform);
    }
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        songList = new string[2]{"Clock Strikes", "Kingslayer"};
        currSelected = 0;
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
