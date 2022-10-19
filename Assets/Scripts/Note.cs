using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {
    #region Note_variables
    // Timestamp the note is at
    float timePos;
    // Lane number the note is in
    int lane;
    // Type of note ("normal", "hold", "burst")
    string type;
    #endregion

    #region Note_functions
    // Note Constructor
    public Note(float t, int l, string ty)
    {
        timePos = t;
        lane = l;
        type = ty;
    }
    public Note(string[] args) {
        timePos = (float)TimeSpan.Parse(args[0]).TotalSeconds;
        lane = int.Parse(args[1]);
        type = args[2];
    }

    // Return the timestamp that the note is at
    public float GetTimePos()
    {
        return timePos;
    }

    // Return the lane number that the note appears in
    public int GetLane()
    {
        return lane;
    }

    // Return the string describing the note's type
    public string GetNoteType()
    {
        return type;
    }
    #endregion
}
